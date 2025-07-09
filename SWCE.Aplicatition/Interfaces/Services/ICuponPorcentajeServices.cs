using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface ICuponPorcentajeServices
    {
        Task<OperationResult> GetbyId(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(CreateCuponPorcentajeDto entity);
        Task<OperationResult> Updateasync(UpdateCuponPorcentajeDto entity);
        Task<OperationResult> DisableAsync(int id);
    }
}
