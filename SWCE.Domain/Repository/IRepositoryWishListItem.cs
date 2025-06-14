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
        public List<WishListItem> GetbyUserid(int userId);
        public List<WishListItem> GetByUserIdAndProductId(int userId, int productId);
    }
}
