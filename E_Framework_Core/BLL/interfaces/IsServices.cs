using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IsServices<T> where T : class
    {
        void Add(T value);
    }
}
