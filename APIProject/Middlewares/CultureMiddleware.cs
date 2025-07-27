using System.Globalization;

namespace APIProject.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var culture = "en";

            var langQuery = context.Request.Query["lang"].ToString();

            if (!string.IsNullOrWhiteSpace(langQuery))
            {
                culture = langQuery;
            }

            try
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }
            catch (CultureNotFoundException)
            {

            }

            await _next(context);
        }
    }
}
