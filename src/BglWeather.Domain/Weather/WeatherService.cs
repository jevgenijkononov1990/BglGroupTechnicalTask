using BglWeather.Common.Constants;
using BglWeather.Common.General;
using BglWeather.Common.Integrations.OpenWeather;
using BglWeather.Common.Integrations.OpenWeather.Models;
using BglWeather.Common.Integrations.OpenWeather.Repos;
using BglWeather.Domain.Weather.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BglWeather.Domain.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherRepository _openWeatherRepository;
        private readonly IOpenWeatherIntegrationService _openWeatherIntegrationService;

        public WeatherService(IOpenWeatherRepository openWeatherRepository, IOpenWeatherIntegrationService openWeatherIntegrationService)
        {
            //defense
            _openWeatherRepository = openWeatherRepository ??
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(openWeatherRepository)}");

            _openWeatherIntegrationService = openWeatherIntegrationService ??
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(openWeatherIntegrationService)}");
        }

        public async Task<WeatherDetailsResponse<WeatherSummary>> GetWeatherDataByLocationAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                //log
                //throw new ArgumentNullException($"The ({nameof(location)}) object is null!");
                return new WeatherDetailsResponse<WeatherSummary>
                {
                    Result = null,
                    Success = false,
                    Details = $"The ({nameof(location)}) object is null!",
                };
            }

            OpenWeathertIntegrationSettings integrationSettings = _openWeatherRepository.GetSettings();

            if (integrationSettings == null 
                || string.IsNullOrWhiteSpace(integrationSettings.ApiKey) 
                || string.IsNullOrWhiteSpace(integrationSettings.CoreUrl)
                || string.IsNullOrWhiteSpace(integrationSettings.WeatherLocationEndpoint))
            {
                //log
                return new WeatherDetailsResponse<WeatherSummary>
                {
                    Result = null,
                    Success = false,
                    Details = $"The ({nameof(integrationSettings)}) object is null or one of they key properties is invalid",
                };
                // throw new ArgumentNullException($"The ({nameof(integrationSettings)}) object is null or one of they key properties is invalid");
            }

            string queryUrl = $"{integrationSettings.CoreUrl}{integrationSettings.WeatherLocationEndpoint}?q={location}&appid={integrationSettings.ApiKey}";

            WeatherIntegrationResponse integrationResponse = await _openWeatherIntegrationService.GetWeatherDataAsync(new HttpClient(), queryUrl);

            if (integrationResponse != null)
            {
                return new WeatherDetailsResponse<WeatherSummary>
                {
                    Success = integrationResponse.Success,
                    Details = integrationResponse.ErrorData == null ?  "" : $"{integrationResponse.ErrorData.Message},{integrationResponse.ErrorData.Token}",
                    Result = integrationResponse.Success ? MapToResponse(integrationResponse) : null,
                };
            }

            return null;
        }

        private WeatherSummary MapToResponse(WeatherIntegrationResponse integrationResponse)
        {
            return new WeatherSummary
            {
                Humidity = (integrationResponse.Main != null) ? integrationResponse.Main.Humidity : 0,
                Pressure = (integrationResponse.Main != null) ? integrationResponse.Main.Pressure : 0,
                Location = $"{integrationResponse.Name}, {integrationResponse.Sys?.Country}",

                Temperature = new Temperature
                {
                    Current = integrationResponse.Main != null ? WeatherHelper.ConvertKelvinDegreeToCelsiusString(integrationResponse.Main.Temp) : "",
                    Minimum = integrationResponse.Main != null ? WeatherHelper.ConvertKelvinDegreeToCelsiusString(integrationResponse.Main.Temp_min) : "",
                    Maximum = integrationResponse.Main != null ? WeatherHelper.ConvertKelvinDegreeToCelsiusString(integrationResponse.Main.Temp_max) : "",
                },

                Sunrise = integrationResponse.Sys != null ? WeatherHelper.ConvertSecondsToDateTimeString(integrationResponse.Sys.Sunrise) : "",
                Sunset = integrationResponse.Sys != null ? WeatherHelper.ConvertSecondsToDateTimeString(integrationResponse.Sys.Sunset) : ""
            };
        }
    }
}
