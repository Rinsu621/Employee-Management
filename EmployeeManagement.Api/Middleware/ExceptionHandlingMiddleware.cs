using EmployeeManagement.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace EmployeeManagement.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) //context contains all Http request and response information
        {
            try
            {
                await _next(context); //pass the request to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {

                    CustomValidationException => HttpStatusCode.BadRequest,
                    InvalidOperationException => HttpStatusCode.BadRequest,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = (int)statusCode;

                //var result = JsonSerializer.Serialize(new
                //{
                //    error = ex.Message,
                //    statusCode = context.Response.StatusCode
                //});

                var result = ex switch
                {
                    CustomValidationException validationEx => JsonSerializer.Serialize(new
                    {
                        errors = validationEx.Errors, 
                        statusCode = context.Response.StatusCode
                    }),
                    UnauthorizedAccessException _=> JsonSerializer.Serialize(new
                    {
                        error = "You are not authorized to access this resource.",
                        statusCode = context.Response.StatusCode
                    }),
                    _ => JsonSerializer.Serialize(new
                    {
                        error = ex.Message,
                        statusCode = context.Response.StatusCode
                    })
                };

                await context.Response.WriteAsync(result);
            }
        }

    }
}
