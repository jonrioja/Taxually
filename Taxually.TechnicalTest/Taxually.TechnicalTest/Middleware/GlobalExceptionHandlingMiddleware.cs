using System.Text.Json;

namespace Taxually.TechnicalTest.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught by middleware");

                context.Response.ContentType = "application/json";

                int statusCode;
                string message;

                switch (ex)
                {
                    case NotSupportedException or ArgumentException:
                        statusCode = StatusCodes.Status400BadRequest;
                        message = ex.Message;
                        break;

                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        message = "An unexpected error occurred. Please contact support.";
                        break;
                }

                context.Response.StatusCode = statusCode;

                var errorResponse = new
                {
                    Message = message,
                    TraceId = context.TraceIdentifier
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
