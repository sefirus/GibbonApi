using System.Net;
using Core.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Host.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                ex.Message
            }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                ex.Message
            }));
        }  
    }
}