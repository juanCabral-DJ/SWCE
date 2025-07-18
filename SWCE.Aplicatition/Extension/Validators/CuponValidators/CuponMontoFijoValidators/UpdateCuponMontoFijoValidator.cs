using FluentValidation;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.CuponValidators.CuponMontoFijoValidators
{
    public class UpdateCuponMontoFijoValidator : AbstractValidator<CuponMontoFijo>
    {
        public UpdateCuponMontoFijoValidator()
        {
            RuleFor(c => c.Monto)
                .GreaterThanOrEqualTo(0).WithMessage("El monto no puede ser negativo.");
        }
    }
}
