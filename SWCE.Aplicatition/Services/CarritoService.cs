using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Application.Interfaces.Repositories;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Services
{
    public sealed class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly IItemCarritoRepository _itemCarritoRepository;
        private readonly ILoggerBase<CarritoService> _logger;
        private readonly CreateCarritoValidator _createValidator;
        private readonly UpdateCarritoValidator _updateValidator;
        public CarritoService(
            ICarritoRepository carritoRepository,
            IItemCarritoRepository itemCarritoRepository, 
            ILoggerBase<CarritoService> logger,
            CreateCarritoValidator createValidator,
            UpdateCarritoValidator updateValidator)
        {
            _carritoRepository = carritoRepository;
            _itemCarritoRepository = itemCarritoRepository; 
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }



        public async Task<OperationResult> CreateAsync(CreateCarritoDto dto)
        {
            _logger.LogInformation("Creating carrito.");

            var validationResult = _createValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation failed when creating carrito");
                return OperationResult.Failure(errors);
            }

            try
            {
                var existingCarritoResult = await _carritoRepository.GetByUserIdAsync(dto.ID_Usuario);

                if (existingCarritoResult.IsSuccess && existingCarritoResult.Data != null)
                {
                    _logger.LogError("User already has an active carrito.");
                    return OperationResult.Failure("The user already has an active carrito.");
                }

                var creationResult = await _carritoRepository.Createasync(dto);

                if (creationResult.IsSuccess)
                {
                    _logger.LogInformation("Carrito created successfully.");
                }
                else
                {
                    _logger.LogError("Failed to create carrito.");
                }
                return creationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error during carrito creation.", ex);
                return OperationResult.Failure("An unexpected error occurred during carrito creation.");
            }
        }

        public async Task<OperationResult> DisableAsync(DisableCarritoDto dto)
        {
            _logger.LogInformation("Intentando deshabilitar el carrito.");

            if (dto.Id <= 0)
            {
                _logger.LogError("ID de carrito inválido para deshabilitar. Debe ser mayor que cero.");
                return OperationResult.Failure("El ID del carrito debe ser mayor que cero.");
            }

            try
            {
                var result = await _carritoRepository.DeleteAsync(dto);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Carrito deshabilitado exitosamente.");
                }
                else
                {
                    _logger.LogError("Fallo al deshabilitar el carrito.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error inesperado al deshabilitar el carrito.", ex);
                return OperationResult.Failure("Ocurrió un error inesperado al deshabilitar el carrito.");
            }
        }

        public async Task<OperationResult> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo todos los carritos.");
            try
            {
                var result = await _carritoRepository.GetAllasync();

                if (result.IsSuccess && result.Data is IEnumerable<GetCarritoDto> carritos)
                {
                    foreach (var carrito in carritos)
                    {
                        var items = await _itemCarritoRepository.GetItemsByCarritoIdAsync(carrito.Id);
                        carrito.Productos = items;
                    }

                    _logger.LogInformation("Todos los carritos obtenidos exitosamente con sus items.");
                    return OperationResult.Success("Carritos obtenidos con items.", carritos);
                }
                else
                {
                    _logger.LogError("Fallo al obtener todos los carritos.");
                    return OperationResult.Failure("Fallo al obtener todos los carritos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener todos los carritos.", ex);
                return OperationResult.Failure("Ocurrió un error inesperado al obtener todos los carritos.");
            }
        }

        public async Task<OperationResult> GetActiveCarritoByUserIdAsync(int userId)
        {
            _logger.LogInformation("Buscando carrito activo para el usuario.");

            if (userId <= 0)
            {
                _logger.LogError("ID de usuario inválido para buscar carrito activo. Debe ser mayor que cero.");
                return OperationResult.Failure("El ID del usuario debe ser mayor que cero.");
            }

            try
            {
                var carritoResult = await _carritoRepository.GetByUserIdAsync(userId);

                if (!carritoResult.IsSuccess || carritoResult.Data == null)
                {
                    _logger.LogInformation("No se encontró carrito activo para el usuario.");
                    return OperationResult.Failure("No se encontró un carrito activo para el usuario.");
                }

                var carritoDto = carritoResult.Data as GetCarritoDto;
                if (carritoDto == null)
                {
                    return OperationResult.Failure("Error al mapear el carrito.");
                }

                var items = await _itemCarritoRepository.GetItemsByCarritoIdAsync(carritoDto.Id);
                carritoDto.Productos = items;

                _logger.LogInformation("Carrito activo encontrado para el usuario con items.");
                return OperationResult.Success("Carrito activo encontrado con items.", carritoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error al buscar carrito activo para el usuario.", ex);
                return OperationResult.Failure("Ocurrió un error al buscar el carrito activo.");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo carrito por ID.");

            if (id <= 0)
            {
                _logger.LogError("ID de carrito inválido para obtener. Debe ser mayor que cero.");
                return OperationResult.Failure("El ID del carrito debe ser mayor que cero.");
            }

            try
            {
                var carritoResult = await _carritoRepository.GetbyIdasync(id);

                if (!carritoResult.IsSuccess || carritoResult.Data == null)
                {
                    _logger.LogError("Carrito no encontrado o fallo al obtenerlo.");
                    return carritoResult;
                }

                var carritoDto = carritoResult.Data as GetCarritoDto;
                if (carritoDto == null)
                {
                    return OperationResult.Failure("Error al mapear el carrito.");
                }

                var items = await _itemCarritoRepository.GetItemsByCarritoIdAsync(carritoDto.Id);
                carritoDto.Productos = items;

                _logger.LogInformation("Carrito obtenido exitosamente con items.");
                return OperationResult.Success("Carrito obtenido con items.", carritoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener el carrito por ID.", ex);
                return OperationResult.Failure("Ocurrió un error inesperado al obtener el carrito.");
            }
        }

        public async Task<OperationResult> UpdateAsync(UpdateCarritoDto dto)
        {
            _logger.LogInformation("Intentando actualizar el carrito.");

            var validationResult = _updateValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Fallo en la validación al actualizar el carrito.");
                return OperationResult.Failure(errors);
            }

            try
            {
                var result = await _carritoRepository.Updateasync(dto);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Carrito actualizado exitosamente.");
                }
                else
                {
                    _logger.LogError("Fallo al actualizar el carrito.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error inesperado durante la actualización del carrito.", ex);
                return OperationResult.Failure("Ocurrió un error inesperado durante la actualización del carrito.");
            }
        }

        public async Task<OperationResult> ClearCarritoAsync(int carritoId)
        {
            _logger.LogInformation("Intentando limpiar el carrito.");

            if (carritoId <= 0)
            {
                _logger.LogError("ID de carrito inválido para limpiar. Debe ser mayor que cero.");
                return OperationResult.Failure("El ID del carrito debe ser mayor que cero.");
            }

            try
            {
                var result = await _itemCarritoRepository.ClearItemsByCarritoIdAsync(carritoId);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Carrito limpiado exitosamente.");
                }
                else
                {
                    _logger.LogError("Fallo al limpiar el carrito.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error inesperado al limpiar el carrito.", ex);
                return OperationResult.Failure("Ocurrió un error inesperado al limpiar el carrito.");
            }
        }

        public async Task UpdateCarritoTotal(int carritoId)
        {
            _logger.LogInformation($"Actualizando total del carrito con ID: {carritoId}");

            try
            {

                var items = await _itemCarritoRepository.GetItemsByCarritoIdAsync(carritoId);

                decimal nuevoTotal = 0;

                if (items != null && items.Any())
                {
                    nuevoTotal = items.Sum(i => i.SubTotal);
                }

                _logger.LogInformation($"Nuevo total calculado para el carrito {carritoId}: {nuevoTotal}");

                var carritoResult = await _carritoRepository.GetbyIdasync(carritoId);
                if (!carritoResult.IsSuccess || carritoResult.Data == null)
                {
                    _logger.LogError($"No se pudo encontrar el carrito {carritoId} para actualizar su total.");
                    return;
                }

                var carritoDto = carritoResult.Data as GetCarritoDto;


                var updateDto = CarritoMapper.MapToUpdateDto(carritoDto, nuevoTotal);


                var updateResult = await _carritoRepository.Updateasync(updateDto);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError($"Error al guardar el nuevo total del carrito {carritoId}. Mensaje: {updateResult.Message}");
                }
                else
                {
                    _logger.LogInformation($"Total del carrito {carritoId} actualizado exitosamente a {nuevoTotal}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Excepción inesperada al actualizar el total del carrito {carritoId}.", ex);
            }
        }

    }
}
