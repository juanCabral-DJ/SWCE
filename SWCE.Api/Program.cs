using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWCE.Application.Base.AdministrationModuleMappers;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Application.Validators.CategoriaValidators;
using SWCE.Application.Validators.CuponValidators.CuponMontoFijoValidators;
using SWCE.Application.Validators.CuponValidators.CuponPorcentajeValidators;
using SWCE.Application.Validators.ProductoValidators;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Conexión a la bd
        var connectionString = builder.Configuration.GetConnectionString("E-commerceConnection");
        builder.Services.AddDbContext<E_commerceContext>(options =>
                                    options.UseSqlServer(connectionString));

        // --- Validadores
        builder.Services.AddScoped<CreateProductoValidator>();
        builder.Services.AddScoped<UpdateProductoValidator>();
        builder.Services.AddScoped<CreateCategoriaValidator>();
        builder.Services.AddScoped<UpdateCategoriaValidator>();
        builder.Services.AddScoped<CreateCuponMontoFijoValidator>();
        builder.Services.AddScoped<UpdateCuponMontoFijoValidator>();
        builder.Services.AddScoped<CreateCuponPorcentajeValidator>();
        builder.Services.AddScoped<UpdateCuponPorcentajeValidator>();

        // --- Configuración de AutoMapper ---
        builder.Services.AddSingleton<ProductoMapper>();
        builder.Services.AddSingleton<CategoriaMapper>();
        builder.Services.AddSingleton<CuponMontoFijoMapper>();
        builder.Services.AddScoped<CuponPorcentajeMapper>();

        // --- Configuración de Logging ---
        builder.Services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));

        //  ---Repositorios ---
        builder.Services.AddScoped<IRepositorioProducto, ProductoRepository>();
        builder.Services.AddScoped<IRepositorioCategoria, CategoriaRepository>();
        builder.Services.AddScoped<IRepositorioCuponMontoFijo, CuponMontoFijoRepository>();
        builder.Services.AddScoped<IRepositorioCuponPorcentaje, CuponPorcentajeRepository>();

        // --- Configuración de servicios ---
        builder.Services.AddTransient<IProductoServices, ProductoService>();
        builder.Services.AddScoped<ICategoriaServices, CategoriaServices>();
        builder.Services.AddScoped<ICuponMontoFijoServices, CuponMontoFijoService>();
        builder.Services.AddScoped<ICuponPorcentajeServices, CuponPorcentajeService>();


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