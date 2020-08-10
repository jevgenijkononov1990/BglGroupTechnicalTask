using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BglWeather.Tests.IntegrationTests
{
    public class HttpRequestMessageWrapper
    {
        private void ValidateInputString(string input)
        {
            if (string.IsNullOrEmpty(input)) //log
                throw new ArgumentNullException($"Validation Failure in {this.GetType().Name} due to input null");
        }

        public HttpRequestMessage CreateHttpRequestMessage<Tbody>(HttpMethod apiMethodName, string apiUrl, Tbody requestBody)
        {
            //defense 
            ValidateInputString(apiUrl);

            var request = new HttpRequestMessage(apiMethodName, $"{apiUrl}");
            if (requestBody != null)
            {
                string output = JsonConvert.SerializeObject(requestBody);
                request.Content = new StringContent
                (
                    content: JsonConvert.SerializeObject(requestBody),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"
                );
            }

            return request;
        }
    }
}
