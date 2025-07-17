using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base
{
    public interface IServiceBase
    {
        public interface IServiceBase<TEntity, TCreateDto, TUpdateDto, TDisableDto>
        where TEntity : class
        where TCreateDto : class
        where TUpdateDto : class
        {
            Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
            Task<OperationResult> Createasync(TCreateDto entity);
            Task<OperationResult> Updateasync(TUpdateDto entity);
            Task<OperationResult> Disableasync(int id);
            Task<OperationResult> GetbyId(int id);
        }
    }
}
