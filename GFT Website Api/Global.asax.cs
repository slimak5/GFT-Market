using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json;
using System.Messaging;
using System.Threading;

namespace GFT.Website.Api
{
    public class WebApiApplication : HttpApplication
    {
        static TransactionProcessorService1.TransactionProcessorClient TransactionProcessorService1;
        static TransactionProcessorService2.TransactionProcessorClient TransactionProcessorService2;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            if (!MessageQueue.Exists(@".\private$\mt.to.bak1.queue"))
            {
                MessageQueue.Create(@".\private$\mt.to.bak1.queue", true);
            }

            if (!MessageQueue.Exists(@".\private$\mt.to.bak2.queue"))
            {
                MessageQueue.Create(@".\private$\mt.to.bak2.queue", true);
            }

            if (!MessageQueue.Exists(@".\private$\bak.to.mt.queue"))
            {
                MessageQueue.Create(@".\private$\bak.to.mt.queue", true);
            }

            if (TransactionProcessorService1 == null)
                TransactionProcessorService1 = new TransactionProcessorService1.TransactionProcessorClient();

            if (TransactionProcessorService1.State != System.ServiceModel.CommunicationState.Opening ||
                TransactionProcessorService1.State != System.ServiceModel.CommunicationState.Opened)
            {
                TransactionProcessorService1.Open();
            }

            if (TransactionProcessorService1.GetWorkerThreadState() != ThreadState.Running)
                TransactionProcessorService1.StartMainLoop();


            if (TransactionProcessorService2 == null)
                TransactionProcessorService2 = new TransactionProcessorService2.TransactionProcessorClient();

            if (TransactionProcessorService2.State != System.ServiceModel.CommunicationState.Opening ||
                TransactionProcessorService2.State != System.ServiceModel.CommunicationState.Opened)
            {
                TransactionProcessorService2.Open();
            }

            if (TransactionProcessorService2.GetWorkerThreadState() != ThreadState.Running)
                TransactionProcessorService2.StartMainLoop();
        }
    }
}
