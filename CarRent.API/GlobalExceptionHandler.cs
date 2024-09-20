using CarRent.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CarRent.API
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var excDetails = exception switch
            {
                ValidationAppException => (Detail: exception.Message, StatusCode: StatusCodes.Status422UnprocessableEntity),
                _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError),
            };

            httpContext.Response.StatusCode = excDetails.StatusCode;

            if (exception is ValidationAppException validationException)
            {
                await httpContext.Response.WriteAsJsonAsync(new { validationException.Errors });

                return true;
            }

            return false;
        }
    }
}
