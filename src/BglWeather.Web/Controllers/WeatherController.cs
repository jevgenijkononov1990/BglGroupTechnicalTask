using BglWeather.Common.Constants;
using BglWeather.Common.Enums;
using BglWeather.Common.General;
using BglWeather.Domain.Weather;
using BglWeather.Domain.Weather.Models;
using BglWeather.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            //defense
            _weatherService = weatherService ?? 
                throw new ArgumentNullException($"{this.GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(weatherService)}");
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByLocationAsync(string location)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(location))
                {
                    //log
                    return BadRequest(new ResponseWrapper<object>
                    {
                        Result = null,
                        Error = new ErrorData
                        {
                            Message = CommonConstants.ErrorMessage_CheckInputData,
                            Token = ErrorTokens.WrongInput.ToString()
                        }
                    });
                }

                WeatherDetailsResponse<WeatherSummary> response = await _weatherService.GetWeatherDataByLocationAsync(location);
                
                if (response != null && response.Success && response.Result != null)
                {
                    return Ok(new ResponseWrapper<WeatherSummary>
                    {
                        Result = response.Result,
                        Error = null
                    });
                }
                else
                {
                    //log
                    return BadRequest(new ResponseWrapper<object>
                    {
                        Result = null,
                        Error = new ErrorData
                        {
                            Message = (response == null) ? CommonConstants.ErrorMessage_UnsuccessfulFinish : response.Details,
                            Token = ErrorTokens.NullResponse.ToString()
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                // log ex 
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
