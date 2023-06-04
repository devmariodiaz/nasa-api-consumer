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
            DateTime date = DateTime.Now;
            DateTime endDate = currentDate.AddDays(days);
            string url = $"{_configs.BaseUrl}?start_date={currentDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}&API_KEY={_configs.ApiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var jsonParse = JsonDocument.Parse(json);
                var jsonFilter = jsonParse.RootElement.GetProperty("near_earth_objects").ToString();
                var asteroids = JsonSerializer.Deserialize<JsonElement>(jsonFilter);
                
                while(date < endDate)
                {
                    var prop = asteroids.GetProperty(date.ToString("yyyy-MM-dd"));
                    if(prop.ValueKind != JsonValueKind.Null && prop.ValueKind != JsonValueKind.Undefined)
                    {
                        for (int i = 0; i < prop.GetArrayLength(); i++)
                        {
                            var kilometers = prop[i].GetProperty("estimated_diameter").GetProperty("kilometers");
                            var close_approach_data = prop[i].GetProperty("close_approach_data");

                            var name = prop[i].GetProperty("name");
                            var estimated_diameter_min = kilometers.GetProperty("estimated_diameter_min");
                            var estimated_diameter_max = kilometers.GetProperty("estimated_diameter_max");                            
                            var kilometers_per_hour = close_approach_data[0].GetProperty("relative_velocity").GetProperty("kilometers_per_hour");
                            var close_approach_date = close_approach_data[0].GetProperty("close_approach_date");
                            var orbiting_body = close_approach_data[0].GetProperty("orbiting_body");

                            //if (bool.Parse(prop[i].GetProperty("is_potentially_hazardous_asteroid").ToString().ToLower()))
                            //{

                            //}
                        }
                    }                    

                    date = date.AddDays(1);
                }

            }
        }
    }
}
