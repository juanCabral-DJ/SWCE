using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System.Linq.Expressions;


namespace SWCE.Persistence.Repositories
{
    public class UserRepository : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly ILoggerBase<User> _Logger;
       
        public UserRepository(ILoggerBase<User> _logger, IConfiguration configuration) 
        {
            //_connectionString = IConfiguration[""];
            _Logger = _logger;
        }

        public Task<OperationResult> Createasync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllasync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetbyIdasync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Updateasync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
