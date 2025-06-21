using FluentValidation;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Validators.WishListItemValidator
{
    public class CreateWishListItemValidator : AbstractValidator<CreateWishListItemDto>
    {
        public readonly IRepositoryWishListItem _Wish;
        public CreateWishListItemValidator(IRepositoryWishListItem wish) {
            _Wish = wish;

            RuleFor(x => x.id_user)
                .NotNull().WithMessage("El id del usuario no puede ser nulo");

            RuleFor(x => x.id_producto)
                .NotNull().WithMessage("El id del producto no puede ser nulo");
        }
    }
}
