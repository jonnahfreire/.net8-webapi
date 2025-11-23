using API.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infra.Extensions;

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
