using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IRepositorioProducto
    {
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<Producto> _logger;
        private readonly CreateProductoValidator _createValidator;
        private readonly UpdateProductoValidator _updateValidator;

        public ProductoRepository(
            E_commerceContext context,
            ILoggerBase<Producto> logger,
            CreateProductoValidator createValidator,
            UpdateProductoValidator updateValidator
        ) : base(context)
        {
            _context = context;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public override async Task<OperationResult> GetAllasync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los productos.");
                var productos = await base.GetAllasync();
                return OperationResult.Success("Productos obtenidos correctamente.", productos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener productos: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener los productos.");
            }
        }

        public override async Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo producto por ID: {Id}", id);
                var baseResult = await base.GetbyIdasync(id);

                if (!baseResult.IsSuccess || baseResult.Data == null)
                    return OperationResult.Failure("Producto no encontrado.");

                var producto = baseResult.Data as Producto;
                if (producto == null)
                    return OperationResult.Failure("Error al convertir el producto.");

                return OperationResult.Success("Producto obtenido correctamente.", producto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener producto: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al obtener el producto.");
            }
        }

        public override async Task<OperationResult> Createasync(Producto entity)
        {
            try
            {
                if (entity is not Producto producto)
                    return OperationResult.Failure("Entidad no válida para crear un producto.");

                var validation = await _createValidator.ValidateAsync(new CreateProductoDto
                {
                    Nombre = producto.Nombre,
                    Marca = producto.Marca,
                    Precio = producto.Precio,
                    Stock = producto.Stock
                });

                if (!validation.IsValid)
                    return OperationResult.Failure("Validación fallida: " + string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

                return await base.Createasync(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al crear el producto: {Message}", ex);
                return OperationResult.Failure("Ocurrió un error al crear el producto.");
            }
        }

        public override async Task<OperationResult> Updateasync(Producto entity)
        {
            try
            {
                _logger.LogInformation("Intentando actualizar producto: {@Producto}", entity);

                if (entity == null)
                {
                    _logger.LogError("Se intentó actualizar un producto nulo.");
                    return OperationResult.Failure("El producto no puede ser nulo para la actualización.");
                }

                var validationResult = await _updateValidator.ValidateAsync(entity);
                if (!validationResult.IsValid)
                {
                    var errores = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    _logger.LogError("Fallo de validación para la actualización de producto: " + errores);
                    return OperationResult.Failure("Fallo de validación: " + errores);
                }

                var updateResult = await base.Updateasync(entity);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError("Error al actualizar producto: " + updateResult.Message);
                    return OperationResult.Failure($"Error al actualizar producto: {updateResult.Message}");
                }

                _logger.LogInformation("Producto actualizado exitosamente: {@Producto}", entity);
                return OperationResult.Success("Producto actualizado exitosamente.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inesperado al actualizar producto: {Exception}", ex);
                return OperationResult.Failure("Ocurrió un error inesperado al actualizar el producto: " + ex.Message);
            }
        }
        public async Task<OperationResult> DisableAsync(int id)
        {
            try
            {
                _logger.LogInformation("Intentando deshabilitar el producto con ID: {Id}", id);

                var productoExistente = await _context.Productos.FindAsync(id);

                if (productoExistente == null)
                {
                    _logger.LogError("No se encontró un producto con el ID");
                    return OperationResult.Failure("Producto no encontrado.");
                }

                productoExistente.IsDeleted = true;

                _context.Productos.Update(productoExistente);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Producto con ID {Id} deshabilitado correctamente.", id);
                return OperationResult.Success("Producto deshabilitado correctamente.", productoExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar el producto");
                return OperationResult.Failure("Error interno al deshabilitar el producto.", ex);
            }
        }
    }
}