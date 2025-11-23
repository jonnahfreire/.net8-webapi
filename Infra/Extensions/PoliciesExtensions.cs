namespace API.Infra.Configuration;

public static class PoliciesExtensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("UpdateUserPolicy", policy => policy.RequireClaim("permission", "update_user"));

        return services;
    }
}
