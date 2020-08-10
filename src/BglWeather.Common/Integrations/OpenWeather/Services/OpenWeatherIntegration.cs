using BglWeather.Common.Integrations.OpenWeather.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BglWeather.Common.Integrations.OpenWeather
{
    public class OpenWeatherIntegration : IOpenWeatherIntegrationService
    {
   
        public OpenWeatherIntegration()
        {
            
        }

        public async Task<WeatherIntegrationResponse> GetWeatherDataAsync(HttpClient httpClient, string requestUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(requestUrl))
                {
                    throw new ArgumentException();
                }
                if(httpClient == null)
                {
                    throw new ArgumentException();
                }

                var request = new HttpRequestMessage(new HttpMethod("GET"), $"{requestUrl}");
                var response = await httpClient.SendAsync(request);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var convertedObject = JsonConvert.DeserializeObject<WeatherIntegrationResponse>(jsonResponse);
                    return convertedObject;
                }

                return new WeatherIntegrationResponse
                {
                    Success = false,
                    ErrorData = new General.ErrorData
                    {
                        Message = response.ReasonPhrase,
                        Token = Enums.ErrorTokens.IntegrationError.ToString()
                    }
                };
            }
            catch(Exception ex)
            {
                //log
                return new WeatherIntegrationResponse
                {
                    Success = false,
                    ErrorData = new General.ErrorData
                    {
                        Message = "Something is wrong",
                        Token = Enums.ErrorTokens.IntegrationException.ToString()
                    }
                };
            }
        }
    }
}
