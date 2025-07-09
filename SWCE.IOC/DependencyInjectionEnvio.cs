using Microsoft.Extensions.DependencyInjection;
using SWCE.Aplication.Extension.Validators.EnvioValidator;
using SWCE.Aplication.Interfaces.Services;
using SWCE.Aplication.Services;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;

namespace SWCE.IOC
{
    public static class DependencyInjectionEnvio
    {
        public static void AddEnvioDependencies(this IServiceCollection services)
        {

            //validaciones
            services.AddTransient<EnvioValidator>();
            services.AddTransient<UpdateEnvioValidator>();

            //Repositorios
            services.AddScoped<IEnvioRepository, EnvioRepository>();
            services.AddTransient<IEnvioServices, EnvioServices>();

            services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));
        }
    }
}
