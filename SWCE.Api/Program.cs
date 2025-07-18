using Microsoft.EntityFrameworkCore;
using SWCE.Aplication.Base;
using SWCE.Aplication.Interfaces.Services;
using SWCE.Aplication.Services;
using SWCE.Aplication.Extension.Validators;
using SWCE.Aplication.Extension.Validators.EnvioValidator;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using SWCE.IOC;
using SWCE.Aplication.Extension.MapeoEnvio;

public class Program
{
    private static void Main(string[] args)
    
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //Conexion a la BD
        var connectionString = builder.Configuration.GetConnectionString("E-CommerceConnection");
        builder.Services.AddDbContext<E_commerceContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddEnvioDependencies();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
