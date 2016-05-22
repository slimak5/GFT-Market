using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json;
using System.Messaging;
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

        }
    }
}
