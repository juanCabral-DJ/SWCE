using FluentValidation;
using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Interfaces.Repositories; // Para IRepositoryBase<T>, ICarritoRepository, IItemCarritoRepository
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Interfaces.Services; // Para ICarritoService, IItemCarritoService
using SWCE.Application.Services; // Para CarritoService, ItemCarritoService
using SWCE.Application.Validators.CarritoValidator; // ¡NUEVO! Para validadores de Carrito
using SWCE.Application.Validators.ItemCarritoValidator; // ¡NUEVO! Para validadores de ItemCarrito
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;// Asumo que aquí están tus implementaciones concretas
using Microsoft.EntityFrameworkCore;


namespace SWCE.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // --- Conexión a la BD ---
            var connectionString = builder.Configuration.GetConnectionString("E-commerceConnection");
            builder.Services.AddDbContext<E_commerceContext>(options =>
                options.UseSqlServer(connectionString));


            // ¡NUEVO! Validadores de Carrito y ItemCarrito
            builder.Services.AddTransient<IValidator<CreateCarritoDto>, CreateCarritoValidator>();
            builder.Services.AddTransient<IValidator<UpdateCarritoDto>, UpdateCarritoValidator>();
            builder.Services.AddTransient<IValidator<AddItemCarritoDto>, AddItemCarritoValidator>();
            builder.Services.AddTransient<IValidator<UpdateItemCantidadDto>, UpdateItemCantidadValidator>();
            builder.Services.AddTransient<CreateItemCarritoValidator>();
            builder.Services.AddTransient<CreateCarritoValidator>();
            builder.Services.AddTransient<UpdateCarritoValidator>();

            // Asumiendo que CarritoMapper tiene lógica manual (SqlDataReader), y ItemCarritoMapper es solo para Mapperly:
            builder.Services.AddSingleton<CarritoMapper>(); // Registra si tiene lógica manual
            builder.Services.AddSingleton<ItemCarritoMapper>(); // Registra si tiene lógica manual (Aunque dijimos que este era solo partial, registrarlo no hace daño)

            // --- Repositorios ---

            // ¡NUEVO! Repositorios de Carrito y ItemCarrito
            builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
            builder.Services.AddScoped<IItemCarritoRepository, ItemCarritoRepository>();


            // --- Servicios ---

            // ¡NUEVO! Servicios de Carrito y ItemCarrito
            builder.Services.AddTransient<ICarritoService, CarritoService>();
            builder.Services.AddTransient<IItemCarritoService, ItemCarritoService>();

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