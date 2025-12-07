using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json;

namespace WebApi.Infra.Extensions;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddApiRateLimiter(this IServiceCollection services)
    {
        return services.AddRateLimiter(options =>
        {
            // 10 requests per second
            options.AddFixedWindowLimiter("FixedWindow", opt =>
            {
                opt.PermitLimit = 10;
                opt.Window = TimeSpan.FromSeconds(1);
                opt.QueueLimit = 0;
            });

            options.OnRejected = async (context, token) => 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    title = "Too many requests",
                    status = (int)StatusCodes.Status429TooManyRequests,
                    message = "Limite de requisições excedido",
                    traceId = context.HttpContext.TraceIdentifier
                };

                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse), token);
            };
        });
    }
}
