using ECommerce.Api.Extansions;
using ECommerce.Application;
using ECommerce.Application.Profiles;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Identity.Services;
using Microsoft.Extensions.FileProviders;
namespace ECommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDbConnection"));
            //});

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServicesAsync();

            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //connect with infrastructure
            await app.SeedandMigrateDataAsync();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Files"

            });
            app.UseHttpsRedirection();

            // Must come BEFORE UseAuthorization, otherwise [Authorize] endpoints
            // always return 401 because the request is never authenticated.
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
