using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Validators.CuponValidators.CuponPorcentajeValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public sealed class CuponPorcentajeRepository : RepositoryBase<CuponPorcentaje>, IRepositorioCuponPorcentaje
    {
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<Cupon> _logger;
        private readonly CreateCuponPorcentajeValidator _createValidator;
        private readonly UpdateCuponPorcentajeValidator _updateValidator;

        public CuponPorcentajeRepository(
            E_commerceContext context,
            ILoggerBase<Cupon> logger,
            CreateCuponPorcentajeValidator createValidator,
            UpdateCuponPorcentajeValidator updateValidator
        ) : base(context)
        {
            _context = context;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public override async Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo CuponPorcentaje con ID: {Id}", id);
                var cupon = await _context.Cupones
                    .OfType<CuponPorcentaje>()
                    .FirstOrDefaultAsync(c => c.id == id);

                if (cupon == null)
                    return OperationResult.Failure("Cupón no encontrado o no es del tipo CuponPorcentaje.");

                return OperationResult.Success("Cupón obtenido correctamente.", cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener CuponPorcentaje: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener el cupón.");
            }
        }


        public override async Task<OperationResult> GetAllasync(Expression<Func<CuponPorcentaje, bool>> filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los cupones de porcentaje.");
                var cupones = await _context.Cupones
                    .OfType<CuponPorcentaje>()
                    .Where(filter)
                    .ToListAsync();
                return OperationResult.Success("Cupones obtenidos correctamente.", cupones);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener cupones: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener los cupones.");
            }
        }

        public override async Task<OperationResult> Createasync(CuponPorcentaje entity)
        {
            try
            {
                var validation = await _createValidator.ValidateAsync(entity);
                if (!validation.IsValid)
                {
                    var errores = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                    return OperationResult.Failure("Validación fallida: " + errores);
                }

                return await base.Createasync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al crear el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al crear el cupón.");
            }
        }

        public override async Task<OperationResult> Updateasync(CuponPorcentaje entity)
        {
            try
            {
                var validation = await _updateValidator.ValidateAsync(entity);
                if (!validation.IsValid)
                {
                    var errores = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                    return OperationResult.Failure("Validación fallida: " + errores);
                }

                return await base.Updateasync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al actualizar el cupón.");
            }
        }

        public async Task<OperationResult> ObtenerPorPorcentaje(decimal porcentaje)
        {
            try
            {
                _logger.LogInformation("Buscando cupón de porcentaje: {Porcentaje}", porcentaje);
                var cupon = await _context.Cupones
                    .OfType<CuponPorcentaje>()
                    .FirstOrDefaultAsync(c => c.Porcentaje == porcentaje);

                if (cupon == null)
                    return OperationResult.Failure("Cupón no encontrado.");

                return OperationResult.Success("Cupón encontrado.", cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al buscar el cupón por porcentaje: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al buscar el cupón.");
            }
        }

        public async Task<OperationResult> DisableAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando CuponPorcentaje con ID: {Id}", id);

                var cupon = await _context.Cupones
                    .OfType<CuponPorcentaje>()
                    .FirstOrDefaultAsync(c => c.id == id);

                if (cupon == null)
                {
                    _logger.LogError("Cupón no encontrado o no es de tipo CuponPorcentaje");
                    return OperationResult.Failure("Cupón no encontrado o inválido");
                }

                cupon.IsDeleted = true;

                _context.Update(cupon);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cupón deshabilitado exitosamente.");
                return OperationResult.Success("Cupón deshabilitado correctamente.", cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al deshabilitar el cupón.", ex);
            }
        }

    }
}

