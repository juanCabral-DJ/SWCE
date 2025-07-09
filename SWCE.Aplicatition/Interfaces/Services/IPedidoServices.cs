using SWCE.Application.Dtos.AdministracionModule.PedidoDtos;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Interfaces.Services
{
    public interface IPedidoServices
    {
        Task<OperationResult> GetbyId(int id);
        Task<OperationResult> GetPedidosByClienteId(int clienteId);
        Task<OperationResult> CancelarPedido(CancelarPedidoDto dto);
    }
}
