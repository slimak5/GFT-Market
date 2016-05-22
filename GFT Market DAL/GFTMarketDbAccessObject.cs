using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Database
{
    public class GFTMarketDatabaseInstance : Core.DataAccessObject<GFTMarketDatabaseContext>
    {
        public GFTMarketDatabaseInstance(GFTMarketDatabaseContext database) 
            : base(database)
        {
        }

        public override void Delete<Entity>(Entity dbObject)
        {
            throw new NotImplementedException();
        }

        public override void Insert<Entity>(Entity dbObject)
        {
            throw new NotImplementedException();
        }

        public override Entity Read<Entity>(int entityId)
        {
            throw new NotImplementedException();
        }

        public override void Update<Entity>(Entity dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
