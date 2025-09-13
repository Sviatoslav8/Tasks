using BLL.models;
using DLL;
using DLL.interfaces;
using DLL.models;
using DLL.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.configuration
{
    public static class ConfigurationBLL
    {
        public static void ConfigurationCollection(ServiceCollection service, string connectionstring)
        {
            service.AddTransient(typeof(IRepository<Info_Product>), typeof(ShopRepository));
            service.AddDbContext<ShopContext>(opt => {
                opt.UseSqlServer(connectionstring);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
