using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Framework.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string Model { get; set; }
        [Required]
        [MaxLength(40)]
        public string Color { get; set; }
        [Required]
        public int Hp { get; set; }
        [Required]
        public float Engine { get; set; }
        [Required]
        [MaxLength(40)]
        public string Country { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public float Acceleration { get; set; }
        [Required]
        public float Weight { get; set; }
    }
}
