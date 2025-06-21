using FluentValidation;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Validators
{
    public class UpdateUser : AbstractValidator<UpdateUserDto>
    {
        public readonly IRepositoryUser _UserRepository;
        public UpdateUser(IRepositoryUser repositoryUser)
        {
            _UserRepository = repositoryUser;

            RuleFor(x => x.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.")
            .MustAsync(UniqueEmail).WithMessage("El correo electrónico ya está registrado.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(8).WithMessage("Debe tener más de 8 caracteres");
        }
        private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _UserRepository.GetByEmail(email) == null;
        }
    }
}
