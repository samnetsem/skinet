

using System.Net;
using System.Text.Json;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this._env = env;
            this._logger = logger;
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           try
           {
             await _next(context);
           }
           catch (Exception ex)
           {
             _logger.LogError(ex,ex.Message);
              context.Response.ContentType="application/json";
              context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
               var response=_env.IsDevelopment() 
               ? new ApiException((int)System.Net.HttpStatusCode.InternalServerError,ex.Message,
               ex.StackTrace.ToString())
               : new ApiException((int)System.Net.HttpStatusCode.InternalServerError);
                
               var options=new JsonSerializerOptions{PropertyNamingPolicy=
               JsonNamingPolicy.CamelCase}; 

               var json = JsonSerializer.Serialize(response,options);
               await context.Response.WriteAsync(json);
           }
        }

       
    }
}