using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Database
{
    public class GFTMarketDatabaseInstance : Core.DataAccessObject<GFTMarketDatabase>
    {
        public GFTMarketDatabaseInstance(GFTMarketDatabase database)
            : base(database)
        {
        }

        public override void Delete<Entity>(Entity dbObject)
        {
            if (dbObject.GetType().Name == "Transaction")
            {
                _database.Transactions.Remove(dbObject.GetInstance<Models.Transaction>());
                _database.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public override void Insert<Entity>(Entity dbObject)
        {
            switch (dbObject.GetType().Name)
            {
                case "Transaction":
                    _database.Transactions.Add(dbObject.GetInstance<Models.Transaction>());
                    _database.SaveChanges();
                    break;
                case "Item":
                    _database.Items.Add(dbObject.GetInstance<Models.Item>());
                    _database.SaveChanges();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public override Entity Read<Entity>(int entityId)
        {
            switch (typeof(Entity).Name)
            {
                case "Transaction":
                    return (Entity)Convert.ChangeType(_database.Transactions.Find(entityId),
                        typeof(Models.Transaction));

                case "Item":
                    return (Entity)Convert.ChangeType(_database.Items.Find(entityId),
                        typeof(Models.Item));
                default:
                    throw new InvalidOperationException();
            }
        }

        public override void Update<Entity>(Entity dbObject)
        {
            switch (typeof(Entity).Name)
            {
                case "Transaction":
                    _database.Transactions.Remove(_database.Transactions
                        .Find(dbObject.GetInstance<Models.Transaction>().transactionId));
                    _database.Transactions.Add(dbObject.GetInstance<Models.Transaction>());
                    break;

                case "Item":
                    _database.Items.Remove(_database.Items
                        .Find(dbObject.GetInstance<Models.Item>().itemId));
                    _database.Items.Add(dbObject.GetInstance<Models.Item>());
                    break;

                default:
                    throw new InvalidOperationException();

            }
        }
    }
}
