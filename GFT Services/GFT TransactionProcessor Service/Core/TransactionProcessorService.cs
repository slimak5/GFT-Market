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

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue messageQueueBAK1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMT = new MessageQueue(@".\private$\bak.to.mt.queue");

        public void processOrders()
        {
            Message[] messages = messageQueueBAK1.GetAllMessages();
            using (var db = new DBModels.MarketContext())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
                foreach (Message message in messages)
                {
                    Order order = (Order)xmlSerializer.Deserialize(message.BodyStream);
                    DBModels.Order dbOrder = mapOrderToDB(order, order.type, db.Items.Find(order.item.id));
                    db.Orders.Add(dbOrder);
                    db.SaveChanges();


                }
            }
            messageQueueBAK1.Purge();
            messageQueueBAK1.Dispose();
        }

        public void sendSupportedItems()
        {
            List<Item> itemList = new List<Item>();
            using (var db = new DBModels.MarketContext())
            {
                var query = from b in db.Items where b.SupportedBackend.Equals("BAK1") select b;

                foreach(DBModels.Item item in query)
                {
                    itemList.Add((Item)item);
                }
            }
            Message m = new Message(itemList);
            messageQueueMT.Send(m, MessageQueueTransactionType.Single);

        }
        public void matchOrders()
        {
            //TODO
        }


        private DBModels.Order mapOrderToDB(Order order, string type, DBModels.Item baseItem)
        {
            DBModels.Order dbOrder = new DBModels.Order();
            dbOrder.ItemID = order.item.id;
            dbOrder.OrderID = order.orderID.ToString();
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
