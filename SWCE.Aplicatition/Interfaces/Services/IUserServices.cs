using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Services
{
    public interface IUserServices
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(CreateUserDto entity);
        Task<OperationResult> Updateasync(UpdateUserDto entity);
        Task<OperationResult> Disableasync(DisableUserDto entity);
        Task<OperationResult> GetByEmail(string email);
    }
}
