using BglWeather.Common.Integrations.OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Common.Integrations.OpenWeather.Repos
{
    public interface IOpenWeatherRepository
    {
        OpenWeathertIntegrationSettings GetSettings();
    }
}
