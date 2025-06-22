using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWCE.Aplicatition.Validators.AddressValidator;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SWCE.Persistence.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IRepositoryAddress
    {
        private readonly CreateAddressValidator _Validator;
        private readonly E_commerceContext _Context;
        private readonly ILoggerBase<Address> _logger;

        public AddressRepository(E_commerceContext _context, ILoggerBase<Address> _logger, CreateAddressValidator Validator)
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
                _logger.LogInformation("Retrieving Address entities");
               var address = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving Address entities", address);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Address entities", e);
                result = OperationResult.Failure("An error occurred while retrieving Address entities.");
            } 

            return result;

        }
        public async override Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Address entities");
                var adresses = await base.GetAllasync();

                result = OperationResult.Success("Retrieving Address entities", adresses);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Address entities", e);
                result = OperationResult.Failure("An error occurred while retrieving Address entities.");
            }

            return result;
        }
        public async override Task<OperationResult> Createasync(Address entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Adding Address entity: ${@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null Address entity");
                    return OperationResult.Failure("Address entity cannot be null");
                }
                var validationresult = await _Validator.ValidateAsync(entity);

                if (!validationresult.IsValid)
                {
                    _logger.LogError("Address entity validation failed");
                    return OperationResult.Failure("Validation failed: " + string.Join(", ", validationresult.Errors.Select(e => e.ErrorMessage)));
                }

               await base.Createasync(entity);

                _logger.LogInformation("Adding Address entity: ${@Entity}", entity);
                result = OperationResult.Success("Address entity added successfully.", entity);

                return result;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while adding the Address type: {ex.Message}";
                _logger.LogError("An error occurred while adding the Address type: {Message}", ex);
            }
            finally
            {

            }

            return result;
        }
        public async override Task<OperationResult> Updateasync(Address entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("updating Address entity: ${@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null Address entity");
                    return OperationResult.Failure("Address entity cannot be null");
                }

                await base.Updateasync(entity);

                _logger.LogInformation("updating Address entity: ${@Entity}", entity);
                result = OperationResult.Success("Address entity added successfully.", entity);

                return result;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while updating the Address type: {ex.Message}";
                _logger.LogError("An error occurred while updating the Address type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }
        public async override Task<bool> ExistsAsync(Expression<Func<Address, bool>> filter)
        {
            return await base.ExistsAsync(filter);
        }
        public async Task<OperationResult> GetbyPredeterminada(int userid, bool predeterminada)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Address entities for UserId and Predeterminada");
                var address = await _Context.Direcciones
                            .Where(a => a.id_user == userid && a.Es_predeterminada == predeterminada)
                            .FirstOrDefaultAsync();

                if (address != null)
                {
                    return OperationResult.Success("Default address retrieved successfully.", address);
                }
                else
                {
                    return OperationResult.Failure("Default address not found.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Address entities", e);
               return OperationResult.Failure("An error occurred while retrieving Address entity.");
            }
        }

        public async Task<OperationResult> GetbyUserId(int userId)
        {

            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Address entities for UserId");
                var addresses = await _Context.Direcciones
                            .Where(a => a.id_user == userId)
                            .ToListAsync();


                return OperationResult.Success("Retrieving Address entity", addresses);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Address entities", e);
                return OperationResult.Failure("An error occurred while retrieving Address entity.");
            }
        }
    }
}


