using ExceptionHandler.DTO.ExceptionDetail;
using ExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExceptionHandler.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await ExceptionHandler(httpContext, exception);
            }
        }

        private async Task ExceptionHandler(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "System server error";


            var exceptionDetail = GetExceptionDetail(exception);
            LogException(exception);


            await httpContext.Response.WriteAsync(exceptionDetail.ToJson());
        }

        private ExceptionResult GetExceptionDetail(Exception exception)
        {
            string exceptionType = exception.GetType().Name;
            switch (exception)
            {
                case ValidationException validationException:
                    return new ValidationExceptionResult
                        (
                            type: exceptionType,
                            description: exception.Message,
                            errors: validationException.Errors
                        );

                default:
                    return new ExceptionResult
                    (
                        type: exceptionType,
                        description: exception.Message
                    );
            }
        }
        private void LogException(Exception exception)
        {
            string logMessage = $"[{exception.GetType().Name}] \n " +
                                $"Message : {exception.Message} \n ";

            logMessage += exception switch
            {
                ValidationException validationException => $"Errors : {validationException.Errors} \n",
                _ => ""
            };

            _logger.LogError(exception, logMessage);
        }
    }
}
