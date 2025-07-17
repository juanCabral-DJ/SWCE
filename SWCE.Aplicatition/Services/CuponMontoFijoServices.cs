using Microsoft.Extensions.Configuration;
using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Application.Extension.MappersAdministrationModule;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;

namespace SWCE.Application.Services
{
    public sealed class CuponMontoFijoService : ICuponMontoFijoServices
    {
        private readonly IRepositorioCuponMontoFijo _cuponRepo;
        private readonly ILoggerBase<CuponMontoFijoService> _logger;
        private readonly IConfiguration _configuration;

        public CuponMontoFijoService(
            IRepositorioCuponMontoFijo cuponRepo,
            ILoggerBase<CuponMontoFijoService> logger,
            IConfiguration configuration)
        {
            _cuponRepo = cuponRepo;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateCuponMontoFijoDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Creando cupon monto fijo");

                var cupon = CuponMontoFijoMapper.MapToEntityCreate(entity);

                result = await _cuponRepo.Createasync(cupon);

                _logger.LogInformation("Cupon monto fijo creado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error creando CuponMontoFijo", ex);
                result = OperationResult.Failure("Ha ocurrido un error creando el cupon");
            }

            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateCuponMontoFijoDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Actualizando cupon monto fijo");

                var cupon = CuponMontoFijoMapper.MapToEntityUpdate(entity);

                result = await _cuponRepo.Updateasync(cupon);

                _logger.LogInformation("Cupon monto fijo actualizado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error actualizando CuponMontoFijo", ex);
                result = OperationResult.Failure("Ha ocurrido un error actualizando CuponMontoFijo");
            }

            return result;
        }

        public async Task<OperationResult> GetbyId(int id)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation($"Buscando CuponMontoFijo con ID {id}");

                result = await _cuponRepo.GetbyIdasync(id);

                _logger.LogInformation("CuponMontoFijo encontrado satisfactoriamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error buscando CuponMontoFijo por ID", ex);
                result = OperationResult.Failure("Ha ocurrido un error buscando el cupon");
            }

            return result;
        }

        public async Task<OperationResult> GetAllasync()
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Buscando todos los cupones de monto fijo");

                result = await _cuponRepo.GetAllasync(x => true); // Traer todos

                _logger.LogInformation("Se han encontrado todos los CuponMontoFijo");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error buscando CuponMontoFijo", ex);
                result = OperationResult.Failure("Ha ocurrido un error buscando los copones");
            }

            return result;
        }

        public async Task<OperationResult> Disableasync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando cupón con ID: {Id}", id);

                var result = await _cuponRepo.GetbyIdasync(id);

                if (!result.IsSuccess)
                    return OperationResult.Failure("Cupón no encontrado");

                if (result.Data is not CuponMontoFijo cupon)
                    return OperationResult.Failure($"Cupón inválido: tipo esperado CuponMontoFijo, recibido {result.Data?.GetType().Name}");

                cupon.IsDeleted = true;

                var updateResult = await _cuponRepo.Updateasync(cupon);

                if (!updateResult.IsSuccess)
                    return OperationResult.Failure("No se pudo deshabilitar el cupón");

                _logger.LogInformation("Cupón deshabilitado exitosamente.");
                return OperationResult.Success("Cupón deshabilitado correctamente", cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar cupón", ex);
                return OperationResult.Failure("Error interno al deshabilitar cupón", ex);
            }
        }

        public Task<OperationResult> GetAllAsync(Expression<Func<CuponMontoFijo, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}

