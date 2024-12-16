using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IHostEnvironment env;

        // by conventions
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message); // Development Mode
                // log Exception (Database | Files) using serial log package => Production Model

                // i want to config http response (head | body)
                // 1. head (config type , statusCode)
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode =  (int)HttpStatusCode.InternalServerError;   // 500



                // 2. body (config response body shape)

                var response = env.IsDevelopment() ? new ExceptionApiResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ExceptionApiResponse((int)HttpStatusCode.InternalServerError);


                var options =new JsonSerializerOptions(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json  = JsonSerializer.Serialize(response , options);


                httpContext.Response.WriteAsync(json);


            }
        }
    }
}
