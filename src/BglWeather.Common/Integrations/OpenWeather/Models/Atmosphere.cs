using BglWeather.Common.General;

namespace BglWeather.Common.Integrations.OpenWeather.Models
{
    public class Atmosphere : IAtmosphereDetails, ITemperature
    {
        public int Pressure { get; set ; }
        public int Humidity { get; set; }
        public double Temp { get; set; }
        public double Temp_max { get; set; }
        public double Temp_min { get; set; }
    }
}
