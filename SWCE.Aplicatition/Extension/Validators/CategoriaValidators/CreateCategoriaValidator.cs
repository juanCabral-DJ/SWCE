using FluentValidation;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.Validators.CategoriaValidators
{
    public class CreateCategoriaValidator : AbstractValidator<CreateCategoriaDto>
    {
        public CreateCategoriaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255).WithMessage("La descripción no puede superar los 255 caracteres");
        }
    }
}
