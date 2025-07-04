using FluentValidation;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Validators_Registro.WishListItemValidator
{
    public class CreateWishListItemValidator : AbstractValidator<WishListItem>
    {

        public CreateWishListItemValidator()
        {


            RuleFor(x => x.Id_Usuario)
                .NotNull().WithMessage("El id del usuario no puede ser nulo");

            RuleFor(x => x.id_producto)
                .NotNull().WithMessage("El id del producto no puede ser nulo");
        }
    }
}
