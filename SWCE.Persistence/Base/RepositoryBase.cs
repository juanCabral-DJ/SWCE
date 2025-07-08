using Microsoft.EntityFrameworkCore;
using SWCE.Aplication.Base;
using SWCE.Persistence.Context;
using System.Linq.Expressions;
using SWCE.Domain.Base;

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
                    return OperationResult.Success("Obtuvo la entidad correctamente", entity);
                }
                else
                {
                    return OperationResult.Failure($"La entidad con Id {id} no ha sido encontrada");
                }
            }
            catch (Exception ex) 
            {
                return OperationResult.Failure($"Ha ocurrido un error mientras se recuperaba la entidad con el id {id}: {ex.Message}");
            }
        }
        public virtual async Task<OperationResult> GetAllasync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var entities = await Entity.ToListAsync();

                    return OperationResult.Success("Obtuvo la entidad correctamente", entities);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Ha ocurrido un error mientras se recuperaban todas las entidades: {ex.Message}");
            }
        }  
        public virtual async Task<OperationResult> Createasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "La entidad ha sido creada con exito";
                result.Data = entity;
            }
            catch (Exception)
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
                result.IsSuccess = true;
                result.Message = "La entidad ha sido actualizada con exito";
                result.Data = entity;
            }
            catch (Exception)
            {
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al actualizar los datos";
            }
            return result;
        }

    }
}

