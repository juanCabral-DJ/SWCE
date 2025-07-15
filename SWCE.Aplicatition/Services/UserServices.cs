 
using Microsoft.Extensions.Configuration;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_User;
using SWCE.Aplicatition.Extension.Validators_Registro.UserValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;


namespace SWCE.Aplicatition.Services
{
    public sealed class UserServices : IUserServices
    {
        private readonly CreateUserValidator _Validator;
        private readonly UpdateUserValidator _ValidatorUpdate;
        private readonly IRepositoryUser _repository;
        private readonly ILoggerBase<UserServices> _logger;
        private readonly IConfiguration _configuration;

        public UserServices(IRepositoryUser repository, ILoggerBase<UserServices> logger, IConfiguration configuration
            , CreateUserValidator Validator, UpdateUserValidator ValidatorUpdate)
        {
            _Validator = Validator;
            _ValidatorUpdate = ValidatorUpdate;
            _repository = repository; 
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching User by id.");

                result = await _repository.GetbyIdasync(id);

                result = OperationResult.Success("User retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched User.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting User.", ex);
                result = OperationResult.Failure("An error occurred while getting User.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching all User.");

                result = await _repository.GetAllasync();

                result = OperationResult.Success("User retrieved successfully.",result.Data);
                _logger.LogInformation("Successfully fetched User.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting User.", ex);
                result = OperationResult.Failure("An error occurred while getting User.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Createasync(CreateUserDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("creating User entity");

                //Falta mapear
                var User = UserMapper.MapToEntityCreate(entity);
                var entityvalidate = await _Validator.ValidateAsync(User);

                if (!entityvalidate.IsValid)
                {
                   
                    var errorMessages = string.Join(", ", entityvalidate.Errors.Select(e => e.ErrorMessage));
                    _logger.LogError("User validation failed");
                    return OperationResult.Failure("La validación falló: " + errorMessages);
                }
                result = await _repository.Createasync(User);

                _logger.LogInformation("succefully created User");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating User", ex);
                result = OperationResult.Failure("An error occurred while creating User");
            }
            finally
            {

            }
            return result;
        }

       public async Task<OperationResult> Updateasync(UpdateUserDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                _logger.LogInformation("Updating User with ID {id}.", entity.id);

                
               var User = UserMapper.MapToEntity(entity);
                var entityvalidate = await _ValidatorUpdate.ValidateAsync(User);

                if (!entityvalidate.IsValid)
                {
                    var errorMessages = string.Join(", ", entityvalidate.Errors.Select(e => e.ErrorMessage));
                    _logger.LogError("User validation failed");
                    return OperationResult.Failure("La validación falló: " + errorMessages);
                }
                result = await _repository.Updateasync(User);

                _logger.LogInformation("Successfully updated User with ID {id}.", entity.id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while Updating User.", ex);
                result = OperationResult.Failure("An error occurred while Updating the User.");
            }
            return result;
        }

        public async Task<OperationResult> Disableasync(DisableUserDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                _logger.LogInformation("Disabling User with ID {id}.", entity.id);

                //Falta mapear
                var User = UserMapper.MapToEntityDisable(entity);
                result = await _repository.Disableasync(User);

                _logger.LogInformation("Successfully disabled User with ID {id}.", entity.id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while disabling User.", ex);
                result = OperationResult.Failure("An error occurred while disabling the User.");
            }
            return result;
        }

        public async Task<OperationResult> GetByEmail(string email)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching User by email.");

                result = await _repository.GetByEmail(email);

                result = OperationResult.Success("User retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched User.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting User.", ex);
                result = OperationResult.Failure("An error occurred while getting User.");
            }
            finally
            {

            }
            return result;
        }

    }
}
