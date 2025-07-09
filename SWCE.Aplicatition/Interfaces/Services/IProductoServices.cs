using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface IProductoServices
    {
        Task<OperationResult> GetbyId(int id);
        Task<OperationResult> GetAllasync(Expression<Func<Producto, bool>> filter);
        Task<OperationResult> Createasync(CreateProductoDto entity);
        Task<OperationResult> Updateasync(UpdateProductoDto entity);
        Task<OperationResult> DisableAsync(int productoId);
    }
}
