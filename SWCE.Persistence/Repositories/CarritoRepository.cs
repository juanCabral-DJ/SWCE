using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWCE.Application.Base;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Application.Interfaces.Repositories;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SWCE.Persistence.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILoggerBase<Carrito> _logger;
        private readonly CreateCarritoValidator _createValidator;
        private readonly UpdateCarritoValidator _updateValidator;


        public CarritoRepository(IConfiguration configuration, ILoggerBase<Carrito> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:E-commerceConnection"];
            _logger = logger;
            _createValidator = new CreateCarritoValidator();
            _updateValidator = new UpdateCarritoValidator();
        }

        public async Task<OperationResult> Createasync(CreateCarritoDto entity)
        {
            var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("La conexión está abierta y lista");
            }
            else
            {
                Console.WriteLine("La conexión NO está abierta");
            }

            OperationResult result = new OperationResult();
            var validation = _createValidator.Validate(entity);

            if (!validation.IsValid)
            {
                result.IsSuccess = false;
                result.Message = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Falló la validación del carrito");
                return result;
            }

            try
            {
                _logger.LogInformation("Intentando crear un nuevo carrito con: {@Entity}", entity);

                await ExecuteStoredProcedureAsync("dbo.CreateCarrito",
                    new SqlParameter("@ID_Usuario", entity.ID_Usuario)
                );

                result.IsSuccess = true;
                result.Message = "Carrito creado exitosamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al crear el carrito.";
                _logger.LogError("Error al crear el carrito", ex);
            }

            return result;
        }

        public async Task<OperationResult> GetAllasync()
        {
            OperationResult result = new OperationResult();
            var carritos = new List<GetCarritoDto>();

            try
            {
                _logger.LogInformation("Obteniendo todos los carritos");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("dbo.GetCarrito", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carrito = new GetCarritoDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                                    Productos = new List<GetItemCarritoDto>()
                                };

                                carrito.Productos = await GetItemsByCarritoIdAsync(carrito.Id);

                                carritos.Add(carrito);
                            }
                        }
                    }
                }

                result.IsSuccess = true;
                result.Data = carritos;
                result.Message = "Carritos obtenidos exitosamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los carritos", ex);
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al obtener los carritos.";
            }

            return result;
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Buscando carrito por ID: {Id}", id);

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("dbo.GetCarritoById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", id));

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var carrito = new GetCarritoDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                                    Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                                    Productos = new List<GetItemCarritoDto>()
                                };

                                reader.Close();

                                var Productos = await GetItemsByCarritoIdAsync(carrito.Id);
                                carrito.Productos = Productos;

                                result.IsSuccess = true;
                                result.Data = carrito;
                                result.Message = "Carrito encontrado.";
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = "Carrito no encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener el carrito por ID", ex);
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al buscar el carrito.";
            }

            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateCarritoDto entity)
        {
            var result = new OperationResult();
            var validation = _updateValidator.Validate(entity);

            if (!validation.IsValid)
            {
                result.IsSuccess = false;
                result.Message = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Falló la validación al actualizar el carrito");
                return result;
            }

            try
            {
                _logger.LogInformation("Actualizando carrito con: {@Entity}", entity);

                await ExecuteStoredProcedureAsync("dbo.ModifyCarrito",
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ID_Usuario", entity.IdUsuario),
                    new SqlParameter("@Total", entity.Total),
                    new SqlParameter("@IsDeleted", entity.IsDeleted)
                );

                result.IsSuccess = true;
                result.Message = "Carrito actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al actualizar el carrito.";
                _logger.LogError("Error al actualizar el carrito", ex);
            }

            return result;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            _logger.LogInformation("Verificando si existe carrito por ID: {Id}", id);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("dbo.CarritoExistsById", (SqlConnection)connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", id));

                        var outputParam = new SqlParameter("@Existe", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await ((SqlConnection)connection).OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        return (bool)outputParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el carrito por ID: {id}", ex);
                return false;
            }
        }

        

        public async Task<OperationResult> DeleteAsync(DisableCarritoDto entity)
        {
            var result = new OperationResult();

            
            var carritoExiste = await ExistsByIdAsync(entity.Id);
            if (!carritoExiste)
            {
                result.IsSuccess = false;
                result.Message = $"No se encontró un carrito con el ID {entity.Id} para eliminar.";
                _logger.LogError("Intento de eliminación fallido. No existe el carrito con ID");
                return result;
            }

            try
            {
                _logger.LogInformation("Iniciando eliminación del carrito con ID: {CarritoId}", entity.Id);

                await ExecuteStoredProcedureAsync("dbo.DisableCarrito",
                    new SqlParameter("@Id", entity.Id)
                );

                result.IsSuccess = true;
                result.Message = "Carrito eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error al eliminar el carrito", ex);
                result.IsSuccess = false;
                result.Message = "Ocurrió un error al eliminar el carrito.";
            }

            return result;
        }
        private async Task ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
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
                    await command.ExecuteNonQueryAsync(); // Para INSERT, UPDATE, DELETE
                }
            }
        }

        public async Task<OperationResult> GetByUserIdAsync(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Attempting to retrieve carrito by user ID: {UserId}", userId);

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("dbo.sp_Carrito_ExistsByUserId", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ID_Usuario", userId));

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var carrito = CarritoMapper.MapToGetCarritoDtoFromReader(reader);
                                result.IsSuccess = true;
                                result.Data = carrito;
                                result.Message = "Carrito found for the user.";
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = "No carrito found for the specified user ID.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving carrito", ex);
                result.IsSuccess = false;
                result.Message = "An error occurred while retrieving the carrito for the user.";
            }
            return result;
        }

        public async Task<List<GetItemCarritoDto>> GetItemsByCarritoIdAsync(int carritoId)
        {
            var items = new List<GetItemCarritoDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("dbo.GetItemCarritoByCarritoId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@CarritoId", carritoId));

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                items.Add(new GetItemCarritoDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    CarritoId = reader.GetInt32(reader.GetOrdinal("CarritoId")),
                                    IdProducto = reader.GetInt32(reader.GetOrdinal("ID_Producto")),
                                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Subtotal = reader.GetDecimal(reader.GetOrdinal("SubTotal"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los items del carrito", ex);
            }

            return items;
        }
        

    }

}

