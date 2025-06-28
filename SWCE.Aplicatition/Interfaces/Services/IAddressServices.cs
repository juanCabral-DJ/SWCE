using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Services
{
    public interface IAddressServices
    {
        Task<OperationResult> GetAllasync(Expression<Func<Address, bool>> filter);
        Task<OperationResult> Createasync(CreateAddressDto entity);
        Task<OperationResult> Updateasync(UpdateAddressDto entity);
        Task<OperationResult> Disableasync(DisableAddressDto entity);
        Task<OperationResult> GetbyPredeterminada(int userid);
        Task<OperationResult> GetbyUserId(int userId);
        Task<OperationResult> Getbyidasync(int id);

    }
}
