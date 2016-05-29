using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace GFT.Services.TransactionProcessor.Test
{
    [TestClass]
    public class TransactionProcessorService1Tests
    {
        TransactionProcessorService1 _TransactionProcessor;

        [TestMethod]
        public void ServiceCanManageItsThread()
        {
            _TransactionProcessor = new TransactionProcessorService1();

            _TransactionProcessor.StartMainLoop();
            Thread.Sleep(100);
            Assert.AreEqual(ThreadState.Running, _TransactionProcessor.GetWorkerThreadState());

            _TransactionProcessor.StopMainLoop();
            _TransactionProcessor.StartMainLoop();
            Thread.Sleep(100);
            Assert.AreEqual(ThreadState.Running, _TransactionProcessor.GetWorkerThreadState());

            _TransactionProcessor.StopMainLoop();
            Thread.Sleep(2000);
            Assert.AreEqual(ThreadState.Stopped, _TransactionProcessor.GetWorkerThreadState());
        }
    }
}
