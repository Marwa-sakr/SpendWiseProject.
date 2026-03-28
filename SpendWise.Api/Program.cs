using Microsoft.EntityFrameworkCore; // 1. لازم نضيف الـ Namespace ده
using SpendWise.Core.Data;          // 2. وعشان يشوف الـ ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// 3. إضافة الـ DbContext للـ Container (الربط بالداتابيز)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// كود الـ WeatherForecast (ممكن تسيبيه أو تمسحيه مش هيأثر علينا حالياً)
var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
app.MapGet("/weatherforecast", () => { /* ... */ }).WithName("GetWeatherForecast").WithOpenApi();

app.Run();

// الـ Record بتاع الـ WeatherForecast بيفضل في الآخر عادي
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
