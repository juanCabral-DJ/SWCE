using FluentValidation;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplication.Validators.EnvioValidator
{
    public class UpdateEnvioValidator : AbstractValidator<EnvioEntity>
    {
        public UpdateEnvioValidator()
        {
            RuleFor(e => e.Estado).NotEmpty().MaximumLength(50);
        }
    }
}
