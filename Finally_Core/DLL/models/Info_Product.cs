using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.models
{
    public class Info_Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name_Disk { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name_Collective { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name_Avtor { get; set; }
        [Required]
        public int Number_Sound { get; set; }
        [Required]
        [MaxLength(30)]
        public string Genre { get; set; }
        [Required]
        public DateTime? Issue { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public int Price { get; set; }
        public List<SoldProduct> SoldProducts { get; set; }
        public List<PostponeDisk> PostponeDisks { get; set; }
        public List<NowProduct> NnowProducts { get; set; }
    }
}
