using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Services.TransactionProcessor.Models
{
    public class Transaction
    {
        DbModels.OrderEntity buyOrder { get; set; }
        DbModels.OrderEntity sellOrder { get; set; }
        public Transaction(DbModels.OrderEntity buyOrder, DbModels.OrderEntity sellOrder)
        {
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
        }

        public DbModels.FeedEntity GenerateFeedEntity()
        {
            if (buyOrder.Item != null && sellOrder.Item != null)
            {
                DbModels.FeedEntity feedEntity = new DbModels.FeedEntity();
                feedEntity.ItemName = buyOrder.Item.Name;
                feedEntity.OperationType = "sell";
                feedEntity.Price = sellOrder.Price;
                if (buyOrder.Quantity > sellOrder.Quantity)
                {
                    feedEntity.Quantity = sellOrder.Quantity;
                }
                else
                {
                    feedEntity.Quantity = buyOrder.Quantity;
                }
                return feedEntity;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void RemoveFromDatabase(DbModels.MarketDatabase database)
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
