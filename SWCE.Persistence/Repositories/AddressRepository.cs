using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Base;
using SWCE.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SWCE.Persistence.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IRepositoryAddress
    {
        private readonly E_commerceContext _Context;
        private readonly ILoggerBase<Address> _logger;
        
        public AddressRepository(E_commerceContext _context, ILoggerBase<Address> _logger) : base(_context)
        {
            _Context = _context;
            _logger = _logger;
        }

        public async override Task<OperationResult> Createasync(Address entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Adding Address entity: ${@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null Address entity");
                    return OperationResult.Failure("Address entity cannot be null");
                }

                await base.Createasync(entity);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"An error occurred while adding the network type: {ex.Message}";
                _logger.LogError("An error occurred while adding the network type: {Message}", ex);
            }
            finally
            {

            }

            return result;
        }

        public async override Task<List<Address>> GetAllasync()
        {
            return await _Context.Direcciones.Where(d => d.IsDeleted == false).ToListAsync();
        }

        public async override Task<Address> GetbyIdasync(int id)
        {
            return await _Context.Direcciones.FindAsync(id);
        }

        public async Task<Address> GetbyPredeterminada(int userid, bool predeterminada)
        {
            return await _Context.Direcciones.FirstOrDefaultAsync(d => d.id_user == userid && d.Es_predeterminada == predeterminada);
        }

        public async Task<List<Address>> GetbyUserId(int userId)
        {
            return await _Context.Direcciones.Where(d => d.id_user == userId).ToListAsync();
        }

        public async override Task<OperationResult> Updateasync(Address entity)
        {
           return await base.Updateasync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Address, bool>> filter)
        {
            return await _Context.Direcciones.AnyAsync(filter);
        }
    }
}
