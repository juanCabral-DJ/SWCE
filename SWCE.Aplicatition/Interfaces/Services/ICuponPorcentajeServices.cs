using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWCE.Application.Base.IServiceBase;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICuponPorcentajeServices : IServiceBase<CuponPorcentaje, CreateCuponPorcentajeDto, UpdateCuponPorcentajeDto, DisableCuponPorcentajeDto>
    {
        Task<OperationResult> GetAllasync();
    }
}
