using SWCE.Aplicatition.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.User_Perfil
{
    public interface IRepositoryWishListItem : IRepositoryBase<WishListItem>
    {
        public Task<OperationResult> GetbyUserid(int userId);
        Task<OperationResult> DisableAsync(WishListItem entity);
    }
}
