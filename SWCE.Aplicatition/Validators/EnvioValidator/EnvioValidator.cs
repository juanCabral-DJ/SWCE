using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SWCE.Domain.Base;

namespace SWCE.Aplication.Validators.EnvioValidator
{
    public class EnvioValidator : AbstractValidator<EnvioBase>
    {
        public EnvioValidator()
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("El UsuarioId no puede estar vacío.")
                .NotNull().WithMessage("El UsuarioId no puede ser nulo.");
        }
    }
}
