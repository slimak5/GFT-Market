using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFT.Models;

namespace GFT.Database.Core
{
    public abstract class DataAccessObject<Database> : IDisposable
        where Database : DbContext
    {
        protected Database _Database { get; set; }

        public DataAccessObject(Database database)
        {
            _Database = database;
        }

        public void Dispose()
        {
            _Database.Dispose();
        }

        public abstract void Delete<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract void Insert<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract Entity ReadSingle<Entity>(int index)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract Entity ReadSingle<Entity>(string key, string value)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract void Update<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

    }
}
