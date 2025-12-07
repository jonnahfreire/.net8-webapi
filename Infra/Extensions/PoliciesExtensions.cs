namespace WebApi.Infra.Extensions;

public static class PoliciesExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = null; // Desabilita auth global
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("UpdateUserPolicy", policy => policy.RequireClaim("permission", "update_user"));

        return services;
    }
}
