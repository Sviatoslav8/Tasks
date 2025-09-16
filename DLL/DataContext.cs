using BLL.interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DataContext(DbContextOptions<DataContext> options) { }
        public DbSet<Car> Cars { get; set; }
        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync(default);
        }
    }
}
