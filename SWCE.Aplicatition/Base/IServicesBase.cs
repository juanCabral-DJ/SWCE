using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Base
{
    public interface IServiceBase<TEntity, TCreateDto, TUpdateDto>
    where TEntity : class
    where TCreateDto : class
    where TUpdateDto : class
    {
        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> Createasync(TCreateDto entity);
        Task<OperationResult> Disableasync(TUpdateDto entity);
        Task<OperationResult> Getbyidasync(int id);
    }
}
