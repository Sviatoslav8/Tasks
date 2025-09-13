using E_Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Framework.Context
{
    public class CarContext : DbContext
    {
        public CarContext(string connectionString) : base(connectionString) { }
        public DbSet<Car> Cars { get; set; }
    }
}
