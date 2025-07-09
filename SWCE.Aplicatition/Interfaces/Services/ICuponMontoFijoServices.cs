using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICuponMontoFijoServices
    {
        Task<OperationResult> GetbyId(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(CreateCuponMontoFijoDto entity);
        Task<OperationResult> Updateasync(UpdateCuponMontoFijoDto entity);
        Task<OperationResult> DisableAsync(int id);
    }
}
