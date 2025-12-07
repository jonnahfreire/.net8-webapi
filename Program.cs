using WebApi.Infra.DIExtensions;
using WebApi.Infra.Extensions;
using WebApi.Infra.Http.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsPolicies();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Extensões
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddGlobalExceptionHandler();
builder.Services.AddJsonOptions();
builder.Services.AddJwtConfiguration(builder.Configuration); // JWT Auth
builder.Services.AddAuthorizationPolicies(builder.Configuration); // Policies

// Configura Swagger
builder.Services.AddSwaggerServices();
builder.Services.AddSwaggerAuthorization();

// Rate Limiter
builder.Services.AddApiRateLimiter();

// WebHost config
builder.WebHost.AddWebHostConfiguration();

var app = builder.Build();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();

//app.UseHttpsRedirection();
app.UseCors("Default");
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
