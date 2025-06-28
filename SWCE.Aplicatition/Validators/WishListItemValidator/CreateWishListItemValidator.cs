using FluentValidation;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Validators.WishListItemValidator
{
    public class CreateWishListItemValidator : AbstractValidator<WishListItem>
    {
         
        public CreateWishListItemValidator() {
            

            RuleFor(x => x.Id_Usuario)
                .NotNull().WithMessage("El id del usuario no puede ser nulo");

            RuleFor(x => x.id_producto)
                .NotNull().WithMessage("El id del producto no puede ser nulo");
        }
    }
}
