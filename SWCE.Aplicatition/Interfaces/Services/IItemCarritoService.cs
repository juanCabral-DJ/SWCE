using SWCE.Application.Base;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface IItemCarritoService : IServiceBase<ItemCarrito>
    {
        Task<OperationResult> AddItemToCarritoAsync(AddItemCarritoDto dto);
        Task<OperationResult> UpdateItemCantidadAsync(UpdateItemCantidadDto dto);
        Task<OperationResult> RemoveItemFromCarritoAsync(int itemId);
        Task<OperationResult> GetItemsByCarritoIdAsync(int carritoId);
    }
}
