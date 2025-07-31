using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.API_Interface
{
    public interface IAPIUserRepository
    {
        Task<OperationResult> GetAllUsersAsync();
        Task<OperationResult> GetUserByIdAsync(int id);
        Task<OperationResult> GetUserByEmailAsync(string email);
        Task<OperationResult> CreateUserAsync(CreateUserDto user);
        Task<OperationResult> UpdateUserAsync(UpdateUserDto user);
        Task<OperationResult> DisableUserAsync(DisableUserDto user);
    }
}
