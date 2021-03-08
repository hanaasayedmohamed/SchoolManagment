using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public static async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        public static bool LogError(ILogger logger, Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            return true;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var stopWatch = new Stopwatch();

                var dictionary = new Dictionary<string, object>
                {
                    { "CorrelationId", context.Request.Headers["X-Correlation-Id"].ToString() },
                    { "RequestBody", await FormatRequestAsync(context.Request) },
                    { "RequestUri", $"{context.Request.Host.ToString()}/{context.Request.PathBase.ToString()}/{context.Request.Path.ToString()}/{context.Request.QueryString.ToString()}"},
                    { "Method", context.Request.Method},
                    { "IpAddress", context.Connection.RemoteIpAddress.ToString()},
                    { "ServiceName", "StudentsService"},
                };

                var originalBodyStream = context.Response.Body;

                await using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    using (_logger.BeginScope(dictionary))
                    {
                        stopWatch.Start();

                        await _next(context);

                        stopWatch.Stop();
                        using (_logger.BeginScope(
                            new Dictionary<string, object> {
                                { "ResponseBody", await FormatResponse(context.Response) },
                                { "StatusCode", context.Response.StatusCode },
                                { "ResponseTime" , stopWatch.Elapsed.TotalMilliseconds},
                            }))

                            _logger.LogInformation("Internal Logging Event");
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }
            catch (Exception ex)
                when (LogError(_logger, ex))
            { }
        }

        private static async Task<string> FormatRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();
            int bufferSize = 1024;

            if (request.ContentLength != null && request.ContentLength != 0)
            {
                bufferSize = (int)request.ContentLength.Value;
            }

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, bufferSize, true))
            {
                var bodyAsText = await reader.ReadToEndAsync();
                request.Body.Position = 0;

                return bodyAsText;
            }
        }
    }
}