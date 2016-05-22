using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFT.Models;

namespace GFT.Database.Core
{
    public abstract class DataAccessObject<Database>
        where Database : DbContext
    {
        public abstract void Delete<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract void Insert<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract Entity Read<Entity>(int entityId)
            where Entity : Interfaces.IDatabaseEntity;

        public abstract void Update<Entity>(Entity dbObject)
            where Entity : Interfaces.IDatabaseEntity;

    }
}
