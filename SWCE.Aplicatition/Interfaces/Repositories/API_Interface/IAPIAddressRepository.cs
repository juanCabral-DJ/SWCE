using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.API_Interface
{
    public interface IAPIAddressRepository
    {
        Task<OperationResult> GetAllAddressesAsync();
        Task<OperationResult> GetAddressByIdAsync(int id);
        Task<OperationResult> GetAddressByUserIdAsync(int userId);
        Task<OperationResult> GetByUseridAdressPredeterminada(int userId);
        Task<OperationResult> CreateAddressAsync(CreateAddressDto address);
        Task<OperationResult> UpdateAddressAsync(UpdateOrDisableAddressDto address);
        Task<OperationResult> DisableAddressAsync(UpdateOrDisableAddressDto address);

    }
}
