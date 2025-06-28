using AutoMapper;
using Microsoft.Extensions.Configuration;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Services
{
    public sealed class UserServices : IUserServices
    {
        private readonly IRepositoryUser _repository;
        private readonly UserMapper _mapper;
        private readonly ILoggerBase<User> _logger;
        private readonly IConfiguration _configuration;

        public UserServices(IRepositoryUser repository, UserMapper mapper, ILoggerBase<User> logger, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
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
                _logger.LogInformation("Successfully fetched Address.");
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
                 var User = _mapper.MapToEntityCreate(entity);
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

               //Falta mapear
               var User = _mapper.MapToEntity(entity);
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
                var User = new User()
                {
                    id = entity.id,
                };
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
