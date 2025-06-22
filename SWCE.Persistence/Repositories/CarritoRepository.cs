using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Interfaces.Repositories;
using SWCE.Application.Validators.CarritoValidator;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly string _connectionString;
        private readonly ILoggerBase<Carrito> _logger;
        private readonly CreateCarritoValidator _createValidator;
        private readonly UpdateCarritoValidator _updateValidator;

        public CarritoRepository(IConfiguration configuration, ILoggerBase<Carrito> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
            _createValidator = new CreateCarritoValidator();
            _updateValidator = new UpdateCarritoValidator();
        }

        public async Task<OperationResult> Createasync(CreateCarritoDto entity)
        {
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

                await ExecuteStoredProcedureAsync("CreateCarrito",
                    new SqlParameter("@ID_Usuario", entity.IdUsuario)
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
                    using (var command = new SqlCommand("GetCarrito", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                carritos.Add(new GetCarritoDto
                                {
                                    Id = reader.GetInt32(0),
                                    IdUsuario = reader.GetInt32(1),
                                    Total = reader.GetDecimal(2)
                                });
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
                    using (var command = new SqlCommand("GetCarritoById", connection))
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
                                    Id = reader.GetInt32(0),
                                    IdUsuario = reader.GetInt32(1),
                                    Total = reader.GetDecimal(2)
                                };

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

                await ExecuteStoredProcedureAsync("ModifyCarrito",
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ID_Usuario", entity.IdUsuario),
                    new SqlParameter("@Total", entity.Total)
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
                    // Usamos sp_ para denotar "Stored Procedure", una buena práctica.
                    using (var command = new SqlCommand("sp_Carrito_ExistsById", (SqlConnection)connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", id));

                        // Un parámetro de salida es la forma más eficiente de devolver un valor simple.
                        var outputParam = new SqlParameter("@Existe", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await ((SqlConnection)connection).OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        // Convertimos el resultado del parámetro de salida a booleano.
                        return (bool)outputParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el carrito por ID: {id}", ex);
                // Si ocurre un error, es más seguro asumir que no existe o notificar el fallo.
                return false;
            }
        }

        public async Task<bool> ExistsByUserIdAsync(int userId)
        {
            _logger.LogInformation("Verificando si existe carrito para el Usuario ID: {UserId}", userId);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_Carrito_ExistsByUserId", (SqlConnection)connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserId", userId));

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
                _logger.LogError($"Error verificando existencia de carrito por Usuario ID: {userId}", ex);
                return false;
            }
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
    }

}

