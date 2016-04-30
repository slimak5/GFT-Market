using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Messaging;

namespace GFT.Website.Api.Controllers
{
    public class ItemsController : ApiController
    {
        static List<Models.Item> itemList = new List<Models.Item>();

        static string[] itemListBAK1;
        static string[] itemListBAK2;

        static int idPool = 100;
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string buyItem(Models.Item item)
        {
            Models.Order order = new Models.Order(item, generateID());
            return "Your request has been sent. ID: "+order.orderID; 
        }
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public string sellItem(Models.Item item)
        {
            //TODO Make service call, return response status
            return "Your request has been sent.";
        }
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Item> getItems()
        {
            Models.Item item = new Models.Item();
            item.id = 5;
            item.name = "test";
            //TODO Make service call, return json array of Item
            itemList.Add(item);
            return itemList;//Json<Models.Item>(item);
        }        
        

        int generateID()
        {
            int id = idPool;
            idPool++;
            return id;
        }
    }
}
