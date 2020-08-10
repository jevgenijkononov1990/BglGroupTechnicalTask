using BglWeather.Common.General;
using BglWeather.Common.Integrations.OpenWeather.Models;
using BglWeather.Domain.Weather;
using BglWeather.Domain.Weather.Models;
using BglWeather.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BglWeather.Tests.ServiceTests
{
    public class WeatherServiceTests
    {
        private readonly OpenWeatherSettingsRepositoryMock _openWeatherRepositoryMock;
        private readonly OpenWeatherIntegrationServiceMock _openWeatherIntegrationServiceMock;
        private readonly WeatherService _weatherService = null;

        public WeatherServiceTests()
        {
            _openWeatherRepositoryMock = new OpenWeatherSettingsRepositoryMock();
            _openWeatherIntegrationServiceMock = new OpenWeatherIntegrationServiceMock();

            _weatherService = new WeatherService(_openWeatherRepositoryMock.Object, _openWeatherIntegrationServiceMock.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Test_WeatherService_Constructor_ForDefense_When_OneOfTheDependn_Null_Result_ThrowException(int initScenario)
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                if (initScenario == 0)
                {
                    new WeatherService(null, null);
                }
                if (initScenario == 1)
                {
                    new WeatherService(_openWeatherRepositoryMock.Object, null);
                }
                if (initScenario == 2)
                {
                    new WeatherService(null, _openWeatherIntegrationServiceMock.Object);
                }
            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task Test_GetWeatherDataByLocationAsync__WhenInput_Null_Result_SuccessFalse(string location)
        {
            //Arrange
            
            //Act
            var response = await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            Assert.NotNull(response);
            Assert.NotNull(response.Details);
            Assert.Null(response.Result);
            Assert.False(response.Success);
            Assert.IsType<WeatherDetailsResponse<WeatherSummary>>(response);
        }

        [Fact]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationSettingsThrowException_ResultEx()
        {
            //Arrange
            string location = "aaa";
            _openWeatherRepositoryMock.Mock_GetSettings_ThrowException();

            //Act
            Func<Task> act = async () => await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Theory]
        [InlineData(0,"")]
        [InlineData(1,"")]
        [InlineData(2,"")]
        [InlineData(3,"")]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(2, null)]
        [InlineData(3, null)]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationSettingsNullOrOneOfThePropsNull_Result_SuccessFalse(int initScenario, string value)
        {
            //Arrange
            string location = "aaa";
            OpenWeathertIntegrationSettings customResponse; 
            switch (initScenario)
            {
                case 0: //repo == null
                    {
                        customResponse = null;
                        break; }
                case 1: // ApiKey == null
                    {
                        customResponse = new OpenWeathertIntegrationSettings { ApiKey = value, CoreUrl = "aa", WeatherLocationEndpoint = "aaa"};
                        break; }
                case 2: // CoreUrl == null
                    {
                        customResponse = new OpenWeathertIntegrationSettings { ApiKey = "aa", CoreUrl = value, WeatherLocationEndpoint = "aaa" };
                        break; }
                case 3: // WeatherLocationEndpoint == null
                    {
                        customResponse = new OpenWeathertIntegrationSettings { ApiKey = "aa", CoreUrl = "aa", WeatherLocationEndpoint = value };
                        break; }
                default: throw new NotImplementedException();
            }
            _openWeatherRepositoryMock.Mock_GetSettings_CustomReturn(customResponse);

            //Act
            var response = await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            Assert.NotNull(response);
            Assert.NotNull(response.Details);
            Assert.Null(response.Result);
            Assert.False(response.Success);
            Assert.IsType<WeatherDetailsResponse<WeatherSummary>>(response);
        }

        [Fact]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationServiceThrowException_ResultEx()
        {
            //Arrange
            string location = "aaa";
            _openWeatherRepositoryMock.Mock_GetSettings_CustomReturn(new OpenWeathertIntegrationSettings { ApiKey = "aa",CoreUrl = "aa", WeatherLocationEndpoint = "aa" });
            _openWeatherIntegrationServiceMock.Mock_GetWeatherDataAsync_ThrowsException();

            //Act
            Func<Task> act = async () => await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationServiceGivesNull_ResultNull()
        {
            //Arrange
            string location = "aaa";
            _openWeatherRepositoryMock.Mock_GetSettings_CustomReturn(new OpenWeathertIntegrationSettings { ApiKey = "aa", CoreUrl = "aa", WeatherLocationEndpoint = "aa" });
            _openWeatherIntegrationServiceMock.Mock_GetWeatherDataAsync_CustomResponse(null);

            //Act
            var response =  await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            Assert.Null(response);
        }

        [Fact]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationServiceResponseNotNullSuccess_ResultNotNullObjectBack()
        {
            //Arrange
            string location = "aaa";
            _openWeatherRepositoryMock.Mock_GetSettings_CustomReturn(new OpenWeathertIntegrationSettings { ApiKey = "aa", CoreUrl = "aa", WeatherLocationEndpoint = "aa" });
            _openWeatherIntegrationServiceMock.Mock_GetWeatherDataAsync_CustomResponse(new WeatherIntegrationResponse
            {
                Success = true,
                ErrorData = null,
                Base = "",
                Main = new Atmosphere(),
                Sys = new SunDetails(),
            });

            //Act
            var response = await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotNull(response.Result);
            Assert.IsType<WeatherDetailsResponse<WeatherSummary>>(response);
        }

        [Fact]
        public async Task Test_GetWeatherDataByLocationAsync_WhenIntegrationServiceResponseNotSuccess_ResultNotNullObjectBack()
        {
            //Arrange
            string location = "aaa";
            _openWeatherRepositoryMock.Mock_GetSettings_CustomReturn(new OpenWeathertIntegrationSettings { ApiKey = "aa", CoreUrl = "aa", WeatherLocationEndpoint = "aa" });
            _openWeatherIntegrationServiceMock.Mock_GetWeatherDataAsync_CustomResponse(new WeatherIntegrationResponse
            {
                Success = false,
            });

            //Act
            var response = await _weatherService.GetWeatherDataByLocationAsync(location);

            //Arrange
            Assert.NotNull(response);
            Assert.Null(response.Result);
            Assert.IsType<WeatherDetailsResponse<WeatherSummary>>(response);
        }
    }
}
