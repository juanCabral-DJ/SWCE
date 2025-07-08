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
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El id del usuario no puede estar vacio");
            RuleFor(x => x.FechaPedido).LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.Estado).NotEmpty().WithMessage("El estado del pedido no puede estar vacio").MaximumLength(50).WithMessage("El limite de caracteres son 50");
            RuleFor(x => x.Costo).GreaterThan(-1).WithMessage("El costo debe tener algun valor");
        }
    }
}
