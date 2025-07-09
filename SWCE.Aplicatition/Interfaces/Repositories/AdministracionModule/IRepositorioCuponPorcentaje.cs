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
    public interface IRepositorioCuponPorcentaje : IRepositoryBase<CuponPorcentaje>
    {
        Task<OperationResult> ObtenerPorPorcentaje(decimal porcentaje);
        Task<OperationResult> DisableAsync(int id);

    }
}
