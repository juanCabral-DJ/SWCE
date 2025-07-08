using Microsoft.EntityFrameworkCore;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Validators.ItemCarritoValidator;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class ItemCarritoRepository : RepositoryBase<ItemCarrito>, IItemCarritoRepository
    {
        private readonly CreateItemCarritoValidator _Validator;
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<ItemCarrito> _logger;

        public ItemCarritoRepository(E_commerceContext _context, ILoggerBase<ItemCarrito> _logger, CreateItemCarritoValidator Validator)
            : base(_context)
        {
            _Validator = Validator;
            this._context = _context;
            this._logger = _logger;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando recuperar ItemCarrito con ID: {id}");

                var baseResult = await base.GetbyIdasync(id);

                if (!baseResult.IsSuccess || baseResult.Data == null)
                {
                    return OperationResult.Failure(baseResult.Message ?? "ItemCarrito no encontrado.");
                }

                var item = baseResult.Data as ItemCarrito;
                if (item == null)
                {
                    return OperationResult.Failure("Error interno: No se pudo convertir a ItemCarrito.");
                }

                return OperationResult.Success("ItemCarrito recuperado exitosamente.", item);
            }
            catch (Exception e)
            {
                _logger.LogError("Error al recuperar ItemCarrito", e);
                return OperationResult.Failure("Ocurrió un error al recuperar el ItemCarrito");
            }
        }

        public async override Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Intentando recuperar todos los ItemCarrito");
                var itemsCarrito = await base.GetAllasync();

                result = OperationResult.Success("ItemsCarrito recuperado exitosamente", itemsCarrito);
            }
            catch (Exception e)
            {
                _logger.LogError("Error recuperando los ItemsCarrito", e);
                result = OperationResult.Failure("Ocurrió un error al recuperar todos los ItemCarrito");
            }

            return result;
        }
        public async Task<OperationResult> AddItemAsync(ItemCarrito item)
        {
            try
            {
                _logger.LogInformation("Intentando añadir ItemCarrito: {@Item}", item);

                if (item == null)
                {
                    _logger.LogError("Se intentó añadir un ItemCarrito nulo.");
                    return OperationResult.Failure("El ItemCarrito no puede ser nulo.");
                }

                var validationResult = await _Validator.ValidateAsync(item);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Fallo de validación para ItemCarrito: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return OperationResult.Failure("Fallo de validación: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                var existingItem = await _context.ItemsCarrito
                    .FirstOrDefaultAsync(ic => ic.CarritoId == item.CarritoId && ic.IdProducto == item.IdProducto);

                if (existingItem != null)
                {
                    existingItem.Cantidad += item.Cantidad;
                    var updateResult = await base.Updateasync(existingItem);
                    if (!updateResult.IsSuccess)
                    {
                        _logger.LogError("Error al actualizar la cantidad del ItemCarrito existente: " + updateResult.Message);
                        return OperationResult.Failure($"Error al actualizar la cantidad del ItemCarrito existente: {updateResult.Message}");
                    }
                    _logger.LogInformation("Actualizando cantidad de ItemCarrito existente: {@Item}", existingItem);
                    return OperationResult.Success("Cantidad de ItemCarrito existente actualizada exitosamente.", existingItem);
                }
                else
                {
                    var createResult = await base.Createasync(item);
                    if (!createResult.IsSuccess)
                    {
                        _logger.LogError("Error al añadir nuevo ItemCarrito: " + createResult.Message);
                        return OperationResult.Failure($"Error al añadir nuevo ItemCarrito: {createResult.Message}");
                    }
                    _logger.LogInformation("Añadiendo nuevo ItemCarrito: {@Item}", item);
                    return OperationResult.Success("Nuevo ItemCarrito añadido exitosamente.", item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inesperado al añadir/actualizar ItemCarrito: {@Item}", ex);
                return OperationResult.Failure($"Ocurrió un error inesperado al añadir/actualizar el ItemCarrito: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetItemsByCartIdAsync(int carritoId)
        {
            try
            {
                _logger.LogInformation($"Recuperando items para Carrito con ID: {carritoId}");
                var items = await _context.Set<ItemCarrito>()
                                        .Where(i => i.CarritoId == carritoId && i.IsDeleted == false)
                                        .ToListAsync();

                if (items == null || !items.Any())
                {
                    _logger.LogInformation($"No se encontraron items para el Carrito con ID: {carritoId}");
                    return OperationResult.Failure($"No se encontraron items para el carrito con ID: {carritoId}.");
                }

                _logger.LogInformation($"Se recuperaron {items.Count} items para el Carrito con ID: {carritoId}");
                return OperationResult.Success("Items del carrito recuperados exitosamente.", items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar items para Carrito con ID: {carritoId}", ex);
                return OperationResult.Failure($"Ocurrió un error al recuperar los items del carrito: {ex.Message}");
            }
        }

        public async Task<OperationResult> RemoveItemAsync(int itemId)
        {
            try
            {
                _logger.LogInformation($"Intentando eliminar ItemCarrito con ID: {itemId}");
                var entity = await _context.ItemsCarrito.FindAsync(itemId);
                if (entity == null)
                {
                    _logger.LogInformation($"ItemCarrito con ID {itemId} no encontrado para eliminación.");
                    return OperationResult.Failure($"ItemCarrito con ID {itemId} no encontrado.");
                }

                entity.IsDeleted = true;

                _context.ItemsCarrito.Update(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"ItemCarrito con ID {itemId} eliminado exitosamente.");
                return OperationResult.Success("ItemCarrito eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al eliminar ItemCarrito con ID: {itemId}", ex);
                return OperationResult.Failure($"Ocurrió un error inesperado al eliminar el ItemCarrito: {ex.Message}");
            }
        }

        public async Task<OperationResult> UpdateItemAsync(ItemCarrito item)
        {
            try
            {
                _logger.LogInformation("Intentando actualizar ItemCarrito: {@Item}", item);

                if (item == null)
                {
                    _logger.LogError("Se intentó actualizar un ItemCarrito nulo.");
                    return OperationResult.Failure("El ItemCarrito no puede ser nulo para la actualización.");
                }

                var validationResult = await _Validator.ValidateAsync(item);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Fallo de validación para la actualización de ItemCarrito: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return OperationResult.Failure("Fallo de validación: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                }

                var updateResult = await base.Updateasync(item);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError("Error al actualizar ItemCarrito: " + updateResult.Message);
                    return OperationResult.Failure($"Error al actualizar ItemCarrito: {updateResult.Message}");
                }

                _logger.LogInformation("ItemCarrito actualizado exitosamente: {@Item}", item);
                return OperationResult.Success("ItemCarrito actualizado exitosamente.", item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inesperado al actualizar ItemCarrito: {@Item}", ex);
                return OperationResult.Failure($"Ocurrió un error inesperado al actualizar el ItemCarrito: {ex.Message}");
            }
        }

        public async Task<OperationResult> UpdateItemQuantityAsync(int Id, int newQuantity)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar la cantidad del ItemCarrito con ID: {Id} a {newQuantity}");

                if (newQuantity <= 0)
                {
                    _logger.LogError($"Cantidad inválida ({newQuantity}) para la actualización del ItemCarrito con ID: {Id}.");
                    return OperationResult.Failure("La nueva cantidad debe ser mayor a 0.");
                }

                var getItemResult = await base.GetbyIdasync(Id);
                if (!getItemResult.IsSuccess || getItemResult.Data == null)
                {
                    _logger.LogInformation($"ItemCarrito con ID {Id} no encontrado para la actualización de cantidad.");
                    return OperationResult.Failure(getItemResult.Message ?? $"ItemCarrito con ID {Id} no encontrado.");
                }

                var itemToUpdate = getItemResult.Data as ItemCarrito;
                if (itemToUpdate == null)
                {
                    _logger.LogError($"Error interno: No se pudo convertir el ItemCarrito recuperado para ID: {Id}.");
                    return OperationResult.Failure($"Error interno: No se pudo convertir el ItemCarrito recuperado.");
                }

                itemToUpdate.Cantidad = newQuantity;

                var updateResult = await base.Updateasync(itemToUpdate);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError("Error al actualizar la cantidad del ItemCarrito: " + updateResult.Message);
                    return OperationResult.Failure($"Error al actualizar la cantidad del ItemCarrito: {updateResult.Message}");
                }

                _logger.LogInformation($"Cantidad de ItemCarrito con ID {Id} actualizada exitosamente a {newQuantity}.");
                return OperationResult.Success("Cantidad del ItemCarrito actualizada exitosamente.", itemToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar la cantidad del ItemCarrito con ID: {Id}.", ex);
                return OperationResult.Failure($"Ocurrió un error inesperado al actualizar la cantidad del ItemCarrito: {ex.Message}");
            }
        }
        public async Task<OperationResult> ClearItemsByCarritoIdAsync(int carritoId)
        {
            try
            {
                var items = _context.ItemsCarrito.Where(i => i.CarritoId == carritoId);

                if (!items.Any())
                {
                    return OperationResult.Failure("No hay items para eliminar en el carrito.");
                }

                _context.ItemsCarrito.RemoveRange(items);
                await _context.SaveChangesAsync();

                return OperationResult.Success("Carrito vaciado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al vaciar el carrito.", ex);
                return OperationResult.Failure("Error inesperado al vaciar el carrito.");
            }
        }

    }
}
