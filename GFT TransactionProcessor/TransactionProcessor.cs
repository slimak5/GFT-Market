using GFT.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GFT.Services.TransactionProcessor.Core
{
    public class TransactionProcessor
    {
        private MessageQueue _MiddleToBackendQueue;
        private MessageQueue _BackendToMiddleQueue;
        private HubConnection _HubConnection;
        private IHubProxy _HubProxy;
        private string _BackendId { get; set; }


        private List<Order> _BuyOrders;
        private List<Order> _SellOrders;
        public List<Transaction> FinalizedTransactions;

        public TransactionProcessor(string queueStringToFetchData, string hubConnection,
            string hubName, string backendId)
        {
            _MiddleToBackendQueue = new MessageQueue(queueStringToFetchData);
            _BackendToMiddleQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
            _HubConnection = new HubConnection(hubConnection);
            _HubProxy = _HubConnection.CreateHubProxy(hubName);
            _BackendId = backendId;

            _BuyOrders = new List<Order>();
            _SellOrders = new List<Order>();
            FinalizedTransactions = new List<Transaction>();

            _HubConnection.Start();
        }

        public void FetchMessagesFromQueue()
        {
            XmlSerializer _XMLSerializer = new XmlSerializer(typeof(Order));
            using (_MiddleToBackendQueue)
            {
                _MiddleToBackendQueue.MessageReadPropertyFilter.AppSpecific = true;

                foreach (Message m in _MiddleToBackendQueue.GetAllMessages())
                {
                    if (m.AppSpecific == 1)
                    {
                        _BuyOrders.Add((Order)_XMLSerializer.Deserialize(m.BodyStream));
                        _MiddleToBackendQueue.ReceiveById(m.Id);
                    }

                    if (m.AppSpecific == 2)
                    {
                        _SellOrders.Add((Order)_XMLSerializer.Deserialize(m.BodyStream));
                        _MiddleToBackendQueue.ReceiveById(m.Id);
                    }
                }
            }
        }

        public void PushMessageToQueue(IEnumerable<Item> itemList)
        {
            using (var message = new Message(itemList))
            {
                message.AppSpecific = 1;
                _BackendToMiddleQueue.Send(message, MessageQueueTransactionType.Single);
            }
        }

        public void CreateTransactionsWhenAvaible()
        {
            if (_BuyOrders == null || _SellOrders == null)
            {
                RemoveEmptyOrders();
                return;
            }

            var looping = true;
            while (looping)
            {
                try
                {
                    int maxBuyPrice = _BuyOrders.Max(o => o.price);

                    var buyOrder = _BuyOrders.Find(o => o.price == maxBuyPrice);
                    var sellOrder = _SellOrders.OrderBy(o => o.price)
                        .FirstOrDefault(o => o.item.itemId == buyOrder.item.itemId);

                    if (buyOrder == null || sellOrder == null)
                    {
                        RemoveEmptyOrders();
                        return;
                    }

                    var transaction = Transaction.GenerateTransactionObject(sellOrder, buyOrder);

                    FinalizedTransactions.Add(transaction);
                    using (var db = new Database.GFTMarketDatabaseAccessObject(new Database.GFTMarketDatabase()))
                    {
                        db.Insert(transaction);
                    }
                }
                catch
                {
                    looping = false;
                }
                finally
                {
                    RemoveEmptyOrders();
                }
            }
        }

        private void RemoveEmptyOrders()
        {
            List<Order> buyOrdersToDelete = new List<Order>();
            List<Order> sellOrdersToDelete = new List<Order>();

            foreach (var order in _BuyOrders)
            {
                if (order.quantity == 0 || order.price == 0)
                    buyOrdersToDelete.Add(order);
            }

            foreach (var order in _SellOrders)
            {
                if (order.quantity == 0 || order.price == 0)
                    sellOrdersToDelete.Add(order);
            }

            foreach (var order in buyOrdersToDelete)
            {
                _BuyOrders.Remove(order);
            }

            foreach (var order in sellOrdersToDelete)
            {
                _SellOrders.Remove(order);
            }
        }

        public void PushTransactionsToHub()
        {
            if (FinalizedTransactions == null || FinalizedTransactions.Count == 0) return;

            if (_HubConnection.State == ConnectionState.Connected)
            {
                _HubProxy.Invoke("SendNewTransactionInfo", FinalizedTransactions);
            }
            else
            {
                _HubConnection.Start();

                if (_HubConnection.State == ConnectionState.Connected)
                    _HubProxy.Invoke("SendNewTransactionInfo", FinalizedTransactions);
            }

            FinalizedTransactions = new List<Transaction>();
        }
    }
}
