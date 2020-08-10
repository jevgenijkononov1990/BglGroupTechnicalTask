using BglWeather.Common.Integrations.OpenWeather;
using BglWeather.Common.Integrations.OpenWeather.Models;
using Moq;
using System;
using System.Net.Http;

namespace BglWeather.Tests.Mocks
{
    public class OpenWeatherIntegrationServiceMock : Mock<IOpenWeatherIntegrationService>
    {
        public OpenWeatherIntegrationServiceMock Mock_GetWeatherDataAsync_CustomResponse(WeatherIntegrationResponse response)
        {
            Setup(x => x.GetWeatherDataAsync (It.IsAny<HttpClient>(), It.IsAny<string>())).ReturnsAsync(response);
            return this;
        }

        public OpenWeatherIntegrationServiceMock Mock_GetWeatherDataAsync_ThrowsException()
        {
            Setup(x => x.GetWeatherDataAsync(It.IsAny<HttpClient>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            return this;
        }
    }
}
