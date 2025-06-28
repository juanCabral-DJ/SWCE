using Microsoft.EntityFrameworkCore;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
using SWCE.Aplicatition.Validators;
using SWCE.Aplicatition.Validators.AddressValidator;
using SWCE.Aplicatition.Validators.WishListItemValidator;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;

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

            //Validaciones
            builder.Services.AddTransient<CreateUserValidator>();
            builder.Services.AddTransient<UpdateUserValidator>();
            builder.Services.AddTransient<CreateAddressValidator>();
            builder.Services.AddTransient<CreateWishListItemValidator>();

            //Mapeo
            builder.Services.AddSingleton<AddressMapper, AddressMapper>();
            builder.Services.AddSingleton<UserMapper, UserMapper>();
            builder.Services.AddSingleton<Itemmapper, Itemmapper>();

            //Repositorio
            builder.Services.AddScoped<IRepositoryUser, UserRepository>();
            builder.Services.AddTransient<IUserServices, UserServices>();

            builder.Services.AddScoped<IRepositoryAddress, AddressRepository>();
            builder.Services.AddTransient<IAddressServices, AddressServices>();

            builder.Services.AddScoped<IRepositoryWishListItem, WishListItemRepository>();
            builder.Services.AddTransient<IWishListItemServices, WishListItemServices>();

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
}
