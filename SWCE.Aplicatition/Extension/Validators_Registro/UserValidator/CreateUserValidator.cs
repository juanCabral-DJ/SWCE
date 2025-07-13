using FluentValidation;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Validators_Registro.UserValidator
{
    public class CreateUserValidator : AbstractValidator<User>
    {
        public readonly IRepositoryUser _userRepository;

        public CreateUserValidator(IRepositoryUser userRepository)
        {
             _userRepository = userRepository;

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres.");

            RuleFor(x => x.apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(100).WithMessage("El apellido no puede tener más de 100 caracteres.");

            RuleFor(x => x.id_rol)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .Must(role => role > 0).WithMessage("El rol debe ser un número positivo.");

            RuleFor(x => x.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.")
            .MustAsync(async (email, _) => await BeUniqueEmail(email)).WithMessage("El email ya está en uso. Por favor, utiliza otro email.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("Debe tener más de 8 caracteres");
        }

        private async Task<bool> BeUniqueEmail(string email)
        {
            // 1. Llama al método para buscar al usuario por su email.
            var user = await _userRepository.GetByEmail(email);

            return !user.IsSuccess;
        }

    }
}
