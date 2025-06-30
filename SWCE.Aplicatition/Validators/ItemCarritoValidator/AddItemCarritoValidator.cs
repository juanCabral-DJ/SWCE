using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Validators.ItemCarritoValidator
{
    public class AddItemCarritoValidator : AbstractValidator<AddItemCarritoDto>
    {
        public AddItemCarritoValidator()
        {
            RuleFor(x => x.CarritoId)
                .GreaterThan(0).WithMessage("El ID del carrito debe ser mayor a cero.");

            RuleFor(x => x.ProductoId)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a cero.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");

            RuleFor(x => x.NombreProducto)
                .NotEmpty().WithMessage("El nombre del producto es requerido.")
                .MaximumLength(255).WithMessage("El nombre del producto no puede exceder los 255 caracteres.");
        }
    }
}
