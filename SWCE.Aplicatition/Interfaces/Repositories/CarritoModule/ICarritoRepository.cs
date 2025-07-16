using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Repositories
{
    public interface ICarritoRepository
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(CreateCarritoDto entity);
        Task<OperationResult> Updateasync(UpdateCarritoDto entity);
        Task<bool> ExistsByIdAsync(int id);
        Task<OperationResult> GetByUserIdAsync(int userId);
        Task<OperationResult> DeleteAsync(DisableCarritoDto entity);
        Task<List<GetItemCarritoDto>> GetItemsByCarritoIdAsync(int carritoId);
    }
}

