using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_address;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_Item;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_User;
using SWCE.Aplicatition.Extension.Validators_Registro.AddressValidator;
using SWCE.Aplicatition.Extension.Validators_Registro.UserValidator;
using SWCE.Aplicatition.Extension.Validators_Registro.WishListItemValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.IOC
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {

            //Validaciones
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<CreateAddressValidator>();
            services.AddTransient<CreateWishListItemValidator>();

            //Repositorio
            services.AddScoped<IRepositoryUser, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();

            services.AddScoped<IRepositoryAddress, AddressRepository>();
            services.AddTransient<IAddressServices, AddressServices>();

            services.AddScoped<IRepositoryWishListItem, WishListItemRepository>();
            services.AddTransient<IWishListItemServices, WishListItemServices>();

            services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));
 
        }
    }
}
