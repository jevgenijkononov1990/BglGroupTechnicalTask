namespace BglWeather.Common.General
{
    public class WeatherSummary 
    {
        public string Location { get; set; }
        public Temperature Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
