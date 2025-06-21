using Microsoft.EntityFrameworkCore;
using SWCE.Aplicatition.Base;
using SWCE.Domain.Base;
using SWCE.Persistence.Context;
using System.Data.Common;
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

        public virtual async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity != null)
                { 
                    return OperationResult.Success("Entity retrieved successfully.", entity);
                }
                else
                {
                    return OperationResult.Failure($"Entity with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> GetAllasync()
        {
            try
            {
                var entities = await Entity.ToListAsync();

                return OperationResult.Success("Entities retrieved successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving all entities: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> Createasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Ha ocurrido un error al guardar los datos";
            }
            return result;
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

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }
    }

}


