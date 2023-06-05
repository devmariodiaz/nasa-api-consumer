using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Services
{
    public class DefaultHttpClientFactory : IDefaultHttpClientFactory
    {
        public HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }
    }
}
