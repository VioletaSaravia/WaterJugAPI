using Microsoft.AspNetCore.Http.Timeouts;
using WaterJugAPI.Algorithms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromMilliseconds(1000),
        TimeoutStatusCode = 504,
        WriteTimeoutResponse = async context =>
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Request timeout.");
        }
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IWaterJugSolver, WaterJugSolver>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseRequestTimeouts();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();