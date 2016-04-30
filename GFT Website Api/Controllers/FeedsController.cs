using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GFT.Website.Api.Controllers
{
    public class FeedsController : ApiController
    {
        static List<Models.Feed> feedList = new List<Models.Feed>();
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Feed> getFeeds()
        {
            
            //TODO Make service call, return json array of Items from backend svc
            Models.Feed item = new Models.Feed();
            feedList.Add(item);
            return feedList;
        }
        
    }
}
