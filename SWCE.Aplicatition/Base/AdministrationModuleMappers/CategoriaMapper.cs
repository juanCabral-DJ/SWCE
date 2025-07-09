using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base.AdministrationModuleMappers
{
    [Mapper]
    public partial class CategoriaMapper
    {
        public Categoria MapToEntityCreate(CreateCategoriaDto dto)
        {
            return new Categoria(
                dto.id,
                dto.Nombre!,
                dto.Descripcion!
            );
        }

        public Categoria MapToEntity(UpdateCategoriaDto dto)
        {
            return new Categoria(
                dto.Id,
                dto.Nombre!,
                dto.Descripcion!
            );
        }   
    }
}
