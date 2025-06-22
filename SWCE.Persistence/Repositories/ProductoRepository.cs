using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Validators.ProductoValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IRepositorioProducto
    {
        private readonly E_commerceContext _context;
        private readonly ILoggerBase<Producto> _logger;
        private readonly CreateProductoValidator _validator;

        public ProductoRepository(E_commerceContext context, ILoggerBase<Producto> logger, CreateProductoValidator validator)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Obteniendo entidad Producto por ID");
                var entity = await base.GetbyIdasync(id);
                result = OperationResult.Success("Producto obtenido correctamente", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener el Producto por ID", ex);
                result = OperationResult.Failure("Ocurrió un error al obtener el Producto.");
            }

            return result;
        }

        public async override Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Obteniendo todas las entidades Producto");
                var entities = await base.GetAllasync();
                result = OperationResult.Success("Productos obtenidos correctamente", entities);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los Productos", ex);
                result = OperationResult.Failure("Ocurrió un error al obtener los Productos.");
            }

            return result;
        }

        public async override Task<OperationResult> Createasync(Producto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Creando entidad Producto");

                if (entity == null)
                {
                    _logger.LogError("El Producto es nulo");
                    return OperationResult.Failure("La entidad Producto no puede ser nula.");
                }

                await base.Createasync(entity);
                result = OperationResult.Success("Producto creado correctamente", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al crear el Producto", ex);
                result = OperationResult.Failure($"Ocurrió un error al crear el Producto: {ex.Message}");
            }

            return result;
        }

        public async override Task<OperationResult> Updateasync(Producto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Actualizando entidad Producto");

                if (entity == null)
                {
                    _logger.LogError("El Producto es nulo");
                    return OperationResult.Failure("La entidad Producto no puede ser nula.");
                }

                await base.Updateasync(entity);
                result = OperationResult.Success("Producto actualizado correctamente", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar el Producto", ex);
                result = OperationResult.Failure($"Ocurrió un error al actualizar el Producto: {ex.Message}");
            }

            return result;
        }

        public async override Task<bool> ExistsAsync(Expression<Func<Producto, bool>> filter)
        {
            return await base.ExistsAsync(filter);
        }

        public async Task<OperationResult> ObtenerPorCategoriaAsync(int categoriaId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Obteniendo Productos por ID de categoría");

                var productos = await _context.Productos
                    .Where(p => p.Categoria.id == categoriaId)
                    .ToListAsync();

                result = OperationResult.Success("Productos obtenidos correctamente por categoría", productos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los Productos por ID de categoría", ex);
                result = OperationResult.Failure("Ocurrió un error al obtener los Productos por categoría.");
            }

            return result;
        }
    }
}
