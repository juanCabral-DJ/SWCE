using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Aqui va la cadena de conexión de la bd
            //Aqui se registran los repositorios y otros servicios
            return services;
        }
    }
}
