using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICategoriaServices
    {
        Task<OperationResult> GetbyId(int id);
        Task<OperationResult> GetAllasync(Expression<Func<Categoria, bool>> filter);
        Task<OperationResult> Createasync(CreateCategoriaDto entity);
        Task<OperationResult> Updateasync(UpdateCategoriaDto entity);
        Task<List<Categoria>> ObtenerActivasAsync();
        Task<OperationResult> DisableAsync(int id);
    }
}
