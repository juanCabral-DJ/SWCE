using FluentValidation;
using SWCE.Application.Base;
using SWCE.Application.Dtos.ItemCarrito;
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
    public sealed class ItemCarritoService : ServiceBase<ItemCarrito>, IItemCarritoService
    {
        private readonly IItemCarritoRepository _itemCarritoRepository;
        private readonly ItemCarritoMapper _itemCarritoMapper;
        private readonly IValidator<AddItemCarritoDto> _addItemValidator;
        private readonly IValidator<UpdateItemCantidadDto> _updateItemCantidadValidator;

        public ItemCarritoService(
            IItemCarritoRepository itemCarritoRepository,
            ILoggerBase<ServiceBase<ItemCarrito>> logger,
            ItemCarritoMapper itemCarritoMapper,
            IValidator<AddItemCarritoDto> addItemValidator,
            IValidator<UpdateItemCantidadDto> updateItemCantidadValidator)
            : base(itemCarritoRepository, logger)
        {
            _itemCarritoRepository = itemCarritoRepository;
            _itemCarritoMapper = itemCarritoMapper;
            _addItemValidator = addItemValidator;
            _updateItemCantidadValidator = updateItemCantidadValidator;
        }

        public async Task<OperationResult> AddItemToCarritoAsync(AddItemCarritoDto dto)
        {
            _logger.LogInformation("Adding item to cart.");

            var validationResult = await _addItemValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation failed for adding item to cart.");
                return OperationResult.Failure(errors);
            }

            try
            {
                var itemCarritoEntity = _itemCarritoMapper.MapToEntity(dto);

                var result = await _itemCarritoRepository.AddItemAsync(itemCarritoEntity);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item added to cart successfully.");

                }
                else
                {
                    _logger.LogError("Failed to add item to cart.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error adding item to cart.", ex);
                return OperationResult.Failure("An unexpected error occurred while adding item to cart.");
            }
        }

        public async Task<OperationResult> UpdateItemCantidadAsync(UpdateItemCantidadDto dto)
        {
            _logger.LogInformation("Updating item quantity in cart.");

            var validationResult = await _updateItemCantidadValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation failed for updating item quantity.");
                return OperationResult.Failure(errors);
            }

            try
            {
                var getItemResult = await _itemCarritoRepository.GetbyIdasync(dto.ItemId);
                if (!getItemResult.IsSuccess || getItemResult.Data == null)
                {
                    _logger.LogInformation("Item not found for quantity update.");
                    return OperationResult.Failure($"Item with ID {dto.ItemId} not found.");
                }

                var itemToUpdate = getItemResult.Data as ItemCarrito;
                if (itemToUpdate == null)
                {
                    _logger.LogError("Retrieved item is not of expected type.");
                    return OperationResult.Failure("Internal error: retrieved item is not valid.");
                }

                // *** ESTA ES LA LÍNEA CLAVE QUE CAMBIA ***
                itemToUpdate.Cantidad = dto.NewCantidad;

                // Usamos el método genérico UpdateAsync de la clase base
                var result = await base.UpdateAsync(itemToUpdate); // O simplemente _repository.UpdateAsync(itemToUpdate);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item quantity updated successfully.");
                }
                else
                {
                    _logger.LogError("Failed to update item quantity.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error updating item quantity.", ex);
                return OperationResult.Failure("An unexpected error occurred while updating item quantity.");
            }
        }

        public async Task<OperationResult> RemoveItemFromCarritoAsync(int itemId)
        {
            _logger.LogInformation("Removing item from cart.");

            if (itemId <= 0)
            {
                _logger.LogError("Invalid item ID for removal.");
                return OperationResult.Failure("The item ID must be greater than zero.");
            }

            try
            {
                // El repositorio se encarga de encontrar y eliminar el item
                var result = await _itemCarritoRepository.RemoveItemAsync(itemId); // Suponiendo un método RemoveItemAsync en el repo

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item removed from cart successfully.");
                }
                else
                {
                    _logger.LogError("Failed to remove item from cart.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error removing item from cart.", ex);
                return OperationResult.Failure("An unexpected error occurred while removing item from cart.");
            }
        }

        public async Task<OperationResult> GetItemsByCarritoIdAsync(int carritoId)
        {
            _logger.LogInformation("Getting items by cart ID.");

            if (carritoId <= 0)
            {
                _logger.LogError("Invalid cart ID for getting items.");
                return OperationResult.Failure("The cart ID must be greater than zero.");
            }

            try
            {
                // El repositorio devuelve una lista de entidades ItemCarrito
                var repoResult = await _itemCarritoRepository.GetItemsByCartIdAsync(carritoId);

                if (repoResult.IsSuccess && repoResult.Data is IEnumerable<ItemCarrito> itemEntities)
                {
                    // Mapear la lista de entidades a una lista de DTOs de salida
                    var itemDtos = itemEntities.Select(item => _itemCarritoMapper.MapToGetDto(item)).ToList();
                    _logger.LogInformation("Items retrieved by cart ID successfully.");
                    return OperationResult.Success("Items retrieved successfully.", itemDtos);
                }
                else
                {
                    _logger.LogInformation("No items found for the cart ID.");
                    return OperationResult.Failure(repoResult.Message ?? "No items found for the specified cart ID.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error getting items by cart ID.", ex);
                return OperationResult.Failure("An unexpected error occurred while retrieving items by cart ID.");
            }
        }
    }
}