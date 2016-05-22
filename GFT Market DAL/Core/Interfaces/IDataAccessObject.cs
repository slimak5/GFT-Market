using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GFT.Database.Interfaces
{
    interface IDataAccessObject
    {
        T Read<T>(int id);
        void Insert<T>(T dbObject);
        void Update<T>(T dbObject);
        void Delete<T>(T dbObject);
    }
}
