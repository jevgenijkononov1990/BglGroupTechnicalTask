using BglWeather.Common.General;
using BglWeather.Domain.Weather;
using BglWeather.Domain.Weather.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Tests.Mocks
{
    public class WeatherServiceMock : Mock<IWeatherService>
    {
        public WeatherServiceMock Mock_GetWeatherDataByLocationAsync_WithCustomReturn(WeatherDetailsResponse<WeatherSummary> weatherDetailsResponse)
        {
            Setup(x => x.GetWeatherDataByLocationAsync(It.IsAny<string>()))
                .ReturnsAsync(weatherDetailsResponse);
            return this;
        }

        public WeatherServiceMock Mock_GetWeatherDataByLocationAsync_ThrowsException()
        {
            Setup(x => x.GetWeatherDataByLocationAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Exception"));
            return this;
        }
    }
}
