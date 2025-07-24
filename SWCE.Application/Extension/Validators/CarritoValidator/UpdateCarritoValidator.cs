using FluentValidation;
using SWCE.Application.Dtos.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.CarritoValidator
{
    public class UpdateCarritoValidator : AbstractValidator<UpdateCarritoDto>
    {
        public UpdateCarritoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id del carrito debe ser mayor que cero.");

            RuleFor(x => x.ID_Usuario)
                .GreaterThan(0).WithMessage("El Id del usuario debe ser mayor que cero.");

            RuleFor(x => x.Total)
                .GreaterThanOrEqualTo(0).WithMessage("El total no puede ser negativo.");
        }
    }
}
