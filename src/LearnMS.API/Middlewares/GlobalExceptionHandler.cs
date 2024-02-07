using LearnMS.API.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace LearnMS.API.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "[GlobalExceptionHandler] ==> by {host}", httpContext.Request.Host);
        if (exception is ApiException apiException)
        {
            httpContext.Response.StatusCode = apiException.Error.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(new ApiWrapper.Failure
            {
                Code = apiException.Error.Code,
                Message = apiException.Error.Message
            });
            return true;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(ServerErrors.Internal);

        return true;
    }
}
