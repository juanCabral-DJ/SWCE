using FluentValidation;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.ProductoValidators
{
    public sealed class UpdateProductoValidator : AbstractValidator<Producto>
    {
        public UpdateProductoValidator()
        {
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}