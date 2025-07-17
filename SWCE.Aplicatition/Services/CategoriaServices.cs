using Microsoft.Extensions.Configuration;
using SWCE.Application.Base;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Application.Extension.MappersAdministrationModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;

namespace SWCE.Application.Services
{
    public sealed class CategoriaServices : ICategoriaServices
    {
        private readonly IRepositorioCategoria _categoriaRepo;
        private readonly ILoggerBase<CategoriaServices> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaServices(
            IRepositorioCategoria categoriaRepo,
            ILoggerBase<CategoriaServices> logger,
            IConfiguration configuration)
        {
            _categoriaRepo = categoriaRepo;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateCategoriaDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Creating new Categoria");

                var categoria = CategoriaMapper.MapToEntityCreate(entity);

                result = await _categoriaRepo.Createasync(categoria);

                _logger.LogInformation("Successfully created Categoria");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating Categoria", ex);
                result = OperationResult.Failure("An error occurred while creating Categoria");
            }

            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateCategoriaDto entity)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Updating Categoria");

                var categoria = CategoriaMapper.MapToEntityUpdate(entity);

                result = await _categoriaRepo.Updateasync(categoria);

                _logger.LogInformation("Successfully updated Categoria");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating Categoria", ex);
                result = OperationResult.Failure("An error occurred while updating Categoria");
            }

            return result;
        }

        public async Task<OperationResult> GetbyId(int id)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation($"Retrieving Categoria with ID: {id}");

                result = await _categoriaRepo.GetbyIdasync(id);

                _logger.LogInformation("Successfully retrieved Categoria");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving Categoria by ID", ex);
                result = OperationResult.Failure("An error occurred while retrieving Categoria by ID");
            }

            return result;
        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<Categoria, bool>> filter)
        {
            OperationResult result = new();

            try
            {
                _logger.LogInformation("Retrieving all Categoria entities");

                result = await _categoriaRepo.GetAllasync(filter);

                _logger.LogInformation("Successfully retrieved all Categoria entities");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving Categorias", ex);
                result = OperationResult.Failure("An error occurred while retrieving Categorias");
            }

            return result;
        }

        public async Task<List<Categoria>> ObtenerActivasAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo categorías activas desde el repositorio");
                var categoriasActivas = await _categoriaRepo.ObtenerActivasAsync();
                return categoriasActivas;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener categorías activas", ex);
                return new List<Categoria>();
            }
        }

        public async Task<OperationResult> Disableasync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando categoría en el servicio con ID: {Id}", id);
                var result = await _categoriaRepo.DisableAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar categoría en el servicio", ex);
                return OperationResult.Failure("Ocurrió un error al deshabilitar la categoría", ex);
            }
        }
    }
}

