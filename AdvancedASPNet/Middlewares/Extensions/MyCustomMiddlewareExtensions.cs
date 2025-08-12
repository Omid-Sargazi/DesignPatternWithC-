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
}
