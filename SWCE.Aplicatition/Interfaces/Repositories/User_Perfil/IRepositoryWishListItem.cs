using SWCE.Aplicatition.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositoryWishListItem : IRepositoryBase<WishListItem>
    {
        public Task<List<WishListItem>> GetbyUserid(int userId);
    }
}
