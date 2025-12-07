namespace WebApi.Infra.Extensions;

public static class CorsPoliciesExtensions
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {

        return services.AddCors(options =>
         {
             options.AddPolicy("Default", policy =>
             {
                 policy
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();
             });
         });

    }
}
