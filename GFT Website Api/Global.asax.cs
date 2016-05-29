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

            TransactionProcessorService1.TransactionProcessorClient TransactionProcessorService1 = new TransactionProcessorService1.TransactionProcessorClient();

            if (TransactionProcessorService1.State != System.ServiceModel.CommunicationState.Opening ||
                TransactionProcessorService1.State != System.ServiceModel.CommunicationState.Opened)
            {
                TransactionProcessorService1.Open();
            }

            if (TransactionProcessorService1.GetWorkerThreadState() != ThreadState.Running)
                TransactionProcessorService1.StartMainLoop();
        }
    }
}
