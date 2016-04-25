using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace GFT.Website.Api.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpPost]
        public string getItems() {
            //TODO Make service call, return json array of Item
            return "ok";
        }
        [HttpGet]
        public string sellItem(string item)
        {
            //Models.Item Object = new JavaScriptSerializer().Deserialize<Models.Item>(item);

            //TODO Make service call, return response status
            return item;
        }
    }
}
