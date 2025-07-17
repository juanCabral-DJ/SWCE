using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
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
    public interface ICuponMontoFijoServices : IServiceBase<CuponMontoFijo, CreateCuponMontoFijoDto, UpdateCuponMontoFijoDto, DisableCuponMontoFijoDto>
    {
        Task<OperationResult> GetAllasync();
    }
}
