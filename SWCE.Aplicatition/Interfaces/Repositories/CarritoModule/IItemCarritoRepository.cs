using SWCE.Application.Base;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Repositories.CarritoModule
{
    public interface IItemCarritoRepository : IRepositoryBase<ItemCarrito>
    {

        Task<List<GetItemCarritoDto>> GetItemsByCarritoIdAsync(int carritoId);

        public Task<OperationResult> AddItemAsync(ItemCarrito item);

        public Task<OperationResult> RemoveItemAsync(int itemId);
        Task<OperationResult> ClearItemsByCarritoIdAsync(int carritoId);



    }
}
