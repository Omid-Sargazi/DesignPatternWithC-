namespace AdvancedASPNet.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"got Request {context.Request.Path}");

            await _next(context);

            Console.WriteLine($"got response{context.Response.StatusCode}");
        }
    }
}
