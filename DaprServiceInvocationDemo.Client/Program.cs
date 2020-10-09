using System;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Dapr.Client.Http;
using DaprServiceInvocationDemo.Client.Models;

namespace DaprServiceInvocationDemo.Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };

            var client = new DaprClientBuilder()
                .UseJsonSerializationOptions(jsonOptions)
                .Build();

            Console.WriteLine("Retrieving weather forecast...");

            var httpExtension = new HTTPExtension
            {
                Verb = HTTPVerb.Get
            };

            var response = await client.InvokeMethodAsync<WeatherForecastModel[]>("weather-forecast", "weather", httpExtension);

            Console.WriteLine("Weather forecast received:");

            foreach (var thisForecast in response)
            {
                Console.WriteLine(thisForecast);
            }
        }
    }
}