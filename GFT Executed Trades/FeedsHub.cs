using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GFT.Website.Api.Models;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace GFT.Services.ExecutedTrades
{
    [HubName("Feeds")]
    public class FeedsHub : Hub
    {
        public void SendFeeds(List<Feed> feedList)
        {
            if (feedList.Count > 0)
                foreach (Feed feed in feedList)
                {
                    Clients.All.SendFeed(feed);
                }
        }
    }
}