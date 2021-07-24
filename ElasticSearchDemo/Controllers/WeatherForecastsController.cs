using System;
using System.Linq;
using ElasticSearchDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElasticSearchDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastsController> _logger;

        public WeatherForecastsController(ILogger<WeatherForecastsController> logger)
        {
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var rng = new Random();
                if (rng.Next(0, 5) < 2)
                {
                    throw new Exception("Oops, what happened!!!");
                }

                var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Server failed");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server failed");
            }
        }
    }
}
