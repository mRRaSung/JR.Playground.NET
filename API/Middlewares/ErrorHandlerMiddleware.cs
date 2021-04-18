using API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = error switch
                {
                    P4pException => (int)HttpStatusCode.BadRequest,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,

                    _ => (int)HttpStatusCode.InternalServerError,
                };

                BaseResponse rspError = new BaseResponse
                {
                    Message = error.Message,
                    Detail = _env.IsDevelopment() ? error.StackTrace : null
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(rspError), Encoding.UTF8);
            }
        }
    }
}
