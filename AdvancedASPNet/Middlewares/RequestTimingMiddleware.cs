using System.Diagnostics;

namespace AdvancedASPNet.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Request to {context.Request.Path} took {elapsedMs} ms");
        }

    }
}
