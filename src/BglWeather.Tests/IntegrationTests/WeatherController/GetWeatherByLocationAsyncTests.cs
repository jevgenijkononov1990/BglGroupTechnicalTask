using BglWeather.Common.General;
using BglWeather.Domain.Weather;
using BglWeather.Domain.Weather.Models;
using BglWeather.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BglWeather.Tests.IntegrationTests.WeatherController
{
    public class GetWeatherByLocationAsyncTests
    {
        private readonly HttpClient _client;
        private readonly WeatherServiceMock _weatherServiceMock;
        private readonly HttpRequestMessageWrapper _controllerRequester;
        private const string _apiPath = "api/weather/";
        //private const HttpMethod _apiMethodType = HttpMethod.Get;

        public GetWeatherByLocationAsyncTests()
        {
            #region UnitTestPreparation

            _controllerRequester = new HttpRequestMessageWrapper();
            _weatherServiceMock = new WeatherServiceMock();
            _client = new IntegrationTestServer(_weatherServiceMock.Object).CreateClient();

            #endregion
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetWeatherByLocationAsync_Test_When_RequestNull_Result_BadRequest(string location)
        {
            //Arrange
            var requestUrl = _apiPath + $"?location={location}";

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_Test_When_WeatherServiceThrowsException_Result_InternalServerError()
        {
            //Arrange
            string location = "location";
            var requestUrl = _apiPath + $"?location={location}";
            _weatherServiceMock.Mock_GetWeatherDataByLocationAsync_ThrowsException();

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_Test_When_WeatherServiceReturnsNull_Result_BadRequest()
        {
            //Arrange
            string location = "location";
            var requestUrl = _apiPath + $"?location={location}";
            _weatherServiceMock.Mock_GetWeatherDataByLocationAsync_WithCustomReturn(null);

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_Test_When_WeatherServiceReturnsUnsuccess_Result_BadRequest()
        {
            //Arrange
            string location = "location";
            var requestUrl = _apiPath + $"?location={location}";
            _weatherServiceMock.Mock_GetWeatherDataByLocationAsync_WithCustomReturn(new WeatherDetailsResponse<WeatherSummary>
            {
                Success = false
            });

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_Test_When_WeatherServiceReturnsSuccessButResultIsNull_Result_BadRequest()
        {
            //Arrange
            string location = "location";
            var requestUrl = _apiPath + $"?location={location}";
            _weatherServiceMock.Mock_GetWeatherDataByLocationAsync_WithCustomReturn(new WeatherDetailsResponse<WeatherSummary>
            {
                Success = true,
                Result = null,
            });

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_Test_When_WeatherServiceReturnsValidtObject_Result_OK()
        {
            //Arrange
            string location = "location";
            var requestUrl = _apiPath + $"?location={location}";
            _weatherServiceMock.Mock_GetWeatherDataByLocationAsync_WithCustomReturn(new WeatherDetailsResponse<WeatherSummary>
            {
                Success = true,
                Result = new WeatherSummary(),
            });

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(HttpMethod.Get, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
