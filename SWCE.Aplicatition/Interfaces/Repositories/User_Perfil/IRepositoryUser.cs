using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;

namespace SWCE.Aplicatition.Interfaces.Repositories.User_Perfil
{
    public interface IRepositoryUser
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(User entity);
        Task<OperationResult> Updateasync(User entity);
        Task<OperationResult> Disableasync(User entity);
        Task<OperationResult> GetByEmail(string email);
    }
}
