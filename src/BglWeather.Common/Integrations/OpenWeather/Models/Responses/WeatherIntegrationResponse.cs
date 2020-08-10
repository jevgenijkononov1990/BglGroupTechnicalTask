using BglWeather.Common.General;
using System.Collections.Generic;

namespace BglWeather.Common.Integrations.OpenWeather.Models
{
    public class WeatherIntegrationResponse : IStatus
    {
        public Coordinates Coord { get; set; }
        public List<WeatherDetails> Weather { get; set; }
        public string Base { get; set; }
        public Atmosphere Main { get; set; }
        public int Visibility { get; set; }
        public Clouds Clouds { get; set; }
        public long Dt { get; set; }

        public SunDetails Sys { get; set; }
        public long Id { get; set;}
        public string Name { get; set; }
        public int Cod { get; set; }
        public bool Success { get; set; } = true;
        public ErrorData ErrorData { get; set; } = null;
    }
}
