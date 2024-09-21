using CarRent.Application.Exceptions;

namespace CarRent.API
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var excDetails = exception switch
            {
                ValidationAppException => (Detail: exception.Message, StatusCode: StatusCodes.Status422UnprocessableEntity),
                _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError),
            };

            context.Response.StatusCode = excDetails.StatusCode;

            // Se for uma exceção de validação
            if (exception is ValidationAppException validationException)
            {
                await context.Response.WriteAsJsonAsync(new { validationException.Errors });
            }
            else
            {
                await context.Response.WriteAsJsonAsync(new { Error = excDetails.Detail });
            }
        }
    }
}
