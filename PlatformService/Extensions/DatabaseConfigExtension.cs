using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.Extensions
{
    public static class DatabaseConfigExtension
    {
        public static void ConfigurationDatabase(
            this IServiceCollection services,
            IWebHostEnvironment env,
            IConfiguration configuration
        )
        {
            if (env.IsProduction())
            {
                Console.WriteLine("--> Using SQL Server");
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                Console.WriteLine("--> Using In-Memory DB");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }
        }
    }
}