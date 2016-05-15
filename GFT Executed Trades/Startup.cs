using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(GFT.Services.ExecutedTrades.Startup))]

namespace GFT.Services.ExecutedTrades

{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.MapSignalR(new HubConfiguration() { EnableJSONP = true });
        }

    }
}
