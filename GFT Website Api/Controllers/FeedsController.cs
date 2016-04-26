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
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Feed> getFeeds()
        {
            List<Models.Feed> feedList = new List<Models.Feed>();
            //TODO Make service call, return json array of Item
            
            return feedList;//Json<Models.Item>(item);
        }
        
    }
}
