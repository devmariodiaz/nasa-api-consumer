using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }
    }
}
