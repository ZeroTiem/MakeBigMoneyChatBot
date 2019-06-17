using MBM.Application.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MBM.Application.Api.Extensions
{
    /// <summary>
    /// ExceptionMiddlewareExtension
    /// </summary>
    public static class ExceptionMiddlewareExtension
    {
        /// <summary>
        /// Uses the exception middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}