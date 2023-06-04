using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Services
{
    public class NasaAPIService : INasaAPIService
    {
        private readonly HttpClient _httpClient;
        public NasaAPIService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public Task<List<INasaAPIService>> Get(int days)
        {

            throw new NotImplementedException();
        }
    }
}
