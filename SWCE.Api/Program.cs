using Microsoft.EntityFrameworkCore;
using SWCE.Application.Extension.Mapeo_Registro.Mapeo_Producto;
using SWCE.Application.Extension.MappersAdministrationModule;
using SWCE.Application.Extension.Validators.CategoriaValidators;
using SWCE.Application.Extension.Validators.CuponValidators.CuponMontoFijoValidators;
using SWCE.Application.Extension.Validators.CuponValidators.CuponPorcentajeValidators;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using SWCE.IOC;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Conexión a la BD
        var connectionString = builder.Configuration.GetConnectionString("E-commerceConnection");
        builder.Services.AddDbContext<E_commerceContext>(options =>
            options.UseSqlServer(connectionString));

        // Inyección de dependencias
        builder.Services.AddServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Middleware pipeline
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