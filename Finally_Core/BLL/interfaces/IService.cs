using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IService<T> where T : class
    {
        void Add(T value);
        void Edit(T value);
        T FindForOtherMethod(int id);
        void Delete(T value);
        IEnumerable<T> GetInfo();
        T FindByName(string name);
        T FindByAvtor(string avtor);
        T FindByGenre(string genre);
    }
}
