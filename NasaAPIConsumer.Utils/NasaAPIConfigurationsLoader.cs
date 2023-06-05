using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaAPIConsumer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Utils
{
    public class NasaAPIConfigurationsLoader
    {
        public NasaAPIConfigurations LoadConfigurations()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            var configs = configuration?.GetSection("NasaAPIConfigurations")
                                        .Get<NasaAPIConfigurations>();

            if (configs == null)
                throw new Exception("NasaAPIConfigurations cannot be null");

            return configs;
        }
    }
}
