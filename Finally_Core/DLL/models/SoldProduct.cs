using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.models
{
    public class SoldProduct
    {
        public int Id { get; set; }
        public PeopleRegister People { get; set; }
        public DateTime dateSold { get; set; }
    }
}
