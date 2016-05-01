using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFT.Website.Api.Models
{
    public class Order
    {
        public Item item { get; set; }
        public int orderID { get; set; }
        public string type { get; set;}

        public Order() {
            item = new Item();
            orderID = -1;
            type = "n/a";
        }

        public Order(Item item, int orderID, string type)
        {
            this.item = item;
            this.orderID = orderID;
            this.type = type;
        }
    }
}