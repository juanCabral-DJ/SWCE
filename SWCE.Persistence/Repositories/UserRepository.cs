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

namespace SWCE.Persistence.Repositories
{
    public class UserRepository : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly UserValidator _Validator;
        private readonly ILoggerBase<User> _Logger;

        public UserRepository(ILoggerBase<User> _logger, IConfiguration configuration, UserValidator Validator)
        {
         
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

                    result = OperationResult.Failure("An error occurred while retrieving InsuranceProvider entities.");
                }

                await ExecuteStoredProcedureAsync("Usuarios.CreateUser", new SqlParameter("@Nombre", entity.name_user),
                    new SqlParameter("@Apellido", entity.apellido), new SqlParameter("@Email", entity.email)
                    , new SqlParameter("@Password_User", entity.password), new SqlParameter("@ID_Rol", entity.id_rol),
                    new SqlParameter("@Presult", System.Data.SqlDbType.VarChar)
                    {
                        Size = 1000,
                        Direction = System.Data.ParameterDirection.Output
                    });

                  //await context.OpenAsync();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while adding the Address type: {ex.Message}";
                _Logger.LogError("An error occurred while adding the Address type: {Message}", ex);
            }
            finally
            {

            }
            return result;
        }

        public Task<bool> ExistsAsync(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetAllasync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetbyIdasync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Updateasync(UpdateUserDto entity)
        {
            throw new NotImplementedException();
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
