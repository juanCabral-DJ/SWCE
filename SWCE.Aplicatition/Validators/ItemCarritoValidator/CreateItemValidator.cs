using FluentValidation;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.ItemCarritoValidator
{
    public class CreateItemCarritoValidator : AbstractValidator<ItemCarrito>
    {
        public CreateItemCarritoValidator()
        {
            RuleFor(item => item.CarritoId)
                .GreaterThan(0).WithMessage("El ID del carrito debe ser mayor a 0.");

            RuleFor(item => item.IdProducto)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a 0.");

            RuleFor(item => item.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

        }
    }
}
