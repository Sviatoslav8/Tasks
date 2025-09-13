using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.models
{
    public class PeopleRegister
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"\+\d{12}")]
        public string Phone { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
    }
}
