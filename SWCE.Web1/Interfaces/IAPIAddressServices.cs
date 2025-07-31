using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using System.Text.Json;

namespace SWCE.Web1.Interfaces
{
    public interface IAPIAddressServices
    {
         Task<ModelResponse<CreateAddressModel>> CreateAddressAsync(CreateAddressModel address);

         Task<ModelResponse<DisableAddressModel>> DisableAddressAsync(DisableAddressModel address);

         Task<ModelResponse<AddressModel>> GetAddressByIdAsync(int id);

         Task<ModelResponse<List<AddressModel>>> GetAddressByUserIdAsync(int userId);

         Task<ModelResponse<List<AddressModel>>> GetAllAddressesAsync();

         Task<ModelResponse<AddressModel>> GetByUseridAdressPredeterminada(int userId);

         Task<ModelResponse<EditAddressModel>> UpdateAddressAsync(EditAddressModel address);
    }
}

