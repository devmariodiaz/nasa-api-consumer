using Microsoft.Extensions.DependencyInjection;
using NasaAPIConsumer.Domain;
using NasaAPIConsumer.Utils;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaAPIConsumer.Services
{
    public class NasaAPIService : INasaAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly NasaAPIConfigurations _configs;
        public NasaAPIService(HttpClient httpClient)
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            _configs = _configuration.GetSection("NasaAPIConfigurations")
                                     .Get<NasaAPIConfigurations>()
                                     ?? throw new Exception("This property can't be null");
            _httpClient = httpClient;
        }
        public async Task Get(int days)
        {
            DateTime currentDate = DateTime.Now;
            DateTime endDate = currentDate.AddDays(days);
            string url = $"{_configs.BaseUrl}?start_date={currentDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}&API_KEY={_configs.ApiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var jsonParse = JsonDocument.Parse(json);
                var jsonFilter = JsonDocument.Parse(jsonParse.RootElement.GetProperty("near_earth_objects").ToString());
                var asteroids = JsonSerializer.Deserialize<Asteroid>(jsonFilter);
                
            }
        }
    }
}
