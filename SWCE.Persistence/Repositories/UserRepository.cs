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
        private readonly UpdateUserValidator _ValidatorUpdate;
        private readonly ILoggerBase<User> _Logger;

        public UserRepository(ILoggerBase<User> _logger, IConfiguration configuration, CreateUserValidator Validator,
            UpdateUserValidator ValidatorUpdate)
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

                if (!entityvalidate.IsValid)
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
                    _Logger.LogInformation("User added successfully with result: {Result}", result);
                    return OperationResult.Success("User added successfully.", entity);
                    
                }
                else
                {
                    _Logger.LogError("Failed to add User. No rows affected.");
                    return OperationResult.Failure("Failed to add User.");
                }

            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while adding the User: {Message}", ex);
                return OperationResult.Failure($"An error occurred while adding the User: {ex.Message}");
            }
            finally
            {

            }
        }
        
        public async Task<bool> ExistsAsync(int filter)
        {
            OperationResult presult = new OperationResult();
            _Logger.LogInformation("Viendo si existe el usuario");

            if (filter <= 0)
            {
                _Logger.LogError("El id no puede ser 0 o negativo");
                OperationResult.Failure("El id para buscar el usuario no puede ser 0 o negativo");
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

               var users = await ExecuteReaderListAsync<GetUserDto>("Usuarios.GetAllUSer", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                });

                return OperationResult.Success("Users retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving all Users: {ex.Message}");
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
            }
            finally
            {
            }
        }

        public async Task<OperationResult> GetByEmail(string email)
        {
            OperationResult result = new OperationResult();
            
            try
            {
             _Logger.LogInformation("Retriver User entity by email");

               var user = await ExecuteReaderSingleAsync<GetUserDto>("Usuarios.GetByIdUser", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Email", email));

                if (user != null)
                {
                    return OperationResult.Success("User retrieved successfully.", user);
                }
                else
                {
                    return OperationResult.Failure($"User with email {email} not found.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
                return OperationResult.Failure($"An error occurred while retrieving the User by email: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                _Logger.LogInformation("Retriver User entity by id");

                var user = await ExecuteReaderSingleAsync<GetUserDto>("Usuarios.GetByIdUser", reader => new GetUserDto
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name_user = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Id", id));

                if (user != null)
                {
                    return OperationResult.Success("User retrieved successfully.", user);
                }
                else
                {
                    return OperationResult.Failure($"User with id {id} not found.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
                return OperationResult.Failure($"An error occurred while retrieving the User by id: {ex.Message}");
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

                if (!entityvalidate.IsValid)
                {
                    _Logger.LogError("Attempted to update a null User entity");

                    result = OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                var presult = await ExecuteStoredProcedureAsync("Usuarios.UpdateUser", new SqlParameter("@Id", entity.id),
                    new SqlParameter("@Email", entity.email), new SqlParameter("@Password", entity.password));

                if (presult > 0)
                {
                    _Logger.LogInformation("User updated successfully with result: {Result}", result);
                    return OperationResult.Success("User updated successfully.", entity);
                }
                else
                {
                    _Logger.LogError("Failed to updating User. No rows affected.");
                    return OperationResult.Failure("Failed to update User.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while updating the User: {Message}", ex);
                return OperationResult.Failure($"An error occurred while adding the User: {ex.Message}");
            }
            finally
            {
            }
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
