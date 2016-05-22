using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Threading;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;
using GFT.Database;
namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue middleToBackendQueue = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static MessageQueue backendToMiddleQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
        static Thread thread = new Thread(MainLoop);
        static HubConnection hubConnection = new HubConnection("http://localhost:53008");
        static IHubProxy hubProxy = hubConnection.CreateHubProxy("Feeds");

        public Models.Item StartMainLoop()
        {
            //hubConnection.Start();
            return SendSupportedItemsList();
            //thread.Start();
        }

        private Models.Item SendSupportedItemsList()
        {
            
            GFTMarketDatabaseInstance database = new GFTMarketDatabaseInstance();
            database.Insert(new Models.Item { itemId = 500, itemName = "tescik", supportedServiceId = "BAK5" });
            return database.Read<Models.Item>(1);
        }

        public void StopMainLoop()
        {
            thread.Abort();
        }
        static void MainLoop()
        {
            while (true)
            {

            }
        }
    }
}