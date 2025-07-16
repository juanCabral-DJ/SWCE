using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.CarritoValidator
{
    public class ItemCarritoValidator : AbstractValidator<CreateItemCarritoDto>
    {
        public ItemCarritoValidator()
        {
            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0.");

        }
    }
}
