using FluentValidation;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Application.Validators.Base;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.CuponValidators.CuponMontoFijoValidators
{
    public sealed class CreateCuponMontoFijoValidator : CuponValidator<CuponMontoFijo>
    {
        public CreateCuponMontoFijoValidator()
        {
            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que cero.");
        }
    }
}
