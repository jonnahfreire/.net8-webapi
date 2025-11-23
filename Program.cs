using API.Infra.Configuration;
using API.Infra.DIExtensions;
using API.Infra.Extensions;
using API.Infra.Http.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configura Extensões
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddGlobalExceptionHandler();
builder.Services.AddJsonOptions();
builder.Services.AddJwtConfiguration(builder.Configuration); // Autenticação com JWT
builder.Services.AddPolicies(builder.Configuration); // Policies
builder.Services.AddSwaggerAuthorization(builder.Configuration); // Configura Autorização no Swagger

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();
app.AddSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
