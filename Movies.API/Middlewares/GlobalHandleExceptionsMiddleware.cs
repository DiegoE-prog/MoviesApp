using Movies.API.Exceptions;
using Movies.Common.Models.Http;
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
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await SendResponse(context, ex);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await SendResponse(context, ex);
            }
        }

        private ServiceResponse<object> CreateResponse(string message)
        {
            ServiceResponse<object> response = new()
            {
                Data = null,
                Message = message,
                Success = false
            };

            return response;
        }

        private async Task SendResponse(HttpContext context, Exception ex)
        {
            ServiceResponse<object> response = CreateResponse(ex.Message);

            string json = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}