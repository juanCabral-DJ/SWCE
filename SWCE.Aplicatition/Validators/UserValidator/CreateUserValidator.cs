using FluentValidation;
 
using SWCE.Domain.Entities.Configuration.User_Perfil;
 

namespace SWCE.Aplicatition.Validators
{
    public class CreateUserValidator : AbstractValidator<User>
    {
         

        public CreateUserValidator()
        {
            

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres.");

            RuleFor(x => x.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("Debe tener más de 8 caracteres");
        }

    }
}
