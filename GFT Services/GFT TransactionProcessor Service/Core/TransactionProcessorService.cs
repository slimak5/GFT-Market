using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;
using System.Web.Script.Serialization;
using GFT.Website.Api.Models;
using System.Xml.Serialization;
using System.Threading;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue middleToBackendQueue = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue backendToMiddleQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
        static Thread thread = new Thread(MainLoop);
        static HubConnection hubConnection = new HubConnection("http://localhost:53008");
        static IHubProxy hubProxy = hubConnection.CreateHubProxy("Feeds");

        public void StartMainLoop()
        {
            hubConnection.Start();
            SendSupportedItemsList();
            thread.Start();
        }
        public void StopMainLoop()
        {
            thread.Abort();
        }

        static void MainLoop()
        {
            while (true)
            {
                PassOrdersToDb();
                if (MatchAvaibleOrders())
                {
                    SendFeedsToHub();
                }
                Thread.Sleep(2000);
            }
        }

        bool SendSupportedItemsList()
        {
            List<Item> itemList = new List<Item>();
            using (var db = new DbModels.MarketDatabase())
            {
                var query = db.Items.Where(i => i.SupportedBackend == "BAK1");
                foreach (DbModels.ItemEntity itemEntity in query)
                {
                    itemList.Add((Item)itemEntity);
                }
            }
            backendToMiddleQueue.Send(new Message(itemList) { Label = "Supported Items", AppSpecific = 1 },
                MessageQueueTransactionType.Single);
            return true;
        }

        static bool PassOrdersToDb()
        {
            Message[] messages = middleToBackendQueue.GetAllMessages();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            using (var db = new DbModels.MarketDatabase())
            {
                foreach (Message message in messages)
                {
                    try
                    {
                        Order order = (Order)xmlSerializer.Deserialize(message.BodyStream);
                        db.Orders.Add(MapOrderToDbEntity(order, order.type, db.Items.Find(order.item.id)));
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Debug.Write(e.Message);
                        return false;
                    }
                }
            }
            middleToBackendQueue.Purge();
            middleToBackendQueue.Dispose();
            return true;
        }

        static bool MatchAvaibleOrders()
        {
            var transactions = new List<Models.TransactionEntity>();
            using (var db = new DbModels.MarketDatabase())
            {
                var buyOrders = db.Orders.Where(o => o.OrderType == "buy").OrderBy(o => o.Price).ToList();
                var sellOrders = db.Orders.Where(o => o.OrderType == "sell").OrderByDescending(o => o.Price).ToList();
                foreach (DbModels.OrderEntity buyOrder in buyOrders)
                {
                    try
                    {
                        transactions.Add(new Models.TransactionEntity(
                                            buyOrder,
                                            sellOrders.Where(o => o.Price < buyOrder.Price).
                                            First(o => o.ItemID == buyOrder.ItemID),
                                            db));
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

                foreach (Models.TransactionEntity transaction in transactions)
                {
                    db.Feeds.Add(transaction.GenerateFeedEntity());
                }
                db.SaveChanges();
            }
            return true;
        }

        static async void SendFeedsToHub()
        {
            List<Feed> feedList = new List<Feed>();
            using (var db = new DbModels.MarketDatabase())
            {
                var feedEntities = db.Feeds.ToList();
                foreach (DbModels.FeedEntity feedEntity in feedEntities)
                {
                    feedList.Add((Feed)feedEntity);
                    db.Feeds.Remove(feedEntity);
                    db.SaveChanges();
                }
            }
            await hubProxy.Invoke("SendFeeds", feedList);
        }

        private static DbModels.OrderEntity MapOrderToDbEntity(Order order,
            string type, DbModels.ItemEntity baseItem)
        {
            return new DbModels.OrderEntity
            {
                ItemID = order.item.id,
                OrderID = order.orderID,
                Price = order.item.price,
                Quantity = order.item.quantity,
                OrderType = type,
                Item = baseItem
            };
        }


    }
}
