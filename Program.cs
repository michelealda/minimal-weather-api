using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
WeatherForecast[] GetForecasts()
{
    var rng = new Random();
    return Enumerable.Range(1, 5)
        .Select(_ => rng.Next(-20, 55))
        .Select((temperature, index) => new WeatherForecast(
            DateTime.Now.AddDays(index),
            temperature,
            32 + (int)(temperature / 0.5556),
            Summaries[rng.Next(Summaries.Length)]
        ))
        .ToArray();
}

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder
            .ConfigureServices(services => { })
            .Configure(app =>
            {
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseEndpoints(e =>
                {
                    e.MapGet("/weatherforecast", async context => await context.Response.WriteAsJsonAsync(GetForecasts()));
                });
            });
    })
    .Build()
    .Run();

record WeatherForecast(DateTime Date, int TemperatureC, int TemperatureF, string Summary);