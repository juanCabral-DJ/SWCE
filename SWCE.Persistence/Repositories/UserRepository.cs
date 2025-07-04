
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using System.Data;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Extension.Validators_Registro.UserValidator;

namespace SWCE.Persistence.Repositories
{
    public class UserRepository : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly CreateUserValidator _Validator;
        private readonly UpdateUserValidator _ValidatorUpdate;
        private readonly ILoggerBase<User> _Logger;

        public UserRepository(ILoggerBase<User> _logger, IConfiguration configuration, CreateUserValidator Validator,
            UpdateUserValidator ValidatorUpdate)
        {
            _configuration = configuration;
            _ValidatorUpdate = ValidatorUpdate;
            _Validator = Validator;
            _connectionString = _configuration["ConnectionStrings:E-commerceConnection"];
            _Logger = _logger;
        }

        public async Task<OperationResult> Createasync(User entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Adding a new User ${@Entity}", entity);

                var entityvalidate = await _Validator.ValidateAsync(entity);

                if (!entityvalidate.IsValid)
                {
                    _Logger.LogError("Attempted to add a null User entity");

                    return OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                  var presult = await ExecuteStoredProcedureAsync("dbo.CreateUser", new SqlParameter("@Nombre", entity.Nombre),
                    new SqlParameter("@Apellido", entity.apellido), new SqlParameter("@Email", entity.email)
                    , new SqlParameter("@Password_User", entity.password), new SqlParameter("@ID_Rol", entity.id_rol));

                if (presult > 0)
                {
                    _Logger.LogInformation("User added successfully with result: {Result}", result);
                    return OperationResult.Success("User added successfully.", entity);
                    
                }
                else
                {
                    _Logger.LogError("Failed to add User. No rows affected.");
                    result = OperationResult.Failure("Failed to add User.");
                }

            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while adding the User: {Message}", ex);
                result = OperationResult.Failure($"An error occurred while adding the User:", ex);
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllasync()
        {

            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Retriver a User Entities");

                var users = await ExecuteReaderListAsync<User>("dbo.GetAllUsers", reader => new User
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                });
                
                result = OperationResult.Success("Users retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
                result = OperationResult.Failure($"An error occurred while retrieving all Users: {ex.Message}");
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

               var user = await ExecuteReaderSingleAsync<User>("dbo.GetUserByEmail", reader => new User
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Email", email));

                if (user != null)
                {
                    result = OperationResult.Success("User retrieved successfully.", user);
                }
                else
                {
                    result = OperationResult.Failure($"User with email {email} not found.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
                result = OperationResult.Failure($"An error occurred while retrieving the User by email: {ex.Message}");
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

                var user = await ExecuteReaderSingleAsync<User>("GetUserById", reader => new User
                {
                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                    email = reader.GetString(reader.GetOrdinal("Email"))
                }, new SqlParameter("@Id", id));

                if (user != null)
                {
                    result = OperationResult.Success("User retrieved successfully.", user);
                }
                else
                {
                    result = OperationResult.Failure($"User with id {id} not found.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while retriver the User: {Message}", ex);
                result = OperationResult.Failure($"An error occurred while retrieving the User by id: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Updateasync(User entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Updating a new User ${@Entity}", entity);

                var entityvalidate = await _ValidatorUpdate.ValidateAsync(entity);

                if (!entityvalidate.IsValid)
                {
                    _Logger.LogError("Attempted to update a null User entity");

                    return OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                var presult = await ExecuteStoredProcedureAsync("dbo.UpdateUser", new SqlParameter("@Id", entity.id),
                    new SqlParameter("@Email", entity.email), new SqlParameter("@Password_User", entity.password));

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
        public async Task<OperationResult> Disableasync(User entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _Logger.LogInformation("Updating a new User ${@Entity}", entity);

                if (entity == null)
                {
                    _Logger.LogError("Attempted to update a null User entity");

                    return OperationResult.Failure("An error occurred while retrieving User entities.");
                }

                var presult = await ExecuteStoredProcedureAsync("dbo.DisableUser", new SqlParameter("@Id", entity.id));

                if (presult > 0)
                {   
                    _Logger.LogInformation("User updated successfully with result: {Result}", result);
                    result = OperationResult.Success("User updated successfully.", entity);
                }
                else
                {
                    _Logger.LogError("Failed to updating User. No rows affected.");
                    result = OperationResult.Failure("Failed to update User.");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError("An error occurred while updating the User: {Message}", ex);
                result = OperationResult.Failure($"An error occurred while adding the User: {ex.Message}");
            }
            finally
            {
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

         private async Task<List<User>> ExecuteReaderListAsync<T>(string sql, Func<SqlDataReader, User> map, params SqlParameter[] parameters)
         {
             var items = new List<User>();
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
