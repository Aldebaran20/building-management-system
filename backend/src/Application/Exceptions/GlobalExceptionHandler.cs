using Microsoft.AspNetCore.Diagnostics;

namespace BMS.Application.Exceptions;
    
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        _logger.LogError(
            "Error Message: {exceptionMessage}\nTime of occurrence {time}\nStack Trace: {stackTrace}",
            exceptionMessage, DateTime.UtcNow, exception.StackTrace);
        // Return false to continue with the default behavior
        // - or - return true to signal that this exception is handled
        return ValueTask.FromResult(false);
    }
}