using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.interfaces
{
    public interface IsRepository<T>
    {
        void Add(T value);
    }
}
