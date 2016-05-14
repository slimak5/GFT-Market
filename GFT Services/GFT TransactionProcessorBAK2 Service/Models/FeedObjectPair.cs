using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Services.TransactionProcessor.Models
{
    public class FeedObject
    {
        DBModels.Order buyOrder { get; set; }
        DBModels.Order sellOrder { get; set; }
        public FeedObject(DBModels.Order buyOrder, DBModels.Order sellOrder)
        {
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
        }

        public DBModels.Feed generateFeed()
        {
            if (buyOrder.Item != null && sellOrder.Item != null)
            {
                DBModels.Feed feed = new DBModels.Feed();
                feed.ItemName = buyOrder.Item.Name;
                feed.OperationType = "sell";
                feed.Price = sellOrder.Price;
                if (buyOrder.Quantity > sellOrder.Quantity)
                {
                    feed.Quantity = sellOrder.Quantity;
                }
                else
                {
                    feed.Quantity = buyOrder.Quantity;
                }
                return feed;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void cleanFromDB(DBModels.MarketDatabase database)
        {
            using (database)
            {
                database.Orders.Remove(buyOrder);
                database.Orders.Remove(sellOrder);
                database.SaveChanges();
            }
        }
    }
}
