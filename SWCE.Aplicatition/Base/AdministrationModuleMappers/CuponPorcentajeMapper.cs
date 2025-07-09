using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base.AdministrationModuleMappers
{
    public class CuponPorcentajeMapper
    {
        public CuponPorcentaje MapToEntityCreate(CreateCuponPorcentajeDto dto)
        {
            return new CuponPorcentaje(
                id: dto.id,
                porcentaje: dto.Porcentaje,
                expiracion: dto.FechaExpiracion
            );
        }

        public CuponPorcentaje MapToEntity(UpdateCuponPorcentajeDto dto)
        {
            return new CuponPorcentaje(
                id: dto.id,
                porcentaje: dto.Porcentaje,
                expiracion: dto.FechaExpiracion
            );
        }
    }
}
