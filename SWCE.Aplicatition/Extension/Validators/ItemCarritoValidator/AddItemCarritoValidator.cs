using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.ItemCarritoValidator
{
    public class AddItemCarritoValidator : AbstractValidator<AddItemCarritoDto>
    {
        public AddItemCarritoValidator()
        {
            RuleFor(x => x.CarritoId)
                .GreaterThan(0).WithMessage("El ID del carrito debe ser mayor a cero.");

            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a cero.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

        }
    }
}
