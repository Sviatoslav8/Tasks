using DLL.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name_Disk { get; set; }
        public string Name_Collective { get; set; }
        public string Name_Avtor { get; set; }
        public int Number_Sound { get; set; }
        public string Genre { get; set; }
        public DateTime? Issue { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public List<Sold> Solds { get; set; }
        public List<Postpone> Postpones { get; set; }
        public List<Now> Nnows { get; set; }
    }
}
