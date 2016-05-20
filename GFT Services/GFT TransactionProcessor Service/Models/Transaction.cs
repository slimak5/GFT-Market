using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Services.TransactionProcessor.Models
{
    public class TransactionEntity
    {
       private DbModels.OrderEntity buyOrder { get; set; }
       private DbModels.OrderEntity sellOrder { get; set; }
       private DbModels.MarketDatabase database { get; set; }
        public TransactionEntity(DbModels.OrderEntity buyOrder, DbModels.OrderEntity sellOrder)
        {
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
        }

        public TransactionEntity(DbModels.OrderEntity buyOrder,
            DbModels.OrderEntity sellOrder, DbModels.MarketDatabase database)
        {
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
            this.database = database;
        }

        public DbModels.FeedEntity GenerateFeedEntity()
        {
            if (buyOrder.Item != null && sellOrder.Item != null)
            {
                DbModels.FeedEntity feedEntity = new DbModels.FeedEntity()
                {
                    ItemName = buyOrder.Item.Name,
                    OperationType = "sell",
                    Price = sellOrder.Price
                };
                if (buyOrder.Quantity > sellOrder.Quantity)
                {
                    feedEntity.Quantity = sellOrder.Quantity;
                    database.Orders.Find(buyOrder.Id).Quantity -= sellOrder.Quantity;
                    database.Orders.Remove(sellOrder);
                }
                else if(buyOrder.Quantity < sellOrder.Quantity)
                {
                    feedEntity.Quantity = buyOrder.Quantity;
                    database.Orders.Find(buyOrder.Id).Quantity -= sellOrder.Quantity;
                    database.Orders.Remove(buyOrder);
                }
                else
                {
                    feedEntity.Quantity = buyOrder.Quantity;
                    database.Orders.Remove(sellOrder);
                    database.Orders.Remove(buyOrder);
                }
                database.SaveChanges();
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
