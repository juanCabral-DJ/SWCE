using FluentValidation;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Validators_Registro.UserValidator
{
    public class UpdateUserValidator : AbstractValidator<User>
    {
        public readonly IRepositoryUser _userRepository;

        public UpdateUserValidator(IRepositoryUser userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.")
            .MustAsync(async (email, _) => await BeUniqueEmail(email)).WithMessage("El email debe existir. Por favor, utiliza otro email o registra ese.");

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
