using Microsoft.Extensions.Configuration;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Extension.Mapeo_Registro.Mapeo_Producto;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;

namespace SWCE.Application.Services
{
    public sealed class ProductoService : IProductoServices
    {
        private readonly IRepositorioProducto _productoRepository;
        private readonly ILoggerBase<ProductoService> _logger;
        private readonly IConfiguration _configuration;

        public ProductoService(
            IRepositorioProducto productoRepository,
            ILoggerBase<ProductoService> logger,
            IConfiguration configuration)
        {
            _productoRepository = productoRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateProductoDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Creating Product entity");

                var producto = ProductoMapper.MapToEntityCreate(dto);
                result = await _productoRepository.Createasync(producto);

                _logger.LogInformation("Successfully created Product");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating Product", ex);
                result = OperationResult.Failure("An error occurred while creating Product");
            }

            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateProductoDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                int id = entity.Id;
                Producto producto = ProductoMapper.MapToEntityUpdate(entity);
                result = await _productoRepository.Updateasync(producto);
                _logger.LogInformation("Successfully updated Product");

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating Product", ex);
                result = OperationResult.Failure("An error occurred while updating Product");
            }

            return result;
        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<Producto, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Fetching all Products");

                result = await _productoRepository.GetAllasync(filter);
                result = OperationResult.Success("Products retrieved successfully", result.Data);

                _logger.LogInformation("Successfully fetched Products");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving Products", ex);
                result = OperationResult.Failure("An error occurred while retrieving Products");
            }

            return result;
        }

        public async Task<OperationResult> GetbyId(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Fetching Product by ID");

                result = await _productoRepository.GetbyIdasync(id);
                result = OperationResult.Success("Product retrieved successfully", result.Data);

                _logger.LogInformation("Successfully fetched Product by ID");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving Product by ID", ex);
                result = OperationResult.Failure("An error occurred while retrieving Product by ID");
            }

            return result;
        }

        public async Task<OperationResult> Disableasync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando producto con ID: {Id}", id);

                if (id <= 0)
                    return OperationResult.Failure("El ID del producto debe ser mayor que cero.");

                var result = await _productoRepository.DisableAsync(id);

                if (!result.IsSuccess)
                {
                    _logger.LogError("Falló al deshabilitar producto");
                    return result;
                }

                _logger.LogInformation("Producto con ID {Id} deshabilitado correctamente.", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error inesperado al deshabilitar el producto con ID: {Id}", id);
                return OperationResult.Failure("Ocurrió un error inesperado al deshabilitar el producto.", ex);
            }
        }
    }
}

