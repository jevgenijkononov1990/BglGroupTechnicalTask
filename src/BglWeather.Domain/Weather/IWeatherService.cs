using BglWeather.Common.General;
using BglWeather.Domain.Weather.Models;
using System.Threading.Tasks;

namespace BglWeather.Domain.Weather
{
    public interface IWeatherService
    {
        Task<WeatherDetailsResponse<WeatherSummary>> GetWeatherDataByLocationAsync(string location);
    }
}
