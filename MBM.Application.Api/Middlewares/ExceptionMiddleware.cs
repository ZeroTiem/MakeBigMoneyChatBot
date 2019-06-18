using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MBM.Application.Api.Middlewares
{
    /// <summary>
    /// ExceptionMiddleware
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>
        /// Task
        /// </returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch (Exception ex)
            {
                // 如果 Exception 不是自訂錯誤才記錄到 log
                //if (!ex is MyException)
                //{
                //    this.logger.LogError($"Something went wrong: {ex}");
                //}

                this.logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Task
        /// </returns>
        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new
            {
                context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }
            .ToString());
        }
    }
}