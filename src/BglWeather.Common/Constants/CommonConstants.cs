using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Common.Constants
{
    public static class CommonConstants
    {
        public const string ConstructorInitFailure = "Initialization failure due to:";
        public const string ConstructorInitSuccess = "Initialization success";

        public const string ErrorMessage_CheckInputData = "Sorry, your request has failed. Please check your request input.";
        public const string ErrorMessage_UnsuccessfulFinish = "Sorry, your request has not failed. However, there is no data for the requested criteria.";

        public const double KelvinConstant = 273.15;
    }
}
