using System.Net;
using BLL.Models;
using Infrastructure.Common;

namespace WebApi.Middlewares;

public class ExceptionCatcherMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionCatcherMiddleware> _logger;

    public ExceptionCatcherMiddleware(RequestDelegate next, ILogger<ExceptionCatcherMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Unhandled exception!");
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorDto(ex.Message));
        }
    }
}