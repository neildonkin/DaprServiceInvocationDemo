using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Dapr.Client.Http;
using DaprServiceInvocationDemo.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DaprServiceInvocationDemo.UI.Models;

namespace DaprServiceInvocationDemo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            
            var client = new DaprClientBuilder()
                .UseJsonSerializationOptions(jsonOptions)
                .Build();
            
            var httpExtension = new HTTPExtension
            {
                Verb = HTTPVerb.Get
            };

            var weatherForecast = await client.InvokeMethodAsync<WeatherForecastModel[]>("weather-forecast", "weather", httpExtension);

            return View(weatherForecast);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet("healthz")]
        public IActionResult Health()
        {
            return NoContent();
        }
    }
}