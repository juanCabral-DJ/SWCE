using Microsoft.EntityFrameworkCore;
using SWCE.IOC;
using SWCE.Persistence.Context;

namespace SWCE.Api
{
    public class Program {

        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Conección a la bd
            var connectionString = builder.Configuration.GetConnectionString("E-commerceConnection");
            builder.Services.AddDbContext<E_commerceContext>(options =>
    options.UseSqlServer(connectionString));

            builder.Services.AddServices();

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
}
