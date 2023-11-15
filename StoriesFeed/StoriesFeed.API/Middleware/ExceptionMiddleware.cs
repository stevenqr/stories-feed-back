using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace StoriesFeed.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }            
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var error = ErrorCreator.CreateError(ex);
            context.Response.StatusCode = error.Code;
            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            var jsonString = JsonConvert.SerializeObject(error);
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
    }
}
