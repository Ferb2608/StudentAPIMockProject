using Microsoft.AspNetCore.Diagnostics;
using WebAPISample;

namespace PresentationLayer.ExceptionHandler
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message,
                Source = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
