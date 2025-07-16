using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Application.Extension.Validators.ItemCarritoValidator;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.Application.Interfaces.Repositories;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Repository;
using SWCE.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.IOC.Dependencies
{
    public static  class CarritoDependency
    {
        public static void AddCarritoDependencies(this IServiceCollection services)
        {
            // --- Repositorios ---
            services.AddScoped<ICarritoRepository, CarritoRepository>();
            services.AddScoped<IItemCarritoRepository, ItemCarritoRepository>();
            services.AddScoped<IRepositorioProducto, ProductoRepository>();
           
            // --- Servicios ---
            services.AddScoped<ICarritoService, CarritoService>();
            services.AddScoped<IItemCarritoService, ItemCarritoService>();
           
            // --- Validadores ---
            services.AddTransient<IValidator<CreateCarritoDto>, CreateCarritoValidator>();
            services.AddTransient<IValidator<UpdateCarritoDto>, UpdateCarritoValidator>();
            services.AddTransient<IValidator<AddItemCarritoDto>, AddItemCarritoValidator>();
            services.AddTransient<IValidator<UpdateItemCantidadDto>, UpdateItemCantidadValidator>();
            
            services.AddTransient<CreateItemCarritoValidator>();
            services.AddTransient<CreateCarritoValidator>();
            services.AddTransient<UpdateCarritoValidator>();
            services.AddTransient<CreateProductoValidator>();
            services.AddTransient<UpdateProductoValidator>();

        }
    }
}
