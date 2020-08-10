using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BglWeather.Tests.General
{
    public class WeatherHelperTests
    {
        [Theory]
        [InlineData(1597034324, "04:38")]
        [InlineData(1597087983, "19:33")]
        public void Test_ConvertSecondsToDateTimeString(long value, string result)
        {
            //Arrange

            //Act
            var response = BglWeather.Common.General.WeatherHelper.ConvertSecondsToDateTimeString(value);

            //Assert 
            Assert.True(response == result);
        }

        [Theory]
        [InlineData(0, "-273.15")]
        [InlineData(10, "-263.15")]
        [InlineData(20, "-253.15")]
        public void Test_ConvertKelvinDegreeToCelsiusStringg(double value, string result)
        {
            //Arrange

            //Act
            var response = BglWeather.Common.General.WeatherHelper.ConvertKelvinDegreeToCelsiusString(value);

            //Assert 
            Assert.True(response == result);
        }
    }
}
