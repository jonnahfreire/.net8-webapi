using Microsoft.AspNetCore.Mvc;

namespace WebApi.Infra.Http.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult OkResult(string? message = null, object? data = null)
    {
        return Ok(ApiResponse.Success(message, data));
    }

    protected IActionResult CreatedResult(string? message = null, object? data = null)
    {
        return StatusCode(201, ApiResponse.Success(message, data));
    }

    protected IActionResult UpdatedResult(string? message = null, object? data = null)
    {
        return StatusCode(200, ApiResponse.Success(message, data));
    }

    protected IActionResult ErrorResult(string message, object? errors = null, int statusCode = 400)
    {
        return StatusCode(statusCode, ApiResponse.Error(message, errors));
    }
}

public class ApiResponse
{
    public string? Message { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }

    public static ApiResponse Success(string? message = null, object? data = null)
    {
        return new ApiResponse
        {
            Message = message,
            Data = data
        };
    }

    public static ApiResponse Error(string message, object? errors = null)
    {
        return new ApiResponse
        {
            Message = message,
            Errors = errors
        };
    }
}