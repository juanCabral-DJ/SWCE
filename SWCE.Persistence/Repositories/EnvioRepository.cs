using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Domain.Entities;
using Microsoft.Extensions.Configuration;
using SWCE.Aplication.Validators.EnvioValidator;
using SWCE.Infraestructure.Logging;
using Microsoft.Data.SqlClient;
using System.Data;
using SWCE.Domain.Base;


namespace SWCE.Persistence.Repositories
{
    public class EnvioRepository : IEnvioRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly EnvioValidator _EnvioValidator;
        private readonly UpdateEnvioValdiator _UpdateEnvioValdiator;
        private readonly ILoggerBase<EnvioEntity> _logger;

        public EnvioRepository( ILoggerBase<EnvioEntity> logger, IConfiguration configuration, EnvioValidator envioValidator, UpdateEnvioValdiator updateEnvioValidator)
        {
            _configuration = configuration;
            _logger = logger;
            _EnvioValidator = envioValidator;
            _UpdateEnvioValdiator = updateEnvioValidator;
            _connectionString = _configuration["ConnectionStrings:E-CommerceConnection"];
        }
        public async Task<OperationResult> GetByIdAsync(Guid id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Recuperando Envio por Id");

                var envios = await ExecuteReaderSingleAsync<EnvioEntity>("dbo.GetEnvioById", reader => new EnvioEntity
                {
                    UsuarioId = reader.GetInt32(reader.GetOrdinal("Usuario Id")),
                    FechaPedido = reader.GetDateTime(reader.GetOrdinal("Fecha Pedido")),
                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                    Costo = reader.GetDecimal(reader.GetOrdinal("Costo")),
                    TipoEnvio = reader.GetString(reader.GetOrdinal("Tipo Envio"))
                }, new SqlParameter("@Id", id));

                if (envios != null)
                {
                    result = OperationResult.Success("Se obtuvo el envio con id", envios);
                }
                else
                {
                    result = OperationResult.Failure("No se encontro el envio con ese ID");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener envio con ID", ex);
                result = OperationResult.Failure($"Ocurrio un error mientras se recuperaba el envio {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Recuperando Envios");
                var envios = await ExecuteReaderListAsync<EnvioEntity>("dbo.GetAllEnvios", reader => new EnvioEntity 
                {
                    UsuarioId = reader.GetInt32(reader.GetOrdinal("Usuario Id")),
                    FechaPedido = reader.GetDateTime(reader.GetOrdinal("Fecha Pedido")),
                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                    Costo = reader.GetDecimal(reader.GetOrdinal("Costo")),
                    TipoEnvio = reader.GetString(reader.GetOrdinal("Tipo Envio"))
                });

                result = OperationResult.Success("Recuperando envios", envios);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error recuperando envios {Message}", ex);
                result = OperationResult.Failure($"Ha ocurrido un error recuperando todos los envios: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult> CreateAsync(EnvioEntity envio)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Agregando envio: ${@envio}", envio);
                var entityvalidate = await _EnvioValidator.ValidateAsync(envio);

                if (!entityvalidate.IsValid)
                {
                    _logger.LogError("Inserte valores para agregar");
                    return OperationResult.Failure("La entidad envio no puede ser nulo");
                }

                var presult = await ExecuteStoredProcedureAsync("dbo.CreateEnvios", new SqlParameter("@Id_usuario", envio.UsuarioId), new SqlParameter("@FechaPedido", envio.FechaPedido), new SqlParameter("@Estado", envio.Estado), new SqlParameter("@Total", envio.Costo), new SqlParameter("@TipoEnvio", envio.TipoEnvio));

                if (presult > 0)
                {
                    _logger.LogInformation("Agregando envio: ${@envio}", envio);
                    return OperationResult.Success("Envio Agregado exitosamente", envio);

                }
                else
                {
                    _logger.LogError("Hubo un fallo al agregar envio. Ninguna fila afectada");
                    result = OperationResult.Failure("Fallo al agregar envio");
                } 

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Un error ha ocurrido agregando el envio {ex.Message}";
                _logger.LogError("Un error ha ocurrido agregando el envio: {Message}", ex);
            }
            return result; 

        }
        public async Task<OperationResult> UpdateAsync(EnvioEntity envio)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Actualizando envio: ${@envio}", envio);

                var enviovalidate = await _UpdateEnvioValdiator.ValidateAsync(envio);
                if (!enviovalidate.IsValid)
                {
                    _logger.LogError("Inserte valores para actualizar");
                    return OperationResult.Failure("La entidad envio no puede ser nulo");
                }

                var presult = await ExecuteStoredProcedureAsync("dbo.UpdateEnvioEstado", new SqlParameter("@Id", envio.Id), new SqlParameter("@Esatdo", envio.Estado));
                
                if(presult > 0)
                {
                    _logger.LogInformation("Envio actualizado exitosamente: ${@envio}", envio);
                    return OperationResult.Success("Envio actualizado exitosamente", envio);
                }
                else
                {
                    _logger.LogError("Hubo un fallo al actualizar el envio. Ninguna fila afectada");
                    return OperationResult.Failure("Fallo al actualizar el envio");
                }
            
            }
            catch (Exception ex)
            {
                _logger.LogError("Un error ha ocurrido actualizando el envio: {Message}", ex);
                return OperationResult.Failure($"Un error ha ocurrido actualizando el envio {ex.Message}");
            }
        }
        public async Task<OperationResult> GetByUserId(int userId)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Recuperando Envios mediante UserId");
                var envios = await ExecuteReaderSingleAsync<EnvioEntity>("dbo.GetEnviosByUserId", reader => new EnvioEntity
                {
                    UsuarioId = reader.GetInt32(reader.GetOrdinal("Usuario Id")),
                    FechaPedido = reader.GetDateTime(reader.GetOrdinal("Fecha Pedido")),
                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                    Costo = reader.GetDecimal(reader.GetOrdinal("Costo")),
                    TipoEnvio = reader.GetString(reader.GetOrdinal("Tipo Envio"))
                }, new SqlParameter("@Id_Usuario", userId));

                if (envios != null)
                {
                    result = OperationResult.Success("Se obtuvo el envio con UserId", envios);
                }
                else
                {
                    result = OperationResult.Failure("No se encontro el envio con ese UserId");
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error recuperando envios");
                result = OperationResult.Failure("Ha ocurrido un error recuperando los envios");
            }
            return result;
   
        }

        private async Task<int> ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {

                    command.CommandType = CommandType.StoredProcedure;


                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync(); // Para INSERT, UPDATE, DELETE
                }
            }
        }

        private async Task<List<EnvioEntity>> ExecuteReaderListAsync<T>(string sql, Func<SqlDataReader, EnvioEntity> map, params SqlParameter[] parameters)
        {
            var items = new List<EnvioEntity>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            items.Add(map(reader));
                        }
                    }
                }
            }
            return items;
        }

        private async Task<T?> ExecuteReaderSingleAsync<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return map(reader);
                        }
                    }
                }
            }
            return null;
        }
    }
}
