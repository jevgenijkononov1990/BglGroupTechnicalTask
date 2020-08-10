namespace BglWeather.Domain.Weather.Models
{
    public class WeatherDetailsResponse<TResponse>
    {
        public bool Success { get; set; }
        public string Details { get; set; }
        public TResponse Result { get; set; }
    }
}
