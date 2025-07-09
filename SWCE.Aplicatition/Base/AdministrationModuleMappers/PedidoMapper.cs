using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.AdministracionModule.PedidoDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base.AdministrationModuleMappers
{
    [Mapper]
    public partial class PedidoMapper
    {
        public Pedido MapToEntity(CancelarPedidoDto dto)
        {
            return new Pedido(dto.PedidoId);
        }
    }
}
