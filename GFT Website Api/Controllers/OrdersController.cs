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
        private static MessageQueue _MiddleToBackendQueue1 = new MessageQueue(@".\private$\mt.to.bak1.queue");
        private static MessageQueue _MiddleToBackendQueue2 = new MessageQueue(@".\private$\mt.to.bak2.queue");
        private static MessageQueue _BackendToMiddleQueue = new MessageQueue(@".\private$\bak.to.mt.queue");

        private static List<Order> _OrderList = new List<Order>();
        private static List<Item> _ItemList = new List<Item>();

        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string SendBuyOrder(Order order)
        {
            using (var message = new Message(order)
            { AppSpecific = 1, Label = string.Format("Buy Order:{0}", order.orderId) })
            {
                if (order.item.supportedServiceId == "BAK1")
                {
                    _MiddleToBackendQueue1.Send(message, MessageQueueTransactionType.Single);
                }

                if (order.item.supportedServiceId == "BAK2")
                {
                    _MiddleToBackendQueue2.Send(message, MessageQueueTransactionType.Single);
                }

            }

            return String.Format("Your order have been sent. Order ID: {0}", order.orderId);
        }

        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string SendSellOrder(Order order)
        {
            using (var message = new Message(order)
            { AppSpecific = 2, Label = string.Format("Sell Order:{0}", order.orderId) })
            {
                if (order.item.supportedServiceId == "BAK1")
                {
                    _MiddleToBackendQueue1.Send(message, MessageQueueTransactionType.Single);
                }

                if (order.item.supportedServiceId == "BAK2")
                {
                    _MiddleToBackendQueue2.Send(message, MessageQueueTransactionType.Single);
                }
            }
            return String.Format("Your order have been sent. Order ID: {0}", order.orderId);
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Order> GetItems()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));

            _BackendToMiddleQueue.MessageReadPropertyFilter.AppSpecific = true;
            foreach (var message in _BackendToMiddleQueue.GetAllMessages())
            {
                if (message.AppSpecific == 1)
                {
                    _BackendToMiddleQueue.ReceiveById(message.Id);
                    _ItemList.AddRange((List<Item>)xmlSerializer.Deserialize(message.BodyStream));
                }
            }

            foreach (var item in _ItemList)
            {
                _OrderList.Add(new Order(item) {orderId = GenerateOrderId() });
            }
            return _OrderList;
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public int GenerateOrderId()
        {
            return new Random().Next(10000, 99999);
        }


    }
}
