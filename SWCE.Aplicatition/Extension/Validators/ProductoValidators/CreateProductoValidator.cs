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
    public class CreateProductoValidator : AbstractValidator<CreateProductoDto>
    {
        public CreateProductoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("La marca es obligatoria")
                .MaximumLength(100).WithMessage("La marca no puede exceder los 100 caracteres");

            /*RuleFor(x => x.CategoriaId)
                .NotNull().WithMessage("La categoría es obligatoria");*/

            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}
