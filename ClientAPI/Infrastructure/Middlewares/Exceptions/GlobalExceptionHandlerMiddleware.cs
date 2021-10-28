using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares.Exceptions
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var SerializedObject = JsonConvert.SerializeObject(exception);
                var message = $"{HttpStatusCode.InternalServerError.ToString()} - " +
                              $"Unexpected error: {exception.Message} - " +
                              $"Object: {SerializedObject}";

                _logger.LogError(exception, message, SerializedObject);
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = new
            {
                context.Response.StatusCode,
                Message = "An error occurred whilst processing your request"
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}