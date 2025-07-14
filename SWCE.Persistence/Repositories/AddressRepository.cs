
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Aplicatition.Extension.Validators_Registro.AddressValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;

using System.Linq.Expressions;


namespace SWCE.Persistence.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IRepositoryAddress
    {
         
        private readonly E_commerceContext _Context;
        private readonly ILoggerBase<Address> _logger;

        public AddressRepository(E_commerceContext _context, ILoggerBase<Address> logger)
            : base(_context)
        {
          
            _Context = _context;
            _logger = logger;
        }

        public async override Task<OperationResult> GetAllasync(Expression<Func<Address, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving address entities");
                var Addresses = await base.GetAllasync(filter);

                result = OperationResult.Success("Retrieving Address entities", Addresses);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving address entities", e);
                result = OperationResult.Failure("An error occurred while retrieving address entities.");
            }

            return result;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Retrieving address entities");

                if (id <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var entity = await base.GetbyIdasync(id);

                result = OperationResult.Success("Retrieving Address entity", entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while retrieving entity by ID {id}: {ex.Message}");
            }
            return result;  
        }

        public async Task<OperationResult> DisableAsync(Address entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity is null)
                {
                    return OperationResult.Failure("Address entity cannot be null");
                }

                Address? addressexist = await _Context.Direcciones.FindAsync(entity.id);

                if (addressexist is null)
                {
                    _logger.LogError("Address entity not found");
                    return OperationResult.Failure("Address entity not found.");
                }

                _logger.LogInformation("deleting  Address entity");

                addressexist.IsDeleted = true;
                result = await base.Updateasync(addressexist);

                _logger.LogInformation("Address con ID {Id} deshabilitada con éxito.", entity.id);

                result = OperationResult.Success("Address entity deleted successfully.", addressexist);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure("Ocurrió un error al eliminar los datos", ex);
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
                    _logger.LogError("Attempted to add a null Address entity.");
                    return OperationResult.Failure("Address entity cannot be null.");
                }

                return await base.Createasync(entity);

            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while adding the Address type: {ex.Message}");
                _logger.LogError("An error occurred while adding the Address");
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
                if (entity == null)
                {
                    _logger.LogError("Attempted to update a null Address entity.");
                    return OperationResult.Failure("Address entity cannot be null");
                }

                _logger.LogInformation("updating Address entity: ${@Entity}", entity);

                Address addressupdate = await _Context.Direcciones.FindAsync(entity.id);

                if (addressupdate is null)
                    return OperationResult.Failure("InsuranceProvider entity not found.");

                addressupdate.Es_predeterminada = false;

                result = await base.Updateasync(addressupdate);
            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"An error occurred while updating the Address type: {ex.Message}");
                _logger.LogError("An error occurred while updating the Address type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }
        public async Task<OperationResult> GetbyPredeterminada(int userid)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Address entities for UserId and Predeterminada");

                if (userid <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }

                var address = await _Context.Direcciones
                            .Where(a => a.ID_Usuario == userid && a.Es_predeterminada == true)
                            .FirstOrDefaultAsync();

                if (address != null)
                {
                    result = OperationResult.Success("Default address retrieved successfully.", address);
                }
                else
                {
                    result = OperationResult.Failure("Default address not found.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Address entities", e);
               result = OperationResult.Failure("An error occurred while retrieving Address entity.");
            }
            return result;
        }

        public async Task<OperationResult> GetbyUserId(int userId)
        {

            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Address entities for UserId");

                if (userId <= 0)
                {
                    return OperationResult.Failure("El id tiene que ser positivo");
                }  

                var addresses = await _Context.Direcciones
                            .Where(a => a.ID_Usuario == userId)
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


