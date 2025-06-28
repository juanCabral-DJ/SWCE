using FluentValidation;
 
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Validators.AddressValidator
{
    public class CreateAddressValidator : AbstractValidator<Address>
    {

        public CreateAddressValidator()
        {


            RuleFor(x => x.calle)
                .NotEmpty().WithMessage("La calle no puede estar vacia")
                .MaximumLength(255).WithMessage("La calle no puede pasar de los 255 caracteres");

            RuleFor(x => x.ciudad)
                .NotEmpty().WithMessage("La ciudad no puede estar vacia")
                .MaximumLength(100).WithMessage("La ciudad no puede pasar de los 100 caracteres");

            RuleFor(x => x.estado_provincia)
                .NotEmpty().WithMessage("El estado o provincia no puede estar vacio")
                .MaximumLength(100).WithMessage("EL estado o provincia no puede pasar de los 100 caracteres");

            RuleFor(x => x.codigo_postal)
                .NotEmpty().WithMessage("El codigo postal no puede estar vacio")
                .MaximumLength(20).WithMessage("El codigo postal no puede pasar de los 20 caracteres");

            RuleFor(x => x.pais)
                .NotEmpty().WithMessage("El pais no puede estar vacio")
                .MaximumLength(100).WithMessage("El pais no puede pasar de los 100 caracteres");

        }
    }
}
