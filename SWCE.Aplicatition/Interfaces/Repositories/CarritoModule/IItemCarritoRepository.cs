using SWCE.Application.Base;
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

        public Task<OperationResult> GetItemsByCartIdAsync(int carritoId);

        public Task<OperationResult> AddItemAsync(ItemCarrito item);

        public Task<OperationResult> UpdateItemAsync(ItemCarrito item);

        Task<OperationResult> UpdateItemQuantityAsync(int itemId, int newQuantity);
        public Task<OperationResult> RemoveItemAsync(int itemId);
        Task<OperationResult> ClearItemsByCarritoIdAsync(int carritoId);

    }
}
