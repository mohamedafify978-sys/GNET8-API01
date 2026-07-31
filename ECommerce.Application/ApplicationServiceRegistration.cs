using ECommerce.Application.Contacts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServicesAsync(this IServiceCollection services)
        {
            // Register application services here
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegistration).Assembly);
            services.AddScoped<IproductService, ProductService>();
            services.AddScoped<IbasketServices, BasketService>();

            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
