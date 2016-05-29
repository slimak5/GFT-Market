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
using GFT.Models;

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorService2 : ITransactionProcessor
    {

        private static Thread _WorkerThread;
        private static CancellationTokenSource _CancellationToken;
        private Core.TransactionProcessor _TransactionProcessor;


        public TransactionProcessorService2()
        {
            _TransactionProcessor = new Core.TransactionProcessor(@".\private$\mt.to.bak2.queue",
                "http://localhost:53008", "ExecutedTransactionsHub", "BAK2");
        }

        public void StartMainLoop()
        {

            lock (this)
            {
                if (_WorkerThread == null)
                {
                    SendSupportedItems();

                    _WorkerThread = new Thread(MainLoop);
                    _WorkerThread.Start();
                }
            }
        }

        public void StopMainLoop()
        {
            if (_CancellationToken.Token.CanBeCanceled)
                _CancellationToken.Cancel();
        }

        void MainLoop()
        {
            _CancellationToken = new CancellationTokenSource();
            try
            {
                while (!_CancellationToken.IsCancellationRequested)
                {
                    _TransactionProcessor.FetchMessagesFromQueue();
                    _TransactionProcessor.CreateTransactionsWhenAvaible();
                    _TransactionProcessor.PushTransactionsToHub();
                }
            }
            finally
            {
                _WorkerThread = null;
            }
        }

        private void SendSupportedItems()
        {
            using (var db = new GFTMarketDatabaseAccessObject(new GFTMarketDatabase()))
            {
                List<Item> supportedItems = db.GetSupportedItemList("BAK2");
                _TransactionProcessor.PushMessageToQueue(supportedItems);
            }
        }

        public System.Threading.ThreadState GetWorkerThreadState()
        {
            if (_WorkerThread != null)
                return _WorkerThread.ThreadState;

            return System.Threading.ThreadState.Unstarted;
        }
    }
}