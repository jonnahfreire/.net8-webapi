using API.Application.Repository;
using API.Application.Services;
using API.Infra.Database;
using API.Infra.Repository;

namespace API.Infra.DIExtensions;

public static class DIConfigurationExtensions
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
   
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
        return services;
    }
}
