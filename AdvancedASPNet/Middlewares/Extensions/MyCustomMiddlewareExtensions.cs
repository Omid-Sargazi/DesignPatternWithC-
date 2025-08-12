using Microsoft.AspNetCore.Builder;

namespace AdvancedASPNet.Middlewares.Extensions
{
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>(); 
        }
    }

    public static class MyLoggingMiddleware
    {
        public static IApplicationBuilder UseMyLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }

    public static class MyRequestTimingMiddleware
    {
        public static IApplicationBuilder UseRequestTimingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimingMiddleware>();
        }
    }
}
