using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.ItemCarritoValidator
{
    public class UpdateItemCantidadValidator : AbstractValidator<UpdateItemCantidadDto>
    {
        public UpdateItemCantidadValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del ítem debe ser mayor a cero.");

            RuleFor(x => x.NewCantidad)
                .GreaterThan(0).WithMessage("La nueva cantidad debe ser mayor a cero.");
        }
    }
}
