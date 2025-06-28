using SWCE.Aplicatition.Base;

using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Interfaces.Repositories.User_Perfil
{
    public interface IRepositoryAddress : IRepositoryBase<Address>
    {
        Task<OperationResult> GetbyPredeterminada(int userid);
        Task<OperationResult> GetbyUserId(int userId);

        Task<OperationResult> DisableAsync(Address entity);
    }
}
