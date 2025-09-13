using DLL.interfaces;
using DLL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.repository
{
    public class PeopleRepository : IsRepository<People_Info>
    {
        private PeopleContext _context;
        public PeopleRepository(PeopleContext peopleContext)
        {
            this._context = peopleContext;
        }
        public void Add(People_Info value)
        {
            _context.Add(value);
            _context.SaveChanges();
        }
    }
}
