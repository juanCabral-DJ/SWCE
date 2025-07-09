using SWCE.Aplication.Interfaces.Services;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using Microsoft.Extensions.Configuration;
using SWCE.Aplication.Base;
using SWCE.Infraestructure.Logging;
using SWCE.Aplication.DTOs.Envio;
using SWCE.Aplication.Extension.MapeoEnvio;

namespace SWCE.Aplication.Services
{
    public sealed class EnvioServices : IEnvioServices
    {
        private readonly IEnvioRepository _repository;
        private readonly ILoggerBase<EnvioEntity> _logger;
        private readonly IConfiguration _configuration;

        public EnvioServices(IEnvioRepository repository, ILoggerBase<EnvioEntity> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching Envio by Id.");

                result = await _repository.GetByIdAsync(Id);

                result = OperationResult.Success("Envio retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Envio.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Envio.", ex);
                result = OperationResult.Failure("An error occurred while getting Envio.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Recuperando todos los envios.");

                result = await _repository.GetAllAsync();

                result = OperationResult.Success("Envio recuperado satisfactoriamente.", result.Data);
                _logger.LogInformation("Successfully fetched envio.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error mientras se recuperaban los envios.", ex);
                result = OperationResult.Failure("An error occurred while getting Envio.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> CreateAsync(CreateEnvioDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Creating Envio entity");

                //Falta mapear
                var Envio = EnvioMapper.MapToEntityCreate(entity);
                result = await _repository.CreateAsync(Envio);

                _logger.LogInformation("Succefully created Envio");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating Envio", ex);
                result = OperationResult.Failure("An error occurred while creating Envio");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> UpdateAsync(UpdateEnvioDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                _logger.LogInformation("Updating Envio with Id {Id}.", entity.Id);

                //Falta mapear
                var Envio = EnvioMapper.MapToEntity(entity);
                result = await _repository.UpdateAsync(Envio);

                _logger.LogInformation("Successfully updated Envio with Id {Id}.", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while Updating Envio.", ex);
                result = OperationResult.Failure("An error occurred while Updating the Envio.");
            }
            return result;
        }

        public Task<OperationResult> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        
    }
}
