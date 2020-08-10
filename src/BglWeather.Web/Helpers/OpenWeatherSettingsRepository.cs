using BglWeather.Common.Integrations.OpenWeather.Models;
using BglWeather.Common.Integrations.OpenWeather.Repos;
using Microsoft.Extensions.Options;
using System;

namespace BglWeather.Web.Helpers
{
    public class OpenWeatherSettingsRepository : IOpenWeatherRepository
    {
        private readonly IOptions<OpenWeathertIntegrationSettings> _settings;

        public OpenWeatherSettingsRepository(IOptions<OpenWeathertIntegrationSettings> settings)
        {
            //defense
            _settings = settings ?? throw new ArgumentNullException("OpenWeatherSettingsRepository init failure due to ioptions");
        }
        public OpenWeathertIntegrationSettings GetSettings()
        {
            return _settings == null || _settings?.Value == null ? null : _settings.Value;
        }
    }
}
