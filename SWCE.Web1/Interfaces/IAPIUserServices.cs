using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;

namespace SWCE.Web1.Interfaces
{
    public interface IAPIUserServices
    {
        Task<ModelResponse<List<UserModel>>> GetAllUsersAsync();
        Task<ModelResponse<UserModel>> GetUserByIdAsync(int id);
        Task<ModelResponse<UserModel>> GetUserByEmailAsync(string email);
        Task<ModelResponse<UserModelCreate>> CreateUserAsync(UserModelCreate user);
        Task<ModelResponse<UserModelEdit>> UpdateUserAsync(UserModelEdit user);
        Task<ModelResponse<DisableUserModel>> DisableUserAsync(DisableUserModel user);
    }
}
