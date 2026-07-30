using ECommerce.Domain.Contracts;

namespace ECommerce.Api.Extansions
{
    public static class WebApplicationExtansion
    {
        public static async Task<WebApplication> SeedandMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var catalogSeed = scope.ServiceProvider.GetRequiredKeyedService<IDataSeed>("Catalog");
            var identitySeed = scope.ServiceProvider.GetRequiredKeyedService<IDataSeed>("Identity");
            await catalogSeed.SeedDataAsync();
            await identitySeed.SeedDataAsync();
            return app;
        }
    }
}