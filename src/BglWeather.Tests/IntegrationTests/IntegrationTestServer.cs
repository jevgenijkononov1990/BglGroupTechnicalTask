using BglWeather.Domain.Weather;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using WebApplication2;

namespace BglWeather.Tests.IntegrationTests
{
    public class IntegrationTestServer
    {
        public readonly IWeatherService _weatherService;

        public IntegrationTestServer(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        public HttpClient CreateClient() =>
            Initialization().CreateClient();

        private TestServer Initialization()
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseStartup<Startup>().
                ConfigureTestServices(services =>
                {
                    services.AddSingleton(_weatherService);
                });
            TestServer testServer = new TestServer(webHostBuilder);

            return testServer;
        }
    }
}