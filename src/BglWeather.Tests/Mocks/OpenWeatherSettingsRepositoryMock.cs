using BglWeather.Common.Integrations.OpenWeather.Models;
using BglWeather.Common.Integrations.OpenWeather.Repos;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Tests.Mocks
{
    class OpenWeatherSettingsRepositoryMock : Mock<IOpenWeatherRepository>
    {
        public OpenWeatherSettingsRepositoryMock Mock_GetSettings_CustomReturn(OpenWeathertIntegrationSettings responseObject)
        {
            Setup(x => x.GetSettings())
                .Returns(responseObject);
            return this;
        }

        public OpenWeatherSettingsRepositoryMock Mock_GetSettings_ThrowException()
        {
            Setup(x => x.GetSettings())
                .Throws(new Exception("Exception"));
            return this;
        }
    }




}
