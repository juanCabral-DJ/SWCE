using Microsoft.Extensions.Configuration;
using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Application.Extension.MappersAdministrationModule;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;

namespace SWCE.Application.Services
{
    public sealed class CuponPorcentajeService : ICuponPorcentajeServices
    {
        private readonly IRepositorioCuponPorcentaje _cuponRepo;
        private readonly ILoggerBase<CuponPorcentajeService> _logger;
        private readonly IConfiguration _configuration;

        public CuponPorcentajeService(
            IRepositorioCuponPorcentaje cuponRepo,
            ILoggerBase<CuponPorcentajeService> logger,
            IConfiguration configuration)
        {
            _cuponRepo = cuponRepo;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateCuponPorcentajeDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Creating percentage coupon");

                var cupon = CuponPorcentajeMapper.MapToEntityCreate(entity);

                result = await _cuponRepo.Createasync(cupon);

                _logger.LogInformation("Successfully created CuponPorcentaje");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating CuponPorcentaje", ex);
                result = OperationResult.Failure("An error occurred while creating the percentage coupon");
            }

            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateCuponPorcentajeDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Updating percentage coupon");

                var cupon = CuponPorcentajeMapper.MapToEntityUpdate(entity);

                result = await _cuponRepo.Updateasync(cupon);

                _logger.LogInformation("Successfully updated CuponPorcentaje");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating CuponPorcentaje", ex);
                result = OperationResult.Failure("An error occurred while updating the percentage coupon");
            }

            return result;
        }

        public async Task<OperationResult> GetbyId(int id)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation($"Fetching CuponPorcentaje with ID {id}");

                result = await _cuponRepo.GetbyIdasync(id);

                _logger.LogInformation("Successfully fetched CuponPorcentaje");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching CuponPorcentaje by ID", ex);
                result = OperationResult.Failure("An error occurred while fetching the coupon");
            }

            return result;
        }

        public async Task<OperationResult> GetAllasync()
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Fetching all CuponPorcentaje");

                result = await _cuponRepo.GetAllasync(x => true);

                _logger.LogInformation("Successfully fetched all CuponPorcentaje");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching CuponPorcentaje", ex);
                result = OperationResult.Failure("An error occurred while fetching the percentage coupons");
            }

            return result;
        }

        public async Task<OperationResult> Disableasync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando CuponPorcentaje con ID: {Id}", id);

                var result = await _cuponRepo.GetbyIdasync(id);
                if (!result.IsSuccess)
                    return result;

                var cupon = (CuponPorcentaje?)result.Data;
                cupon!.IsDeleted = true;

                var updateResult = await _cuponRepo.Updateasync(cupon!);
                return updateResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar CuponPorcentaje", ex);
                return OperationResult.Failure("Error interno al deshabilitar el cupón.");
            }
        }

        public Task<OperationResult> GetAllAsync(Expression<Func<CuponPorcentaje, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}

