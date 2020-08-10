using System;
using System.Collections.Generic;
using System.Text;

namespace BglWeather.Common.Enums
{
    public enum ErrorTokens
    {
        WrongInput,
        WrongRequest,
        WrongData,

        IntegrationError,
        IntegrationException,

        ServiceError,
        ServiceException,

        NullResponse,
        BadResponse,

        UnknownIssue
    }
}
