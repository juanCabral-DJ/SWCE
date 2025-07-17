using FluentValidation;
using SWCE.Application.Extension.Validators.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.CuponValidators.CuponPorcentajeValidators
{
    public sealed class CreateCuponPorcentajeValidator : CuponValidator<CuponPorcentaje>
    {
        public CreateCuponPorcentajeValidator()
        {
            RuleFor(x => x.Porcentaje)
                .InclusiveBetween(1, 100)
                .WithMessage("El porcentaje debe estar entre 1% y 100%.");
        }
    }
}
