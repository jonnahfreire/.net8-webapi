using WebApi.Application.Repository;
using WebApi.Application.Services;
using WebApi.Infra.Database;
using WebApi.Infra.Repository;

namespace WebApi.Infra.DIExtensions;

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
