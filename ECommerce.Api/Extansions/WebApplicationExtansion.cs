using ECommerce.Domain.Contracts;

namespace ECommerce.Api.Extansions
{
    public static class WebApplicationExtansion
    {
        public static async Task<WebApplication> SeedandMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var Seed = scope.ServiceProvider.GetRequiredKeyedService<IDataSeed>("Catalog");
            await Seed.SeedDataAsync();
            return app;
        }
    }
}
