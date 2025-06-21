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
        Task<User> GetbyIdasync(int id);
        Task<List<User>> GetAllasync();
        Task<OperationResult> Createasync(User entity);
        Task<OperationResult> Updateasync(User entity);
         Task<User> GetByEmail(string email);
        Task<bool> ExistsAsync(Expression<Func<User, bool>> filter);
    }
}
