using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWCE.Application.Validators.CategoriaValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System.Data;
using System.Linq.Expressions;

namespace SWCE.Persistence.Repositories
{
    public class CategoriaRepository : IRepositorioCategoria
    {
        private readonly string? _connectionString;
        private readonly CreateCategoriaValidator _createValidator;
        private readonly UpdateCategoriaValidator _updateValidator;
        private readonly ILoggerBase<Categoria> _logger;

        public CategoriaRepository(
            IConfiguration configuration,
            CreateCategoriaValidator createValidator,
            UpdateCategoriaValidator updateValidator,
            ILoggerBase<Categoria> logger)
        {
            _connectionString = configuration["ConnectionStrings:E-commerceConnection"];
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<OperationResult> Createasync(Categoria entity)
        {
            try
            {
                _logger.LogInformation("Adding Categoria {@Entity}", entity);
                var validation = await _createValidator.ValidateAsync(new Application.Dtos.AdministracionModule.CategoriaDto.CreateCategoriaDto
                {
                    Nombre = entity.Nombre,
                    Descripcion = entity.Descripcion
                });

                if (!validation.IsValid)
                {
                    return OperationResult.Failure(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));
                }

                var result = await ExecuteStoredProcedureAsync("dbo.CreateCategoria",
                    new SqlParameter("@Nombre", entity.Nombre),
                    new SqlParameter("@Descripcion", entity.Descripcion));

                if (result > 0)
                    return OperationResult.Success("Categoría agregada exitosamente", entity);
                else
                    return OperationResult.Failure("No se pudo agregar la categoría");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al agregar categoría: {Message}", ex);
                return OperationResult.Failure("Error interno al agregar categoría", ex);
            }
        }

        public async Task<OperationResult> Updateasync(Categoria entity)
        {
            try
            {
                _logger.LogInformation("Updating Categoria {@Entity}", entity);
                var validation = await _updateValidator.ValidateAsync(entity);
                if (!validation.IsValid)
                {
                    return OperationResult.Failure(string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage)));
                }

                var result = await ExecuteStoredProcedureAsync("dbo.UpdateCategoria",
                    new SqlParameter("@Id", entity.id),
                    new SqlParameter("@Nombre", entity.Nombre),
                    new SqlParameter("@Descripcion", entity.Descripcion));

                if (result > 0)
                    return OperationResult.Success("Categoría actualizada exitosamente", entity);
                else
                    return OperationResult.Failure("No se pudo actualizar la categoría");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar categoría: {Message}", ex);
                return OperationResult.Failure("Error interno al actualizar categoría", ex);
            }
        }

        public async Task<OperationResult> GetAllasync(Expression<Func<Categoria, bool>> filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las categorías");
                var categorias = await ExecuteReaderListAsync("dbo.GetAllCategorias", reader => new Categoria(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetString(reader.GetOrdinal("Nombre")),
                    reader.GetString(reader.GetOrdinal("Descripcion"))
                ));

                return OperationResult.Success("Categorías obtenidas correctamente", categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener categorías: {Message}", ex);
                return OperationResult.Failure("Error interno al obtener categorías", ex);
            }
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo categoría por ID: {Id}", id);
                var categoria = await ExecuteReaderSingleAsync("dbo.GetCategoriaById", reader => new Categoria(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetString(reader.GetOrdinal("Nombre")),
                    reader.GetString(reader.GetOrdinal("Descripcion"))
                ), new SqlParameter("@Id", id));

                if (categoria != null)
                    return OperationResult.Success("Categoría encontrada", categoria);
                else
                    return OperationResult.Failure("Categoría no encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener categoría: {Message}", ex);
                return OperationResult.Failure("Error interno al obtener categoría", ex);
            }
        }

        public async Task<List<Categoria>> ObtenerActivasAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo categorías activas");
                var categorias = await ExecuteReaderListAsync("dbo.GetCategoriasActivas", reader => new Categoria(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetString(reader.GetOrdinal("Nombre")),
                    reader.GetString(reader.GetOrdinal("Descripcion"))
                ));
                return categorias;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener categorías activas: {Message}", ex);
                return new List<Categoria>();
            }
        }

        private async Task<int> ExecuteStoredProcedureAsync(string procedure, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(procedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        private async Task<List<Categoria>> ExecuteReaderListAsync(string procedure, Func<SqlDataReader, Categoria> map, params SqlParameter[] parameters)
        {
            var result = new List<Categoria>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(procedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(map(reader));
            }
            return result;
        }

        private async Task<Categoria?> ExecuteReaderSingleAsync(string procedure, Func<SqlDataReader, Categoria> map, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(procedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? map(reader) : null;
        }

        public Task<bool> ExistsAsync(Expression<Func<Categoria, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> DisableAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deshabilitando categoría con ID: {Id}", id);

                var result = await ExecuteStoredProcedureAsync("dbo.DeshabilitarCategoria",
                    new SqlParameter("@Id", id));

                if (result > 0)
                    return OperationResult.Success("Categoría deshabilitada correctamente");

                return OperationResult.Failure("No se pudo deshabilitar la categoría");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al deshabilitar categoría: {Message}", ex);
                return OperationResult.Failure("Error interno al deshabilitar categoría", ex);
            }
        }
    }
}

