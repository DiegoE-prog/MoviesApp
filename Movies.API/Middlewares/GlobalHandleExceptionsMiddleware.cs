using Movies.API.Models;
using System.Net;
using System.Text.Json;

namespace Movies.API.Middlewares
{
    public class GlobalHandleExceptionsMiddleware : IMiddleware
    {
        //private readonly ILogger _logger;

        //public GlobalHandleExceptionsMiddleware(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ServiceResponse<object> response = new()
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };

                string json = JsonSerializer.Serialize(response);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
