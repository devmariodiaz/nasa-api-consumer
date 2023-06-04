using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Domain
{
    public  class AsteroidResponse
    {
        public string date { get; set; }
        List<Asteroid> asteroids { get; set; }
    }
}
