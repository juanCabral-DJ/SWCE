using FluentValidation;
using SWCE.Application.Dtos.AdministracionModule.PedidoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.PedidoValidators
{
    public class CancelarPedidoValidator : AbstractValidator<CancelarPedidoDto>
    {
        public CancelarPedidoValidator()
        {
            RuleFor(x => x.PedidoId)
                .GreaterThan(0).WithMessage("El ID del pedido debe ser mayor que 0");
        }
    }
}
