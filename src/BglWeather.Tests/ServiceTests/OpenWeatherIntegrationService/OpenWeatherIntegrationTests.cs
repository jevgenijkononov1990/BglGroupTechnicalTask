using BglWeather.Common.Integrations.OpenWeather;
using BglWeather.Common.Integrations.OpenWeather.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BglWeather.Tests.ServiceTests.OpenWeatherIntegrationService
{
    public class OpenWeatherIntegrationTests
    {
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;

        public OpenWeatherIntegrationTests()
        {

        }

        [Fact]
        public async Task GetWeatherDataAsync_WhenClientIsNull_Result_False()
        {
            //Arrange
            OpenWeatherIntegration openWeatherIntegration = new OpenWeatherIntegration();

            //Act
            var result = await openWeatherIntegration.GetWeatherDataAsync(null, "aa");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorData);
        }

        [Fact]
        public async Task GetWeatherDataAsync_WhenInputUrlNull_Result_False()
        {
            //Arrange
            OpenWeatherIntegration openWeatherIntegration = new OpenWeatherIntegration();

            //Act
            var result = await openWeatherIntegration.GetWeatherDataAsync(new HttpClient(), null);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorData);
        }

        //[Fact]
        //public async Task GetWeatherDataAsync_WhenResponseFromApiUnsuccessful_Result_False()
        //{
        //    //Arrange
        //    OpenWeatherIntegration openWeatherIntegration = new OpenWeatherIntegration();
        //    _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.BadRequest,
        //        Content = new StringContent("{\"success\": false,\"error-codes\": [\"It's a fake error!\",\"It's a fake error\"]}")
        //    });

        //    _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

        //    //Act
        //    var result = await openWeatherIntegration.GetWeatherDataAsync(_httpClient, null);

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.False(result.Success);
        //    Assert.NotNull(result.ErrorData);
        //}

        //[Fact]
        //public async Task GetWeatherDataAsync_WhenResponseFromApiSuccessful_Result_True()
        //{
        //    //Arrange
        //    WeatherIntegrationResponse temp = new WeatherIntegrationResponse();
        //    string tempJson = JsonConvert.SerializeObject(temp);

        //    OpenWeatherIntegration openWeatherIntegration = new OpenWeatherIntegration();
        //    _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content = new StringContent($"{tempJson}")
        //    });
        //    _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
            
        //    //Act
        //    var result = await openWeatherIntegration.GetWeatherDataAsync(_httpClient, null);

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //}

    }

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            //throw new NotImplementedException("Now we can setup this method with our mocking framework");
            return new HttpResponseMessage();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
