using FluentValidation;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.Base
{
    public abstract class CuponValidator <T> : AbstractValidator<T> where T : Cupon
    {
        public CuponValidator()
        {
            RuleFor(c => c.FechaExpiracion)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("El cupón ya está expirado.");
        }
    }
}
