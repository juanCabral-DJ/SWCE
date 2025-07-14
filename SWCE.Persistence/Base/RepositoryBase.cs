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

        public virtual async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity != null)
                {
                    result = OperationResult.Success("Entity retrieved successfully.", entity);
                }
                else
                {
                    result = OperationResult.Failure($"Entity with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }

        public virtual async Task<OperationResult> GetAllasync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var entities = await Entity.Where(filter).ToListAsync();

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
                result = OperationResult.Success("Entidad creada con éxito.", entity);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ha ocurrido un error al guardar los datos", ex);
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
                result = OperationResult.Success("Entidad actualizada con éxito.", entity);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al actualizar los datos", ex);
            }
            return result;
        }


    }

}


