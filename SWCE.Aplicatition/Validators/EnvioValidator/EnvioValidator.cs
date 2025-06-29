using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SWCE.Domain.Entities;

namespace SWCE.Aplication.Validators.EnvioValidator
{
    public class EnvioValidator : AbstractValidator<EnvioEntity>
    {
        public EnvioValidator()
        {
            RuleFor(e => e.UsuarioId).NotEmpty();
            RuleFor(e => e.FechaPedido).LessThanOrEqualTo(DateTime.Now);
            RuleFor(e => e.Estado).NotEmpty().MaximumLength(50);
            RuleFor(e => e.Costo).GreaterThan(0);
        }
    }
}
