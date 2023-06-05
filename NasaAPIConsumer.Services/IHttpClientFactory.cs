using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Services
{
    public interface IDefaultHttpClientFactory
    {
        HttpClient CreateHttpClient();
    }
}
