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

namespace GFT.Website.Api.Controllers
{
    public class ItemsController : ApiController
    {
        static List<Models.Item> itemList = new List<Models.Item>();
        static MessageQueue messageQueueBAK1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue messageQueueMT = new MessageQueue(@".\private$\bak.to.mt.queue");


        static int idPool = 100;
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string buyItem(Models.Item item)
        {

            Models.Order order = new Models.Order(item, generateID(), "buy");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            using (Message m = new Message())
            {
                m.Body = order;
                messageQueueBAK1.Send(m, order.orderID.ToString(), MessageQueueTransactionType.Single);
                return "Your buy request has been sent. ID: " + order.orderID;
            }


        }
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string sellItem(Models.Item item)
        {
            Models.Order order = new Models.Order(item, generateID(), "sell");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            using (Message m = new Message())
            {
                m.Body = order;
                messageQueueBAK1.Send(m, order.orderID.ToString(), MessageQueueTransactionType.Single);
                return "Your sell request has been sent. ID: " + order.orderID;
            }
        }
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Item> getItems()
        {
            messageQueueMT.MessageReadPropertyFilter.AppSpecific = true;
            Message[] messages = messageQueueMT.GetAllMessages();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Models.Item>));
            foreach (Message message in messages)
            {

                if (message.AppSpecific == 1)
                {
                    messageQueueMT.ReceiveById(message.Id);
                    itemList.AddRange((List<Models.Item>)xmlSerializer.Deserialize(message.BodyStream));
                    
                }
            }
            messageQueueMT.Dispose();
            return itemList;
        }


        int generateID()
        {
            int id = idPool;
            idPool++;
            return id;
        }
    }
}
