using ApiWithbasicAuthentication.Common;
using ApiWithbasicAuthentication.Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorCode = string.Empty;

            context.Response.ContentType = "application/json";

            if (exception is CustomException)
            {
                errorCode = (exception as CustomException).ErrorCode;
                context.Response.StatusCode =int.Parse( errorCode.Split("Error_")[1]);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorCode = "Error_500";
            }

            return context.Response.WriteAsync(new ErrorDetails()
            {
                ErrorCode = errorCode,
                Message = $"Exception Source: {exception.Source} ,message {exception.Message}"
            }.ToString());
        }
    }
}
