using NasaAPIConsumer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Services
{
    public interface INasaAPIService
    {
        Task<List<Asteroid>> Get(int days);
    }
}
