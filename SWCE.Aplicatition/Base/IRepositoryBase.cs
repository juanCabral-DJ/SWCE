using SWCE.Domain.Base;
 
 
using System.Linq.Expressions; 

namespace SWCE.Aplicatition.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> Createasync(TEntity entity);
        Task<OperationResult> Updateasync(TEntity entity);
 
    }
}
