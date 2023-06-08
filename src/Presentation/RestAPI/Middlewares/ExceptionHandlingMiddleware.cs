using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
            catch (Exception ex)
            {
                if (ex.Source == "FluentValidation")
                {
                    context.Response.StatusCode =
                    (int)HttpStatusCode.BadRequest;
                    ProblemDetails details = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "Validation Error",
                        Title = "Validation Error",
                        Detail = ex.Message,
                    };
                    string json = JsonSerializer.Serialize(details);
                    context.Response.Headers["Content-Type"] = "application/json";
                    await context.Response.WriteAsync(json);
                }
                else
                {
                    context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;
                    ProblemDetails details = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server Error",
                        Title = "Server Error",
                        Detail = "An internal server has occurred",
                    };
                    string json = JsonSerializer.Serialize(details);
                    context.Response.Headers["Content-Type"] = "application/json";
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
