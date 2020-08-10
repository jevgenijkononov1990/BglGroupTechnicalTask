using BglWeather.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Common.General
{
    public static class WeatherHelper
    {
        
        public static string ConvertKelvinDegreeToCelsiusString(double kelvinTemp)
        {
            double result = kelvinTemp - CommonConstants.KelvinConstant;
            result = Math.Round(result, 2);
            return $"{result}";
        }

        public static string ConvertSecondsToDateTimeString (long seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string str = time.ToString(@"hh\:mm");
            return str;
        }
    }
}
