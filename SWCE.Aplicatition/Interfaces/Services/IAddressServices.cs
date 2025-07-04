using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;

namespace SWCE.Aplicatition.Interfaces.Services
{
    public interface IAddressServices : IServiceBase<Address, CreateAddressDto, UpdateOrDisableAddressDto>
    {
        Task<OperationResult> Updateasync(UpdateOrDisableAddressDto entity);
        Task<OperationResult> GetbyPredeterminada(int userid);
        Task<OperationResult> GetbyUserId(int userId);
        

    }
}
