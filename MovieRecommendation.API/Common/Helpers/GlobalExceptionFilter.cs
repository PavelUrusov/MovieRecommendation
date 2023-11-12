using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieRecommendation.API.Common.Helpers;

/// <summary>
///     Global exception filter to handle unhandled exceptions.
/// </summary>
public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<GlobalExceptionFilter> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GlobalExceptionFilter" /> class.
    /// </summary>
    /// <param name="environment">The hosting environment.</param>
    /// <param name="logger">The logger.</param>
    public GlobalExceptionFilter(IHostEnvironment environment, ILogger<GlobalExceptionFilter> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the exception and sets the appropriate response.
    /// </summary>
    /// <param name="context">The exception context.</param>
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception.Message, "An unhandled exception occurred");

        if (_environment.IsDevelopment())
        {
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "Internal Server Error"
            };
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new StatusCodeResult(500);
            context.ExceptionHandled = true;
        }
    }
}