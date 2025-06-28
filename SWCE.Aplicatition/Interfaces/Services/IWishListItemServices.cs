using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.WishListItem;
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
    public interface IWishListItemServices
    {
        Task<OperationResult> GetAllasync(Expression<Func<WishListItem, bool>> filter);
        Task<OperationResult> GetbyUserId(int userId);
        Task<OperationResult> Createasync(CreateItemDto entity);
        Task<OperationResult> Disableasync(DisableItemDto entity);
        Task<OperationResult> Getbyid(int id);
    }
}
