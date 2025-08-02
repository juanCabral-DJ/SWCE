using SWCE.Web.Common;
using SWCE.Web.Models;

namespace SWCE.Web.Repositories.Interfaces
{
    public interface IEnvioHttpService
    {
        Task<Result<IEnumerable<EnvioModel>>> GetAllEnviosAsync();
        Task<Result<EnvioModel>> GetEnvioByIdAsync(int id);
        Task<Result<EnvioCreateModelResponse>> CreateEnvioAsync(EnvioCreateModel model);
        Task<Result<EnvioEditModelResponse>> UpdateEnvioAsync(EnvioEditModel model);
    }
}
