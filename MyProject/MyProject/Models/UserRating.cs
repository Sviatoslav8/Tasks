using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class UserRating
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public UserRating(string name, int grade)
        {
            this.Name = name;
            this.Grade = grade;
        }
        public UserRating()
        {

        }
    }
}
