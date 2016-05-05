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
using System.Timers;
using System.Diagnostics;
namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue messageQueueBAK1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMT = new MessageQueue(@".\private$\bak.to.mt.queue");
        static int loopState = 1;

        public void start()
        {
            sendSupportedItems();
            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(mainLoop);
            timer.Enabled = true;
        }

        public void stop()
        {
            loopState = 0;
            
        }

        
    void mainLoop(object sender, ElapsedEventArgs a)
        {
            Debug.Write("loop");
            while (loopState == 1)
            {
                Debug.Write("loop");
                //processOrders();
                //matchOrders();
                //sendFeeds();
            }
        }

        void processOrders()
        {
            Message[] messages = messageQueueBAK1.GetAllMessages();
            using (var db = new DBModels.MarketContext())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
                foreach (Message message in messages)
                {
                    Order order = (Order)xmlSerializer.Deserialize(message.BodyStream);
                    messageQueueBAK1.ReceiveById(message.Id);
                    DBModels.Order dbOrder = mapOrderToDB(order, order.type, db.Items.Find(order.item.id));
                    db.Orders.Add(dbOrder);
                    db.SaveChanges();
                }
            }
            messageQueueBAK1.Dispose();
        }

        void sendSupportedItems()
        {
            List<Item> itemList = new List<Item>();
            using (var db = new DBModels.MarketContext())
            {
                var query = db.Items.Where(i => i.SupportedBackend == "BAK1"); //from b in db.Items where b.SupportedBackend.Equals("BAK1") select b;
                foreach (DBModels.Item item in query)
                {
                    itemList.Add((Item)item);
                }
            }
            Message m = new Message(itemList);
            m.Label = "supported items";
            m.AppSpecific = 1;
            messageQueueMT.Send(m, MessageQueueTransactionType.Single);

        }

        void sendFeeds()
        {
            //TODO: change to websocket
            List<Feed> feedList = new List<Feed>();
            using (var db = new DBModels.MarketContext())
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
        void matchOrders()
        {
            //using (var db = new DBModels.MarketContext())
            //{

            //}

            using (var db = new DBModels.MarketContext())
            {
                var buyOrderList = db.Orders.Where(o => o.OrderType == "buy").ToList();
                var sellOrderList = db.Orders.Where(o => o.OrderType == "sell").ToList();

                foreach (DBModels.Order buyorder in buyOrderList)
                {
                    DBModels.Order minSellOrder = new DBModels.Order();
                    if (sellOrderList.Count > 0)
                    {
                        minSellOrder = sellOrderList.First(o => o.ItemID == buyorder.ItemID);
                    }

                    foreach (DBModels.Order sellorder in sellOrderList)
                    {
                        if (sellorder.Price < minSellOrder.Price && sellorder.Price < buyorder.Price && buyorder.ItemID == sellorder.ItemID)
                        {
                            minSellOrder = sellorder;
                        }
                    }

                    DBModels.Feed feed = new DBModels.Feed();
                    if (minSellOrder.Item != null)
                    {
                        feed.ItemName = minSellOrder.Item.Name;
                        feed.OperationType = minSellOrder.OrderType;
                        feed.Price = minSellOrder.Price;
                        feed.Quantity = minSellOrder.Quantity;
                        db.Feeds.Add(feed);
                        db.Orders.Remove(buyorder);
                        db.Orders.Remove(minSellOrder);
                        db.SaveChanges();
                    }

                }

            }
        }


        private DBModels.Order mapOrderToDB(Order order, string type, DBModels.Item baseItem)
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

        public static void Configure()
        {


        }

    }
}
