using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HPPMDotNetCore.ExpenseTracker.Middleware
{
    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }

    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            List<string> validPaths= new List<string> {
                "/SignIn/Login".ToLower(),
                "/SignUp/Register".ToLower(),
            };
            string urlPath = context.Request.Path.ToString().ToLower();

            if (validPaths.Contains(urlPath))
                goto Result;

            string id = context.Session.GetString("Id");
            if (id == null || string.IsNullOrEmpty(id))
            {
                context.Response.Redirect("/SignIn/Login");
                return;
            }

            Result:
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}