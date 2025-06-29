using SWCE.Aplication.DTOs.Envio;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplication.Interfaces.Services
{
    public interface IEnvioServices
    {
        Task<OperationResult> GetByUserId(int userId);
        Task<OperationResult> GetByIdAsync(Guid id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> CreateAsync(CreateEnvioDto entity);
        Task<OperationResult> UpdateAsync(UpdateEnvioDto entity);
    }
}
