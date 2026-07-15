using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositorys;
using ECommerce.Infrastructure.seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreDbConnection"));
            });
            //services.AddScoped<IDataSeed, Catalogdataseeder>();
            services.AddKeyedScoped<IDataSeed, Catalogdataseeder>("Catalog");


            services.AddScoped<IUnitOfWork, UnitOfWork>();



            return services;
        }
    }
}
