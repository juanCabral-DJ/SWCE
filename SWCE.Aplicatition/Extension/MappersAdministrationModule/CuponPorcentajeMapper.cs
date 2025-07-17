using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.MappersAdministrationModule
{
    public static class CuponPorcentajeMapper
    {
        public static CuponPorcentaje MapToEntityCreate(CreateCuponPorcentajeDto dto)
        {
            return new CuponPorcentaje
            {
                id = dto.Id,
                Porcentaje = dto.Porcentaje,
                FechaExpiracion = dto.FechaExpiracion
            };
        }
        public static CuponPorcentaje MapToEntityUpdate(UpdateCuponPorcentajeDto dto)
        {
            return new Domain.Entities.CuponPorcentaje
            {
                id = dto.Id,
                Porcentaje = dto.Porcentaje,
                FechaExpiracion = dto.FechaExpiracion
            };
        }

        public static CuponMontoFijo MapToEntityDisable(DisableCuponMontoFijoDto dto)
        {
            return new CuponMontoFijo
            {
                id = dto.Id
            };
        }
    }
}
