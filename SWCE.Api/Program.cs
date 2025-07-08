using Microsoft.EntityFrameworkCore;
using SWCE.Aplication.Base;
using SWCE.Aplication.Interfaces.Services;
using SWCE.Aplication.Services;
using SWCE.Aplication.Validators;
using SWCE.Aplication.Validators.EnvioValidator;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;

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

        //validaciones
        builder.Services.AddTransient<EnvioValidator>();
        builder.Services.AddTransient<UpdateEnvioValidator>();

        //Mapeo
        builder.Services.AddSingleton<EnvioMapper>();

        //Repositorios
        builder.Services.AddScoped<IEnvioRepository, EnvioRepository>();
        builder.Services.AddTransient<IEnvioServices, EnvioServices>();

        builder.Services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));


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
