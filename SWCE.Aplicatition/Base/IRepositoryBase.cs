using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SWCE.Domain.Base;

namespace SWCE.Application.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetbyIdasync(int id);
        Task<List<TEntity>> GetAllasync();
        Task<OperationResult> Createasync(TEntity entity);
        Task<OperationResult> Updateasync(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}