using Microsoft.EntityFrameworkCore;
using SWCE.Aplicatition.Extension.Validators_Registro.WishListItemValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;

using System.Linq.Expressions;

namespace SWCE.Persistence.Repositories
{
    public class WishListItemRepository : RepositoryBase<WishListItem> ,IRepositoryWishListItem
    {
         
        private readonly E_commerceContext _Context;
        private readonly ILoggerBase<WishListItem> _logger;

        public WishListItemRepository(E_commerceContext _context, ILoggerBase<WishListItem> logger)
            : base(_context)
        {
            
            _Context = _context;
            _logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<WishListItem, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving item entities");
                var items  = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving item entities", items.Data);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving item entities", e);
                result = OperationResult.Failure("An error occurred while retrieving item entities.");
            }

            return result;
        }
        public async Task<OperationResult> DisableAsync(WishListItem entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("item entity not found.");
                }

                WishListItem item = await _Context.Lista_Deseos.FindAsync(entity.id);
                _logger.LogInformation("deleting  wishlistitem entity");

                if (item is null)
                {
                    _logger.LogError("item not found.");
                    return OperationResult.Failure("item entity not found.");
                }

                item.IsDeleted = true;
                result = await base.Updateasync(item);

                _logger.LogInformation("item con ID {Id} deshabilitada con éxito.", entity.id);
                result = OperationResult.Success("item deshabilitado con éxito.",  item);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos");
                _logger.LogError("An error occurred while deleting the WishListItem type: {Message}", ex);
            }
            return result;

        }
        public async override Task<OperationResult> Createasync(WishListItem entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Adding WishListItem entity: ${@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null WishListItem entity");
                    return OperationResult.Failure("WishListItem entity cannot be null");
                }

                await base.Createasync(entity);

                _logger.LogInformation("Adding WishListItem entity: ${@Entity}", entity);
                return OperationResult.Success("WishListItem entity added successfully.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding the WishListItem type: {Message}", ex);
                return OperationResult.Failure($"An error occurred while adding the WishListItem type: {ex.Message}");
            }
            finally
            {

            }
        }

        public  async Task<OperationResult> GetbyUserid(int userId)
        {

            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving WishListItem entities for UserId");

                if (userId == 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var items = await _Context.Lista_Deseos
                            .Where(a => a.Id_Usuario == userId && a.IsDeleted == false)
                            .ToListAsync();

                return OperationResult.Success("Retrieving WishListItem entity",items);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving WishListItem entities", e);
                return OperationResult.Failure("An error occurred while retrieving WishListItem entity.");
            }
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Retrieving Item entities");

                if(id == 0)
                {
                    return OperationResult.Failure("El id debe ser positivo");
                }
                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving Item entity", entity.Data);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;
        }
    }
}
