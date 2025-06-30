using FluentValidation;
using SWCE.Application.Dtos.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.CarritoValidator
{
    public class CreateCarritoValidator : AbstractValidator<CreateCarritoDto>
    {
        public CreateCarritoValidator()
        {
            RuleFor(x => x.ID_Usuario)
                .GreaterThan(0).WithMessage("El Id del usuario debe ser mayor que cero.");
        }
    }
}
