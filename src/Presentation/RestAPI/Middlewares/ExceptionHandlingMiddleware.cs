using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace BowlingGame.Presentation.RestAPI.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch
            {
                context.Response.StatusCode = 
                    (int)HttpStatusCode.InternalServerError;
                ProblemDetails details = new ProblemDetails()
                {
                    Status= (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An internal server has occurred",
                };
                string json = JsonSerializer.Serialize(details);
                await context.Response.WriteAsync(json);
                context.Response.ContentType = "application/json";
            }
        }
    }
}
