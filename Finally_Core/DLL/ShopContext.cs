using DLL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> option) : base(option)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Info_Product> Products { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<PostponeDisk> PostponeDisks { get; set; }
        public DbSet<PeopleRegister> Peopleregister { get; set; }
        public DbSet<NowProduct> NowProducts { get; set; }
    }
}
