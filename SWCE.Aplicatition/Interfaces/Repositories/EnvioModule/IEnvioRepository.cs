using SWCE.Aplication.Interfaces.Repositories;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.EnvioModule
{
    public interface IEnvioRepository : IRepositoryBase<EnvioBase>
    {
        Task<OperationResult> GetByUserId(int userId);
    }
}
