using StoriesFeed.API.Middleware;

namespace StoriesFeed.API.Extensions
{
    public static class ExceptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
