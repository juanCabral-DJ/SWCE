using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class WishListItemRepository : RepositoryBase<WishListItem> ,IRepositoryWishListItem
    {
        private readonly E_commerceContext _Context;
        
        public WishListItemRepository(E_commerceContext context) : base(context) 
        {
            _Context = context;
            
        }

        public async Task<OperationResult> Createasync(WishListItem entity)
        {
            return await base.Createasync(entity);
            
        }

        public async Task<List<WishListItem>> GetAllasync()
        {
            return await _Context.Lista_Deseos.Where(w => w.IsDeleted == false).ToListAsync();
        }

        public async Task<WishListItem> GetbyIdasync(int id)
        {
            return await _Context.Lista_Deseos.FindAsync(id);
        }

        public  async Task<List<WishListItem>> GetbyUserid(int userId)
        {
            return await _Context.Lista_Deseos.Where(w => w.id_user == userId).ToListAsync();
        }

        public async Task<OperationResult> Updateasync(WishListItem entity)
        {
            return await base.Updateasync(entity);
        }
        public async Task<bool> ExistsAsync(Expression<Func<WishListItem, bool>> filter)
        {
            return await _Context.Lista_Deseos.AnyAsync(filter);
        }
    }
}
