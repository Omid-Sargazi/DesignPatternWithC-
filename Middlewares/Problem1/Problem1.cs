namespace Middlewares.Problem1
{
    public class Problem1
    {
        public static async Task Run(WebApplication app)
        {
            

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware1:Incoming Request.");
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello! Request cmpleted");
            });
        }
    }
}
