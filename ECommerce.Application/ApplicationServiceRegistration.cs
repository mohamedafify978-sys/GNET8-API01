using ECommerce.Application.Contacts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServicesAsync(this IServiceCollection services)
        {
            // Register application services here
            services.AddAutoMapper(c=> { },typeof(ApplicationServiceRegistration).Assembly);
            services.AddScoped<IproductService, ProductService>();
            services.AddScoped<IbasketServices, BasketService>();

            return services;
        }
    }
}
