

using AutoMapper;
using FluentNHibernate.Automapping;
using Microsoft.Extensions.Configuration;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;

namespace SWCE.Aplicatition.Services
{
    public sealed class AddressServices : IAddressServices
    {
  
        private readonly IRepositoryAddress _Address;
        private readonly ILoggerBase<AddressServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly AddressMapper _mapper;

        public AddressServices(IRepositoryAddress address, ILoggerBase<AddressServices> logger, AddressMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _Address = address;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateAddressDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("creating address entity");

              //Falta mapear
              var address = _mapper.MapToEntityCreate(entity);

                result = await _Address.Createasync(address);

                _logger.LogInformation("succefully created Address");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating Address", ex);
                result = OperationResult.Failure("An error occurred while creating Address");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Disableasync(DisableAddressDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                _logger.LogInformation("Disabling address with ID {id}.", entity.Id);

                //Falta mapear
                Address addressExist = new Address()
                {
                    id = entity.Id,            
                };

                result = await _Address.DisableAsync(addressExist);

                _logger.LogInformation("Successfully disabled address with ID {id}.", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while disabling address.", ex);
                result = OperationResult.Failure("An error occurred while disabling the address.");
            }
            return result;
        }

        public async Task<OperationResult> GetAllasync(Expression<Func<Address, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching all Address.");

                  result = await _Address.GetAllasync(filter);

                result = OperationResult.Success("Address retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Address.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Address.", ex);
                result = OperationResult.Failure("An error occurred while getting Address.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Getbyidasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching Address by id.");

                result = await _Address.GetbyIdasync(id);

                result = OperationResult.Success("Address retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Address.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Address.", ex);
                result = OperationResult.Failure("An error occurred while getting Address.");
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
                _logger.LogInformation("Fetching Address by predeterminada.");

                result = await _Address.GetbyPredeterminada(userid);

                result = OperationResult.Success("Address retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Address predeterminada.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Address predeterminada.", ex);
                result = OperationResult.Failure("An error occurred while getting Address predeterminada.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetbyUserId(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching All Address by id.");

                result = await _Address.GetbyUserId(userId);

                result = OperationResult.Success("Address retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Address.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Address.", ex);
                result = OperationResult.Failure("An error occurred while getting Address.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateAddressDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                //Falta mapear
               var direccion = _mapper.MapToEntity(entity);
               result = await _Address.Updateasync(direccion);
                _logger.LogInformation("Creating Address");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating Address", ex);
                result = OperationResult.Failure("An error occurred while creating Address");
            }
            finally
            {

            }
            return result;
        }
    }
}
