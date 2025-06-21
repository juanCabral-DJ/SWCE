using Microsoft.EntityFrameworkCore;
using SWCE.Aplicatition.Base;
using SWCE.Domain.Base;
using SWCE.Persistence.Context;
using System.Linq.Expressions;

namespace SWCE.Persistence.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly E_commerceContext _context;
        private DbSet<TEntity> Entity { get; set; }
        public RepositoryBase(E_commerceContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }
        public virtual async Task<OperationResult> Createasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Ha ocurrido un error al guardar los datos";
            }
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllasync()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetbyIdasync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> Updateasync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al actualizar los datos";
            }
            return result;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }
    }
}
