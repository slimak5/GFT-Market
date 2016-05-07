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
namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue messageQueueBAK1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMT = new MessageQueue(@".\private$\bak.to.mt.queue");
        static Thread thread = new Thread(mainLoop);

        public void start()
        {
            sendSupportedItems();
            thread.Start();
        }
        public void stop()
        {
            thread.Abort();
        }

        static void mainLoop()
        {
            while (true)
            {
                Thread.Sleep(1000);
                processOrders();
                matchOrders();
                sendFeeds();
            }
        }
        static void processOrders()
        {
            Message[] messages = messageQueueBAK1.GetAllMessages();
            messageQueueBAK1.Purge();
            using (var db = new DBModels.MarketDatabase())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
                foreach (Message message in messages)
                {
                    try
                    {
                        Order order = (Order)xmlSerializer.Deserialize(message.BodyStream);
                        db.Orders.Add(mapOrderToDB(order, order.type, db.Items.Find(order.item.id)));
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

        void sendSupportedItems()
        {
            List<Item> itemList = new List<Item>();
            using (var db = new DBModels.MarketDatabase())
            {
                var query = db.Items.Where(i => i.SupportedBackend == "BAK1"); //from b in db.Items where b.SupportedBackend.Equals("BAK1") select b;
                foreach (DBModels.Item item in query)
                {
                    itemList.Add((Item)item);
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
        static void sendFeeds()
        {
            //TODO: change to websocket
            List<Feed> feedList = new List<Feed>();
            using (var db = new DBModels.MarketDatabase())
            {
                var feedDBList = db.Feeds.ToList();

                foreach (DBModels.Feed feed in feedDBList)
                {
                    feedList.Add((Feed)feed);
                    db.Feeds.Remove(feed);
                    db.SaveChanges();
                }

            }
            Message m = new Message(feedList);
            m.Label = "latest feeds";
            m.AppSpecific = 2;
            messageQueueMT.Send(m, MessageQueueTransactionType.Single);

        }
        static void matchOrders()
        {
            using (var db = new DBModels.MarketDatabase())
            {
                List<DBModels.Order> buyOrderList = db.Orders.Where(o => o.OrderType == "buy").ToList();
                List<DBModels.Order> sellOrderList = db.Orders.Where(o => o.OrderType == "sell").OrderBy(o => o.Price).ToList();
                foreach (DBModels.Order buyOrder in buyOrderList)
                {
                    DBModels.Order bestSellOrder = new DBModels.Order();
                    try
                    {
                        bestSellOrder = sellOrderList.First(o => o.ItemID == buyOrder.ItemID);

                    }
                    catch (Exception e)
                    {

                    }
                    if (bestSellOrder.Item != null && buyOrder.Item != null)
                    {
                        if (bestSellOrder.Quantity > buyOrder.Quantity)
                        {
                            db.Orders.Remove(bestSellOrder);
                            db.Orders.Remove(buyOrder);
                            bestSellOrder.Quantity -= buyOrder.Quantity;
                            db.Orders.Add(bestSellOrder);

                            DBModels.Feed feedNotification = new DBModels.Feed();
                            try
                            {
                                if (buyOrder.Item != null)
                                {
                                    feedNotification.ItemName = buyOrder.Item.Name;
                                    feedNotification.OperationType = "sell";
                                    feedNotification.Price = bestSellOrder.Price;
                                    feedNotification.Quantity = buyOrder.Quantity;
                                    db.Feeds.Add(feedNotification);
                                }
                            }
                            catch (NullReferenceException e)
                            {
                                break;
                            }
                            db.SaveChanges();
                        }
                        else if (bestSellOrder.Quantity == buyOrder.Quantity)
                        {


                            DBModels.Feed feedNotification = new DBModels.Feed();
                            feedNotification.ItemName = buyOrder.Item.Name;
                            feedNotification.OperationType = "sell";
                            feedNotification.Price = bestSellOrder.Price;
                            feedNotification.Quantity = buyOrder.Quantity;

                            db.Orders.Remove(bestSellOrder);
                            db.Orders.Remove(buyOrder);
                            db.Feeds.Add(feedNotification);
                            db.SaveChanges();
                        }
                        else if (bestSellOrder.Quantity < buyOrder.Quantity)
                        {
                            db.Orders.Remove(bestSellOrder);
                            db.Orders.Remove(buyOrder);
                            buyOrder.Quantity -= bestSellOrder.Quantity;
                            db.Orders.Add(buyOrder);

                            DBModels.Feed feedNotification = new DBModels.Feed();
                            feedNotification.ItemName = buyOrder.Item.Name;
                            feedNotification.OperationType = "sell";
                            feedNotification.Price = bestSellOrder.Price;
                            feedNotification.Quantity = buyOrder.Quantity;

                            db.Feeds.Add(feedNotification);
                            db.SaveChanges();
                        }
                    }
                    bestSellOrder = null;
                }

            }
        }


        private static DBModels.Order mapOrderToDB(Order order, string type, DBModels.Item baseItem)
        {
            DBModels.Order dbOrder = new DBModels.Order();
            dbOrder.ItemID = order.item.id;
            dbOrder.OrderID = order.orderID;
            dbOrder.Price = order.item.price;
            dbOrder.Quantity = order.item.quantity;
            dbOrder.OrderType = type;
            dbOrder.Item = baseItem;
            return dbOrder;
        }


    }
}
