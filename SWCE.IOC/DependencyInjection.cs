using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCE.Application.Extension.Validators.CategoriaValidators;
using SWCE.Application.Extension.Validators.CuponValidators.CuponMontoFijoValidators;
using SWCE.Application.Extension.Validators.CuponValidators.CuponPorcentajeValidators;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;

namespace SWCE.IOC
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // --- Validaciones ---
            services.AddScoped<CreateProductoValidator>();
            services.AddScoped<UpdateProductoValidator>();
            services.AddScoped<CreateCategoriaValidator>();
            services.AddScoped<UpdateCategoriaValidator>();
            services.AddScoped<CreateCuponMontoFijoValidator>();
            services.AddScoped<UpdateCuponMontoFijoValidator>();
            services.AddScoped<CreateCuponPorcentajeValidator>();
            services.AddScoped<UpdateCuponPorcentajeValidator>();

            // --- Logging ---
            services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));

            // --- Repositorios ---
            services.AddScoped<IRepositorioProducto, ProductoRepository>();
            services.AddScoped<IRepositorioCategoria, CategoriaRepository>();
            services.AddScoped<IRepositorioCuponMontoFijo, CuponMontoFijoRepository>();
            services.AddScoped<IRepositorioCuponPorcentaje, CuponPorcentajeRepository>();

            // --- Servicios ---
            services.AddScoped<IProductoServices, ProductoService>();
            services.AddScoped<ICategoriaServices, CategoriaServices>();
            services.AddScoped<ICuponMontoFijoServices, CuponMontoFijoService>();
            services.AddScoped<ICuponPorcentajeServices, CuponPorcentajeService>();
        }
    }

}
