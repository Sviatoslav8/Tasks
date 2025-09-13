using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.models
{
    public class PostponeDisk
    {
        public int Id { get; set; }
        public PeopleRegister People { get; set; }
        public DateTime datePostpone { get; set; }

    }
}
