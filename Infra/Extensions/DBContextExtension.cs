using Microsoft.EntityFrameworkCore;
using WebApi.Infra.Database;

namespace WebApi.Infra.Extensions;

public static class DBContextExtension
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
        });

        return services;
    }
}
