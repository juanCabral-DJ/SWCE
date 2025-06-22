using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;
using SWCE.Aplicatition.Dtos.User;
using System.Data;
using SWCE.Aplicatition.Validators;
using System.ComponentModel.DataAnnotations;
using SWCE.Persistence.Context;
using Microsoft.EntityFrameworkCore.Update.Internal;
using FluentValidation;

namespace SWCE.Persistence.Repositories
{
    public class UserRepository : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly CreateUserValidator _Validator;
        private readonly UpdateUser _ValidatorUpdate;
        private readonly ILoggerBase<User> _Logger;

        public UserRepository(ILoggerBase<User> _logger, IConfiguration configuration, CreateUserValidator Validator,
            UpdateUser ValidatorUpdate)
        {
            _ValidatorUpdate = ValidatorUpdate;
            _Validator = Validator;
            //_connectionString = IConfiguration[""];
            _Logger = _logger;
        }

        public async Task<OperationResult> Createasync(CreateUserDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding a new User ${@Entity}", entity);

                var entityvalidate = _Validator.Validate(entity);

                if (entityvalidate != null)
                {
                    _Logger.LogError("Attempted to add a null User entity");

                    result = OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                var presult = await ExecuteStoredProcedureAsync("Usuarios.CreateUser", new SqlParameter("@Nombre", entity.name_user),
                    new SqlParameter("@Apellido", entity.apellido), new SqlParameter("@Email", entity.email)
                    , new SqlParameter("@Password_User", entity.password), new SqlParameter("@ID_Rol", entity.id_rol),
                    new SqlParameter("@Presult", System.Data.SqlDbType.VarChar)
                    {
                        Size = 1000,
                        Direction = System.Data.ParameterDirection.Output
                    });

                if (presult > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "User added successfully.";
                    _Logger.LogInformation("User added successfully with result: {Result}", result);

                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Failed to add User.";
                    _Logger.LogError("Failed to add User. No rows affected.");

                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while adding the User: {ex.Message}";
                _Logger.LogError("An error occurred while adding the User: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }
        
        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> filter)
        {
            OperationResult presult = new OperationResult();
            _Logger.LogInformation("Viendo si existe el usuario");

            if (filter == null)
            {
                _Logger.LogError("El filtro no puede ser nulo");
                OperationResult.Failure("El filtro para buscar el usuario no puede ser nulo");
            }
            var result = await ExecuteScalarStoredProcedureAsync("Usuarios.ExistsProcedure", new SqlParameter("@Id", filter));

            return (result != null && result != DBNull.Value && Convert.ToBoolean(result));
        }

        public async Task<OperationResult> GetAllasync()
        {

            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retriver a User Entities");

                await ExecuteReaderListAsync<GetUserDto>("Usuarios.GetAllUSer", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                });

                result.IsSuccess = true;
                result.Message = "User's retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while retriver the User: {ex.Message}";
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
            }
            finally
            {
            }
            return result;
        }

        public async Task<OperationResult> GetByEmail(string email)
        {
            OperationResult result = new OperationResult();
            
            try
            {
             _Logger.LogInformation("Retriver User entity by email");

                await ExecuteReaderSingleAsync<GetUserDto>("Usuarios.GetByIdUser", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Email", email));

                result.IsSuccess = true;
                result.Message = "User retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while retriver the User: {ex.Message}";
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retriver User entity by id");

              await ExecuteReaderSingleAsync<GetUserDto>("Usuarios.GetByIdUser", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Id", id));

                result.IsSuccess = true;
                result.Message = "User retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while retriver the User: {ex.Message}";
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Updateasync(UpdateUserDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Updating a new User ${@Entity}", entity);

                var entityvalidate = _ValidatorUpdate.Validate(entity);

                if (entityvalidate != null)
                {
                    _Logger.LogError("Attempted to update a null User entity");

                    result = OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                var presult = await ExecuteStoredProcedureAsync("Usuarios.UpdateUser", new SqlParameter("@Id", entity.id),
                    new SqlParameter("@Email", entity.email), new SqlParameter("@Password", entity.password));

                if (presult > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "User updated successfully.";
                    _Logger.LogInformation("User updated successfully with result: {Result}", result);

                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Failed to updating User.";
                    _Logger.LogError("Failed to updating User. No rows affected.");
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while updating the User: {ex.Message}";
                _Logger.LogError("An error occurred while updating the User: {Message}", ex);
            }
            finally
            {
            }
            return result;
        }


        private async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    await connection.OpenAsync();

                    // ExecuteScalarAsync devuelve el valor de la primera columna de la primera fila del resultado.
                    return await command.ExecuteScalarAsync();
                }
            }
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

        private async Task<List<T>> ExecuteReaderListAsync<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
        {
            var items = new List<T>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
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
        private async Task<T> ExecuteReaderSingleAsync<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters) where T : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters);
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
