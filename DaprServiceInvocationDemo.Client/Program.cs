using System;
using System.Text.Json;
using Dapr.Client;

namespace DaprServiceInvocationDemo.Client
{
    class Program
    {
        static void Main(string[] args)
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
        }
    }
}