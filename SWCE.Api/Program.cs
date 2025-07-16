using FluentValidation;
using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Interfaces.Repositories; 
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Interfaces.Services; 
using SWCE.Application.Services; 
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using SWCE.Domain.Repository;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Application.Extension.Validators.ItemCarritoValidator;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.IOC.Dependencies;

namespace SWCE.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // --- Conexión a la BD ---
            var connectionString = builder.Configuration.GetConnectionString("E-commerceConnection");
            builder.Services.AddDbContext<E_commerceContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddCarritoDependencies();

            // --- Logger ---
            builder.Services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));

            // --- Configuración de API ---
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
    
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}