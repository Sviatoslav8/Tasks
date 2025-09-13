using BLL.Models;
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

namespace BLL.Configuration
{
    public class ConfigurationBLL
    {
        public static void ConfigurationServiceCollection(ServiceCollection services, string connectionString)
        {
            services.AddTransient(typeof(IsRepository<People_Info>), typeof(PeopleRepository));
            services.AddDbContext<PeopleContext>(opt => {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
