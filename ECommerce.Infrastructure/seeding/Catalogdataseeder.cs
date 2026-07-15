using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.product;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.seeding
{
    public class Catalogdataseeder(StoreDbContext DbContext,ILogger<Catalogdataseeder> Logger) : IDataSeed
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigration = await DbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigration.Any()) 
                    await DbContext.Database.MigrateAsync(ct);


                var SeedPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                    await SeedIfEmptyAsync<ProductsBrand, int>(SeedPath, "brands.json", ct);
                    await SeedIfEmptyAsync<ProductsType, int>(SeedPath, "types.json", ct);
                await DbContext.SaveChangesAsync(ct);
                await SeedIfEmptyAsync<Product, int>(SeedPath, "products.json", ct);
                   int result =  await DbContext.SaveChangesAsync(ct);

                if (result > 0)
                    Logger.LogInformation($"{result} Seeded row");
                else
                    Logger.LogInformation($"already has seeded");
            }
            catch (Exception Ex)
            {

                Logger.LogError(Ex, "Failed to seed data");
                    throw;
            }
          

        }
        private async Task SeedIfEmptyAsync<T ,Key>(string rootPath, string FileName, CancellationToken ct = default) where T : BaseEntity<Key>
        {
            if (await DbContext.Set<T>().AnyAsync(ct))
            {
                Logger.LogInformation($"Table already has seeded data");
                return;
            }

            var filepath = Path.Combine(rootPath, FileName);

            if (!File.Exists(filepath))
            {
                Logger.LogWarning($"Seed File Not Found{filepath}");
                return;
            } 
            await using var stream = File.OpenRead(filepath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var items = await JsonSerializer.DeserializeAsync<List<T>>(stream, options, ct);

            if (items?.Count > 0)
            {
               
                await DbContext.Set<T>().AddRangeAsync(items,ct);
               
            }
        }

    }
}
