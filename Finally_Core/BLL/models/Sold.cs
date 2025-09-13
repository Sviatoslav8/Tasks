using DLL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.models
{
    public class Sold
    {
        public int Id { get; set; }
        public People People { get; set; }
        public DateTime dateSold { get; set; }
    }
}
