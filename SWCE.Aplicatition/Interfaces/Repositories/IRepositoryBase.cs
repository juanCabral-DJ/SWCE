using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SWCE.Domain.Base;

namespace SWCE.Aplication.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(TEntity entity);
        Task<OperationResult> Updateasync(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}

