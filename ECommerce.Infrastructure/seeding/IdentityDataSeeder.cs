using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Identity.Data;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.seeding
{
    // Note: implements Mo's IDataSeed interface (method is SeedDataAsync, not SeedAsync
    // like in the original project this was copied from).
    internal class IdentityDataSeeder : IDataSeed
    {
        private readonly StoreIdentityDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<IdentityDataSeeder> logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataSeeder> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigration.Any())
                    await dbContext.Database.MigrateAsync(ct);

                if (!await roleManager.Roles.AnyAsync(ct))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Admin",
                        Email = "admin@ecommerce.com",
                        UserName = "Admin",
                        PhoneNumber = "01000000000"
                    };
                    var createResult = await userManager.CreateAsync(admin, "P@ssw0rd");
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "SuperAdmin");
                    }
                    else
                    {
                        var errors = string.Join(';', createResult.Errors.Select(d => d.Description));
                        logger.LogWarning($"Can Not Seed Default Admin {errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Identity Data Seeding Failed");
                return;
            }
        }
    }
}
