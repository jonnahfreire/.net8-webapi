using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Infra.Http.Swagger;

public class SwaggerGenOptionsConfig : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public SwaggerGenOptionsConfig(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider ?? throw new ArgumentNullException(nameof(apiVersionDescriptionProvider));
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateOpenApiInfo(description));
        }
    } 

    private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description)
    {
        var webApiName = "WebApi - Jonas Freire";
        var infoDescription = "This API is part of the portfolio of Jonas Freire, a software developer specializing in building robust and scalable web applications. The API demonstrates best practices in API design, versioning, and documentation using modern technologies.";
        return new OpenApiInfo
        {
            Title = $"{webApiName} {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = infoDescription,
            Contact = new OpenApiContact
            {
                Name = "Jonas Freire",
                Email = "jonnas.freire17@gmail.com"
            }
        };
    }
}
