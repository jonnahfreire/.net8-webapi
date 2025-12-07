namespace WebApi.Infra.Extensions;

public static class WebHostExtensions
{
    public static IWebHostBuilder AddWebHostConfiguration(this IWebHostBuilder builder)
    {
        return builder.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
        });
    }
}
