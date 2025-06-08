using System.Net;
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex, "Client error: not supported");
                await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, "Invalid Request", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled server error");
                await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, "Unexpected Error", "An unexpected error occurred. Please contact support.");
            }
        }

        private static async Task WriteProblemDetailsAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var problem = new
            {
                type = $"https://httpstatuses.com/{(int)statusCode}",
                title,
                status = (int)statusCode,
                detail,
                traceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}
