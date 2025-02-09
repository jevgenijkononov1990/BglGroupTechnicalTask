﻿namespace BglWeather.Common.Integrations.OpenWeather.Models
{
    public class SunDetails
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public double Message { get; set; }
        public string Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}
