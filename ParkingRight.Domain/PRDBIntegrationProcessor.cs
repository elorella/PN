using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain
{
    public class PrdbIntegrationProcessor : IPrdbIntegrationProcessor
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public PrdbIntegrationProcessor(HttpClient client, ILogger<PrdbIntegrationProcessor> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<int?> Register(ParkingRegistration request)
        {
            //I used the internal request but it would be nicer to use external model that has been shared with nuget 
            var payload = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(request));
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/ParkingRightRegistration/", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError(
                    $"Error registering park right. HTTP Status code returned: {response.StatusCode}, Message: {responseMessage}");
                return null;
            }

            var stringData = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return int.Parse(stringData);
        }
    }
}