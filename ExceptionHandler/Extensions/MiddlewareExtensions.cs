using ExceptionHandler.Middlewares;

namespace ExceptionHandler.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
