using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SWCE.Aplicatition.Validators.AddressValidator;
using SWCE.Aplicatition.Validators.WishListItemValidator;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class WishListItemRepository : RepositoryBase<WishListItem> ,IRepositoryWishListItem
    {
        private readonly CreateWishListItemValidator _Validator;
        private readonly E_commerceContext _Context;
        private readonly ILoggerBase<WishListItem> _logger;

        public WishListItemRepository(E_commerceContext _context, ILoggerBase<WishListItem> _logger, CreateWishListItemValidator Validator)
            : base(_context)
        {
            _Validator = Validator;
            _Context = _context;
            _logger = _logger;
        }


        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving WishListItem entities");
                var item = await base.GetbyIdasync(id);

                return OperationResult.Success("Retrieving Address entities", item);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving WishListItem entities", e);
               return OperationResult.Failure("An error occurred while retrieving WishListItem entities.");
            }
        }
        public async override Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving WishListItem entities");
               var items = await base.GetAllasync();

                result = OperationResult.Success("Retrieving WishListItem entities", items);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving WishListItem entities", e);
                result = OperationResult.Failure("An error occurred while retrieving WishListItem entities.");
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
                var validationresult = await _Validator.ValidateAsync(entity);

                if (!validationresult.IsValid)
                {
                    _logger.LogError("WishListItem entity validation failed");
                    return OperationResult.Failure("Validation failed: " + string.Join(", ", validationresult.Errors.Select(e => e.ErrorMessage)));
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
        public async override Task<OperationResult> Updateasync(WishListItem entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("updating WishListItem entity: ${@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null WishListItem entity");
                    return OperationResult.Failure("WishListItem entity cannot be null");
                }

                await base.Updateasync(entity);

                _logger.LogInformation("updating WishListItem entity: ${@Entity}", entity);
                return OperationResult.Success("WishListItem entity added successfully.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating the WishListItem type: {Message}", ex);
                return OperationResult.Failure($"An error occurred while updating the WishListItem type: {ex.Message}");
            }
            finally
            {

            }
        }
        public async override Task<bool> ExistsAsync(Expression<Func<WishListItem, bool>> filter)
        {
            return await base.ExistsAsync(filter);
        }

        public  async Task<OperationResult> GetbyUserid(int userId)
        {

            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving WishListItem entities for UserId");
                var items = await _Context.Lista_Deseos
                            .Where(a => a.id_user == userId)
                            .FirstOrDefaultAsync();

                return OperationResult.Success("Retrieving WishListItem entity",items);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving WishListItem entities", e);
                return OperationResult.Failure("An error occurred while retrieving WishListItem entity.");
            }
        }
    }
}
