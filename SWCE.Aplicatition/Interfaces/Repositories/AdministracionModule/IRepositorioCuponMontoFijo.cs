using SWCE.Application.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Repositories.AdministracionModule
{
    public interface IRepositorioCuponMontoFijo : IRepositoryBase<CuponMontoFijo>
    {
        Task<OperationResult> ObtenerPorMonto(decimal monto);
        Task<OperationResult> DisableAsync(int id);
    }
}
