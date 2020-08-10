using BglWeather.Common.Integrations.OpenWeather.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BglWeather.Common.Integrations.OpenWeather
{
    public interface IOpenWeatherIntegrationService
    {
        Task<WeatherIntegrationResponse> GetWeatherDataAsync(HttpClient httpClient, string requestUrl);
    }
}
