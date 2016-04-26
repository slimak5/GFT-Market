using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace GFT.Website.Api.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        [EnableCors("*","*","*")]
        public List<Models.Item> getItems() {
            List<Models.Item> itemList = new List<Models.Item>();

            //TODO Make service call, return json array of Item
            
            return itemList;//Json<Models.Item>(item);
        }
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string sellItem(string item)
        {
            //Models.Item Object = new JavaScriptSerializer().Deserialize<Models.Item>(item);

            //TODO Make service call, return response status
            return item;
        }
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string buyItem(string item)
        {
            //Models.Item Object = new JavaScriptSerializer().Deserialize<Models.Item>(item);

            //TODO Make service call, return response status
            return item;
        }
    }
}
