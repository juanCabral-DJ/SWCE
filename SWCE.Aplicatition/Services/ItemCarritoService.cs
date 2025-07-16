using FluentValidation;
using SWCE.Application.Base;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;

namespace SWCE.Application.Services
{
    public sealed class ItemCarritoService : ServiceBase<ItemCarrito>, IItemCarritoService
    {

        private readonly IItemCarritoRepository _itemCarritoRepository;
        private readonly IValidator<AddItemCarritoDto> _addItemValidator;
        private readonly IValidator<UpdateItemCantidadDto> _updateItemCantidadValidator;

        private readonly IRepositorioProducto _ProductoRepository;
        private readonly ICarritoService _carritoService;

        public ItemCarritoService(
            IItemCarritoRepository itemCarritoRepository,
            ILoggerBase<ServiceBase<ItemCarrito>> logger,
            IValidator<AddItemCarritoDto> addItemValidator,
            IValidator<UpdateItemCantidadDto> updateItemCantidadValidator,
            IRepositorioProducto productoRepository,
            ICarritoService carritoService)
            : base(itemCarritoRepository, logger)
        {
            _itemCarritoRepository = itemCarritoRepository;
            _addItemValidator = addItemValidator;
            _updateItemCantidadValidator = updateItemCantidadValidator;
            _ProductoRepository = productoRepository;
            _carritoService = carritoService;
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
                var productoResult = await _ProductoRepository.GetbyIdasync(dto.IdProducto);
                if (!productoResult.IsSuccess || productoResult.Data == null)
                {
                    return OperationResult.Failure("Producto no encontrado.");
                }

                var producto = productoResult.Data as Producto;
                if (producto == null)
                {
                    return OperationResult.Failure("Error interno: no se pudo obtener el producto.");
                }

                if (producto == null)
                {
                    return OperationResult.Failure("Error interno: no se pudo obtener el producto.");
                }

                var precioUnitario = producto.Precio;
                var subtotal = precioUnitario * dto.Cantidad;

                var itemCarritoEntity = ItemCarritoMapper.MapToEntity(dto, producto.Nombre);
                itemCarritoEntity.PrecioUnitario = precioUnitario;
                itemCarritoEntity.SubTotal = subtotal;

                var result = await _itemCarritoRepository.AddItemAsync(itemCarritoEntity);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item added to cart successfully.");
                    await _carritoService.UpdateCarritoTotal(dto.CarritoId);
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
                var getItemResult = await _itemCarritoRepository.GetbyIdasync(dto.Id);
                if (!getItemResult.IsSuccess || getItemResult.Data == null)
                {
                    _logger.LogInformation("Item not found for quantity update.");
                    return OperationResult.Failure($"Item with ID {dto.Id} not found.");
                }

                var itemToUpdate = getItemResult.Data as ItemCarrito;
                if (itemToUpdate == null)
                {
                    _logger.LogError("Retrieved item is not of expected type.");
                    return OperationResult.Failure("Internal error: retrieved item is not valid.");
                }

                var productoResult = await _ProductoRepository.GetbyIdasync(itemToUpdate.IdProducto);
                if (!productoResult.IsSuccess || productoResult.Data == null)
                {
                    _logger.LogError("Product not found when updating item.");
                    return OperationResult.Failure("Product not found when updating item.");
                }

                var producto = productoResult.Data as Producto;
                if (producto == null)
                {
                    return OperationResult.Failure("Internal error: product data is invalid.");
                }

                itemToUpdate.Cantidad = dto.NewCantidad;
                itemToUpdate.PrecioUnitario = producto.Precio;
                itemToUpdate.SubTotal = producto.Precio * dto.NewCantidad;

                var result = await base.UpdateAsync(itemToUpdate);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item quantity and subtotal updated successfully.");
                    await _carritoService.UpdateCarritoTotal(itemToUpdate.CarritoId);
                }
                else
                {
                    _logger.LogError("Failed to update item quantity and subtotal.");
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
                // Obtener el item para saber el carritoId
                var getItemResult = await _itemCarritoRepository.GetbyIdasync(itemId);
                if (!getItemResult.IsSuccess || getItemResult.Data == null)
                {
                    _logger.LogInformation("Item not found for removal.");
                    return OperationResult.Failure($"Item with ID {itemId} not found.");
                }

                var item = getItemResult.Data as ItemCarrito;

                var result = await _itemCarritoRepository.RemoveItemAsync(itemId);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Item removed from cart successfully.");
                    await _carritoService.UpdateCarritoTotal(item.CarritoId);
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


    }
}