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
    public sealed class UpdateCuponPorcentajeValidator : CuponValidator<CuponPorcentaje>
    {
        public UpdateCuponPorcentajeValidator()
        {
            RuleFor(c => c.Porcentaje)
                .InclusiveBetween(1, 100)
                .WithMessage("Si se actualiza, el porcentaje debe estar entre 1% y 100%.");
        }

    }
}
