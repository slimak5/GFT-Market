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
    //TODO: skonczyc implementacje, dopisac matcher metode, dokonczyc webapi aby przesylalo do mq 
    public class TransactionProcessor
    {
        private MessageQueue _MiddleToBackendQueue;
        private MessageQueue _BackendToMiddleQueue;
        private HubConnection _HubConnection;
        private IHubProxy _HubProxy;
        private string BackendId { get; set; }

        private List<Models.Order> BuyOrders;
        private List<Models.Order> SellOrders;

        public TransactionProcessor(string middleToBackendQueueString, string hubConnection,
            string hubName, string backendId)
        {
            _MiddleToBackendQueue = new MessageQueue(middleToBackendQueueString);
            _BackendToMiddleQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
            _HubConnection = new HubConnection(hubConnection);
            _HubProxy = _HubConnection.CreateHubProxy(hubName);
            BackendId = backendId;
        }

        private void FetchMessagesFromQueue()
        {
            XmlSerializer _XMLSerializer = new XmlSerializer(typeof(Models.Order));

            using (_MiddleToBackendQueue)
            {
                foreach (Message m in _MiddleToBackendQueue.GetAllMessages())
                {
                    if (m.AppSpecific == 1)
                        BuyOrders.Add((Models.Order)_XMLSerializer.Deserialize(m.BodyStream));

                    if (m.AppSpecific == 2)
                        SellOrders.Add((Models.Order)_XMLSerializer.Deserialize(m.BodyStream));
                }
            }
        }
    }
}
