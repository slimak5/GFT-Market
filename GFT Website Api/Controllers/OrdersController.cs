using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Messaging;
using System.Xml.Serialization;
using GFT.Models;

namespace GFT.Website.Api.Controllers
{
    public class OrdersController : ApiController
    {
        static MessageQueue messageQueueMtToBak1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMtToBak2 = new MessageQueue(@".\private$\mt.to.bak2.queue");
        static MessageQueue messageQueueBakToMt = new MessageQueue(@".\private$\bak.to.mt.queue");


        static int orderIdPool = 100;
        static int webclientIdPool = 100;

        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string SendBuyOrder(Models.Order item)
        {
            //Models.Order order = new Order(); //TODO FIX
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //using (Message m = new Message(order))
            //{
            //    try
            //    {
            //        if (order.item.itemId >= 200)
            //        {
            //            messageQueueBAK2.Send(m, order.orderId.ToString(), MessageQueueTransactionType.Single);
            //            return "Your buy request has been sent. ID: " + order.transactionId;
            //        }
            //        else
            //        {
            //            messageQueueBAK1.Send(m, order.transactionId.ToString(), MessageQueueTransactionType.Single);
            //            return "Your buy request has been sent. ID: " + order.transactionId;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        return e.Message;
            //    }
            return "Need to be implemented";
        }

        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string sellItem(Models.Order item)
        {
            //Models.Transaction order = new Models.Item(item, generateID(), "sell");
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //using (Message m = new Message())
            //{
            //    m.Body = order;
            //    try
            //    {
            //        if (order.item.id >= 200)
            //        {
            //            messageQueueBAK2.Send(m, order.transactionId.ToString(), MessageQueueTransactionType.Single);
            //            return "Your sell request has been sent. ID: " + order.transactionId;
            //        }
            //        else
            //        {
            //            messageQueueBAK1.Send(m, order.transactionId.ToString(), MessageQueueTransactionType.Single);
            //            return "Your sell request has been sent. ID: " + order.transactionId;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        return e.Message;
            //    }
            //}
            return "Need to be implemented";
        }
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Order> GetItems() 
        {
            messageQueueBakToMt.MessageReadPropertyFilter.AppSpecific = true;

            List<Order> orderList = new List<Order>();
            List<Item> itemList = new List<Item>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));
            var messages = messageQueueBakToMt.GetAllMessages();
            foreach(var message in messages)
            {
                if(message.AppSpecific == 1)
                {
                    messageQueueBakToMt.ReceiveById(message.Id);
                    itemList.AddRange((List<Item>)xmlSerializer.Deserialize(message.BodyStream));
                }
            }

            foreach(var item in itemList)
            {
                orderList.Add(new Order(item));
            }
            return orderList;
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public int GenerateOrderId()
        {
            orderIdPool++;
            return orderIdPool;
        }


    }
}
