using FluentValidation;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Validators
{
    public class UpdateUserValidator : AbstractValidator<User>
    {
       
        public UpdateUserValidator()
        {


            RuleFor(x => x.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");
           

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("Debe tener más de 8 caracteres");
        }
    }
}
