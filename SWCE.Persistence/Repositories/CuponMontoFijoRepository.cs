using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Application.Extension.Validators.CuponValidators.CuponMontoFijoValidators;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
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
    public sealed class CuponMontoFijoRepository : RepositoryBase<CuponMontoFijo>, IRepositorioCuponMontoFijo
    {
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<Cupon> _logger;
        private readonly CreateCuponMontoFijoValidator _createValidator;
        private readonly UpdateCuponMontoFijoValidator _updateValidator;

        public CuponMontoFijoRepository(
            E_commerceContext context,
            ILoggerBase<Cupon> logger,
            CreateCuponMontoFijoValidator createValidator,
            UpdateCuponMontoFijoValidator updateValidator
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
                _logger.LogInformation("Intentando obtener cupón por ID: {Id}", id);

                var cupon = await _context.Cupones.FirstOrDefaultAsync(c => c.id == id);

                if (cupon == null)
                {
                    _logger.LogInformation("No se encontró ningún cupón con ID: {Id}", id);
                    return OperationResult.Failure("Cupón no encontrado.");
                }

                _logger.LogInformation("Tipo del cupón: {Tipo}", cupon.GetType().Name);

                if (cupon is not CuponMontoFijo cup)
                {
                    _logger.LogInformation("El cupón no es de tipo CuponMontoFijo, sino: {Tipo}", cupon.GetType().Name);
                    return OperationResult.Failure("Cupón no es de tipo CuponMontoFijo");
                }

                return OperationResult.Success("Cupón obtenido correctamente.", cup);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener el cupón.");
            }
        }

        public override async Task<OperationResult> GetAllasync(Expression<Func<CuponMontoFijo, bool>> filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los cupones de monto fijo.");
                var cupones = await _context.Cupones
                    .OfType<CuponMontoFijo>()
                    .ToListAsync();
                return OperationResult.Success("Cupones obtenidos correctamente.", cupones);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener cupones: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener los cupones.");
            }
        }

        public override async Task<OperationResult> Createasync(CuponMontoFijo entity)
        {
            try
            {
                if (entity is not CuponMontoFijo cupon)
                    return OperationResult.Failure("Entidad no válida para crear un cupón de monto fijo.");

                var validation = await _createValidator.ValidateAsync(cupon);
                if (!validation.IsValid)
                    return OperationResult.Failure("Validación fallida: " + string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

                return await base.Createasync(cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al crear el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al crear el cupón.");
            }
        }

        public override async Task<OperationResult> Updateasync(CuponMontoFijo entity)
        {
            try
            {
                if (entity is not CuponMontoFijo cupon)
                    return OperationResult.Failure("Entidad no válida para actualizar un cupón de monto fijo.");

                var validation = await _updateValidator.ValidateAsync(cupon);
                if (!validation.IsValid)
                    return OperationResult.Failure("Validación fallida: " + string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

                return await base.Updateasync(cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al actualizar el cupón.");
            }
        }

        public async Task<OperationResult> ObtenerPorMonto(decimal monto)
        {
            try
            {
                _logger.LogInformation("Buscando cupón de monto fijo con monto: {Monto}", monto);
                var cupon = await _context.Cupones
                    .OfType<CuponMontoFijo>()
                    .FirstOrDefaultAsync(c => c.Monto == monto);

                if (cupon == null)
                {
                    return OperationResult.Failure("No se encontró ningún cupón con ese monto.");
                }

                return OperationResult.Success("Cupón encontrado correctamente.", cupon);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al buscar cupón por monto: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al buscar el cupón.");
            }
        }

        public override async Task<bool> ExistsAsync(Expression<Func<CuponMontoFijo, bool>> filter)
        {
            return await base.ExistsAsync(filter);
        }

        public async Task<OperationResult> DisableAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando CuponMontoFijo con ID: {Id}", id);

                var cupon = await _context.Cupones
                    .OfType<CuponMontoFijo>()
                    .FirstOrDefaultAsync(c => c.id == id);

                if (cupon == null)
                {
                    _logger.LogError("Cupón no encontrado o no es de tipo CuponMontoFijo");
                    return OperationResult.Failure("Cupón no encontrado o inválido");
                }

                cupon.IsDeleted = true;

                _context.Update(cupon);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cupón deshabilitado exitosamente.");
                return OperationResult.Success("Cupón deshabilitado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar el cupón: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al deshabilitar el cupón.", ex);
            }
        }


    }
}

