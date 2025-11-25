using API.Infra.Http.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace API.Infra.Extensions;

public static class JwtConfigurationExtensions
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthorizationConfig>(configuration.GetSection("Authorization"));

        var auth = configuration.GetSection("Authorization").Get<AuthorizationConfig>() 
            ?? throw new InvalidOperationException("Authorization configuration is missing.");

        var key = Encoding.UTF8.GetBytes(auth.Key);

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (context.Response.HasStarted) return context.Response.CompleteAsync();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new { message = "Não autorizado" });
                        return context.Response.WriteAsync(result);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new { message = "Token inválido" });
                            return context.Response.WriteAsync(result);
                        }
                        return context.Response.CompleteAsync();
                    },
                    OnForbidden = context =>
                    {
                        context.NoResult();
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new { message = "Acesso negado. Você não possui permissão para acessar este recurso." });
                            return context.Response.WriteAsync(result);
                        }
                        return context.Response.CompleteAsync();
                    }
                };

                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = auth.Issuer,
                    ValidAudience = auth.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return services;
    }
}
