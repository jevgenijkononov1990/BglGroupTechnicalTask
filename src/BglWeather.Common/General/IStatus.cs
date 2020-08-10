using BglWeather.Common.General;

namespace BglWeather.Common.Integrations
{
    public interface IStatus
    {
        bool Success { get; set; }
        ErrorData ErrorData {get;set;}
    }
}
