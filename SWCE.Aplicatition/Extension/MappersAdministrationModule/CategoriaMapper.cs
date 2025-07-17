using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.MappersAdministrationModule
{
    public static class CategoriaMapper
    {
        public static Categoria MapToEntityCreate(CreateCategoriaDto dto)
        {
            return new Categoria(
                id: dto.Id,
                nombre: dto.Nombre ?? string.Empty,
                descripcion: dto.Descripcion ?? string.Empty
            );
        }

        public static Categoria MapToEntityUpdate(UpdateCategoriaDto dto)
        {
            return new Categoria(
                id: dto.Id,
                nombre: dto.Nombre ?? string.Empty,
                descripcion: dto.Descripcion ?? string.Empty
            );
        }

        public static Categoria MapToEntityDisable(DisableCategoriaDto dto)
        {
            return new Categoria(
                id: dto.Id,
                nombre: string.Empty,
                descripcion: string.Empty
            );
        }
    }
}
