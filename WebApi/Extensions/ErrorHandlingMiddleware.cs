namespace WebApi.Extensions
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                // Log to console
                logger.LogError(ex, "Unhandled exception occurred.");

                // Prepare a clean JSON response
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "An unexpected error occurred. Please try again later."
                });
            }
        }
    }
}