using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GFT.Website.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            TransactionProcessorService1.TransactionProcessorClient transactionProcessorClient = new TransactionProcessorService1.TransactionProcessorClient();
            //TransactionProcessorBAK2.TransactionProcessorClient TPClient2 = new TransactionProcessorBAK2.TransactionProcessorClient();
            try
            {
                transactionProcessorClient.StartMainLoop();
            }
            catch (Exception e)
            {

            }
        }
    }
}
