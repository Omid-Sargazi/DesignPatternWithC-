using AdvancedASPNet.Middlewares;
using AdvancedASPNet.Middlewares.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Use(async (context, next) =>
{
    Console.WriteLine("Use 1");
    await next();
    Console.WriteLine("Use 1 - End");
});

app.Run(async context =>{ await context.Response.WriteAsync("Hello from Run"); });

//app.UseMiddleware<MyCustomMiddleware>();
app.UseMyCustomMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
