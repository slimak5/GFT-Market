using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFT.Models;

namespace GFT.Database.Core
{
    public abstract class DataAccessObject<Database> : Interfaces.IDataAccessObject
        where Database : DbContext
    {
        private Database _database;

        public DataAccessObject(Database database)
        {
            _database = database;
        }

        public abstract void Delete<Entity>(Entity dbObject);
        public abstract void Insert<Entity>(Entity dbObject);
        public abstract Entity Read<Entity>(int entityId);
        public abstract void Update<Entity>(Entity dbObject);
    }
}
