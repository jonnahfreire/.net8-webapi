namespace API.Infra.Http.Middlewares;

using API.Domain.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex)
    {
        var (statusCode, title) = ex switch
        {
            ConflictException => (HttpStatusCode.Conflict, "Conflict"),
            UnprocessableEntityException => (HttpStatusCode.UnprocessableEntity, "Unprocessable Entity"),
            NotFoundException => (HttpStatusCode.NotFound, "Not Found"),
            ArgumentNullException => (HttpStatusCode.BadRequest, "Bad Request"),
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };

        var errorResponse = new
        {
            title,
            status = (int)statusCode,
            message = ex.Message,
            traceId = context.TraceIdentifier
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
