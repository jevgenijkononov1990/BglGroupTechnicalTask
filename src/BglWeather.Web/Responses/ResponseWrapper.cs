using BglWeather.Common.General;

namespace BglWeather.Web.Responses
{
    public class ResponseWrapper<TModel> where TModel : class
    {
        public TModel Result;

        public ErrorData Error;

        public ResponseWrapper()
        {
            Error = new ErrorData();
        }
    }
}
