using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Database.Interfaces
{
    public interface IDatabaseEntity
    {
        T GetInstance<T>()
             where T : class;
    }
}
