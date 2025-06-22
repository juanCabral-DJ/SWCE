using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.CarritoValidator
{
    public class ItemCarritoValidator : AbstractValidator<CreateItemCarritoDto>
    {
        public ItemCarritoValidator()
        {
            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0.");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0.");
        }
    }
}
