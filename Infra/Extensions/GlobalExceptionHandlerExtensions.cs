using Microsoft.AspNetCore.Mvc;

namespace API.Infra.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new
                {
                    message = "Erro de validação",
                    errors
                };

                return new BadRequestObjectResult(response);
            };
        });

        return services;
    }
}
