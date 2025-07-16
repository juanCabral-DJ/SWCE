using FluentValidation;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Validators_Registro.AddressValidator
{
    public class CreateAddressValidator : AbstractValidator<Address>
    {

        public CreateAddressValidator()
        {
            RuleFor(x => x.ID_Usuario)
                .NotEmpty().WithMessage("El id del usuario no puede estar vacio.")
                .GreaterThan(0).WithMessage(" El id del usuario debe ser mayor que 0");

            RuleFor(x => x.calle)
                .NotEmpty().WithMessage("La calle no puede estar vacia.")
                .MaximumLength(20).WithMessage(" La calle no puede pasar de los 20 caracteres");

            RuleFor(x => x.ciudad)
                .NotEmpty().WithMessage("La ciudad no puede estar vacia.")
                .MaximumLength(20).WithMessage(" La ciudad no puede pasar de los 20 caracteres");

            RuleFor(x => x.estado_provincia)
                .NotEmpty().WithMessage("El estado o provincia no puede estar vacio")
                .MaximumLength(20).WithMessage(" El estado o provincia no puede pasar de los 20 caracteres");

            RuleFor(x => x.codigo_postal)
                .NotEmpty().WithMessage("El codigo postal no puede estar vacio")
                .MaximumLength(20).WithMessage("El codigo postal no puede pasar de los 20 caracteres");

            RuleFor(x => x.pais)
                .NotEmpty().WithMessage("El pais no puede estar vacio")
                .MaximumLength(20).WithMessage("El pais no puede pasar de los 20 caracteres");

        }
    }
}
