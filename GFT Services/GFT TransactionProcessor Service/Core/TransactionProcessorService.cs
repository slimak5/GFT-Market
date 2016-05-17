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
        static MessageQueue messageQueueBAK1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMT = new MessageQueue(@".\private$\bak.to.mt.queue");
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
                Thread.Sleep(1000);
                PassOrdersToDb();
                MatchAvaibleOrders();
                SendFeedsToHub();
            }
        }
        static void PassOrdersToDb()
        {
            Message[] messages = messageQueueBAK1.GetAllMessages();
            messageQueueBAK1.Purge();
            using (var db = new DbModels.MarketDatabase())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
                foreach (Message message in messages)
                {
                    try
                    {
                        Order order = (Order)xmlSerializer.Deserialize(message.BodyStream);
                        db.Orders.Add(MapOrderToDatabaseEntity(order, order.type, db.Items.Find(order.item.id)));
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            messageQueueBAK1.Dispose();
        }

        void SendSupportedItemsList()
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

            try
            {
                Message message = new Message(itemList);
                message.Label = "Supported Items";
                message.AppSpecific = 1;
                messageQueueMT.Send(message, MessageQueueTransactionType.Single);
            }
            catch (InvalidOperationException e)
            {
                Debug.Write(e.InnerException);
            }

        }
        static void SendFeedsToHub()
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
            hubProxy.Invoke("SendFeeds", feedList);
        }

        static List<Models.Transaction> MatchAvaibleOrders()
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();
            using (var db = new DbModels.MarketDatabase())
            {
                var buyOrders = db.Orders.Where(o => o.OrderType == "buy").OrderBy(o => o.Price).ToList();
                var sellOrders = db.Orders.Where(o => o.OrderType == "sell").OrderByDescending(o => o.Price).ToList();
                for (int i = 0; i < buyOrders.Count; i++)
                {
                    for (int j = 0; j < buyOrders.Count; j++)
                    {
                        if (buyOrders[i].ItemID == sellOrders[j].ItemID)
                        {
                            Models.Transaction transaction = new Models.Transaction(buyOrders[i], sellOrders[j]);
                            transactions.Add(transaction);
                            db.Feeds.Add(transaction.GenerateFeedEntity());
                            transaction.RemoveFromDatabase(db);
                            db.SaveChanges();
                        }
                    }


                }
            }
            return transactions;
        }


        private static DbModels.OrderEntity MapOrderToDatabaseEntity(Order order, string type, DbModels.ItemEntity baseItem)
        {
            DbModels.OrderEntity orderEntity = new DbModels.OrderEntity();
            orderEntity.ItemID = order.item.id;
            orderEntity.OrderID = order.orderID;
            orderEntity.Price = order.item.price;
            orderEntity.Quantity = order.item.quantity;
            orderEntity.OrderType = type;
            orderEntity.Item = baseItem;
            return orderEntity;
        }


    }
}
