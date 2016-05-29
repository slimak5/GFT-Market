using GFT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Database
{
    public class GFTMarketDatabaseAccessObject : Core.DataAccessObject<GFTMarketDatabase>
    {
        public GFTMarketDatabaseAccessObject(GFTMarketDatabase database)
            : base(database)
        {
        }

        public override void Delete<Entity>(Entity dbObject)
        {
            if (dbObject.GetType().Name == "Transaction")
            {
                _Database.Transactions.Remove(dbObject.GetInstance<Models.Transaction>());
                _Database.SaveChanges();
            }
        }

        public override void Insert<Entity>(Entity dbObject)
        {
            switch (typeof(Entity).Name)
            {
                case "Transaction":
                    var transaction = dbObject.GetInstance<Transaction>();
                    _Database.Items.Attach(transaction.orderedItem);
                    _Database.Transactions.Add(transaction);
                    _Database.SaveChanges();
                    break;

                case "Item":
                    _Database.Items.Add(dbObject.GetInstance<Models.Item>());
                    _Database.SaveChanges();
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        public override Entity ReadSingle<Entity>(int index)
        {
            switch (typeof(Entity).Name)
            {
                case "Transaction":
                    return (Entity)Convert.ChangeType(_Database.Transactions.Find(index),
                        typeof(Models.Transaction));

                case "Item":
                    return (Entity)Convert.ChangeType(_Database.Items.Find(index),
                        typeof(Models.Item));
                default:
                    throw new ArgumentException();
            }
        }

        public override Entity ReadSingle<Entity>(string key, string value)
        {
            throw new NotImplementedException();
        }

        public override void Update<Entity>(Entity dbObject)
        {
            switch (typeof(Entity).Name)
            {
                case "Transaction":
                    var transaction = dbObject.GetInstance<Transaction>();
                    _Database.Transactions.Attach(transaction);
                    _Database.Entry(transaction).State = EntityState.Modified;
                    _Database.SaveChanges();
                    break;

                case "Item":
                    var item = dbObject.GetInstance<Item>();
                    _Database.Items.Attach(item);
                    _Database.Entry(item).State = EntityState.Modified;
                    _Database.SaveChanges();
                    break;

                default:
                    throw new ArgumentException();

            }
        }

        public DbSet<Item> GetDatabaseItemsList()
        {
            return _Database.Items;
        }

        public DbSet<Item> GetDatabaseTransactionsList()
        {
            return _Database.Items;
        }

        public void SaveChanges()
        {
            _Database.SaveChanges();
        }

        public List<Models.Item> GetSupportedItemList(string serviceId)
        {
            return _Database.Items.Where(item => item.supportedServiceId == serviceId).ToList();
        }
    }
}
