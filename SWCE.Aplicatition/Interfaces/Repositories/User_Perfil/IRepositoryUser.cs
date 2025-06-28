using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositoryUser 
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(CreateUserDto entity);
        Task<OperationResult> Updateasync(UpdateUserDto entity);
         Task<OperationResult> GetByEmail(string email);
    }
}
