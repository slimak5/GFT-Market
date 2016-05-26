using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GFT.Services.TransactionProcessor.Test.TransactionProcessorService1;
using System.Threading;

namespace GFT.Services.TransactionProcessor.Test
{
    [TestClass]
    public class TransactionProcessorService1Tests
    {
        TransactionProcessorClient _TransactionProcessorClient;

        [TestMethod]
        public void ServiceCanManageItsThread()
        {
            _TransactionProcessorClient = new TransactionProcessorClient();

            _TransactionProcessorClient.StartMainLoop();
            Assert.AreEqual(ThreadState.Running, _TransactionProcessorClient.GetWorkerThreadState());

            _TransactionProcessorClient.StopMainLoop();
            _TransactionProcessorClient.StartMainLoop();
            Assert.AreEqual(ThreadState.Running, _TransactionProcessorClient.GetWorkerThreadState());

            _TransactionProcessorClient.StopMainLoop();
            Assert.AreEqual(ThreadState.Unstarted, _TransactionProcessorClient.GetWorkerThreadState());
        }
    }
}
