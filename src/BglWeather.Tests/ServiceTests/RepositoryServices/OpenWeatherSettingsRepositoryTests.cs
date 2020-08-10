using BglWeather.Common.Integrations.OpenWeather.Models;
using BglWeather.Common.Integrations.OpenWeather.Repos;
using BglWeather.Web.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BglWeather.Tests.ServiceTests.RepositoryServices
{
    public class OpenWeatherSettingsRepositoryTests
    {
        public Mock<IOptions<OpenWeathertIntegrationSettings>> _mockOptions;

        public OpenWeatherSettingsRepository _settingsRepository;

        public OpenWeatherSettingsRepositoryTests()
        {
            _mockOptions = new Mock<IOptions<OpenWeathertIntegrationSettings>>();

            _settingsRepository = new OpenWeatherSettingsRepository(_mockOptions.Object);
        }

        [Fact]
        public void Test_JustEatSettingsRepository_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {

                new OpenWeatherSettingsRepository(null);

            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Fact]
        public void Test_JustEatSettingsRepository_GetSettings_When_OptionsNullOrValueNull_ResultNull()
        {
            //Arrange

            //Act
            OpenWeathertIntegrationSettings result = _settingsRepository.GetSettings();

            //Arrange
            Assert.Null(result);
        }
    }
}
