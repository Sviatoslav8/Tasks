using DLL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<People_Info> Peoples_Intfo { get; set; }
    }
}
