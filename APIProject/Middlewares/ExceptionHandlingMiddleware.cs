using System.Text.Json;

namespace APIProject.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // اجرای بقیه pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " خطای کنترل‌نشده رخ داد!");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment()
                    ? new
                    {
                        statusCode = context.Response.StatusCode,
                        message = ex.Message,
                        //stackTrace = ex.StackTrace
                    }
                    : new
                    {
                        statusCode = context.Response.StatusCode,
                        message = "خطای داخلی سرور رخ داده است"
                    };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }

}
