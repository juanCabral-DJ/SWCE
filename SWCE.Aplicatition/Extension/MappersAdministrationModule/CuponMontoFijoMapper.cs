using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Extension.MappersAdministrationModule
{
    public static class CuponMontoFijoMapper
    {
        public static CuponMontoFijo MapToEntityCreate(CreateCuponMontoFijoDto dto)
        {
            return new CuponMontoFijo
            {
                id = dto.Id,
                Monto = dto.Monto,
                FechaExpiracion = dto.FechaExpiracion
            };
        }

        public static CuponMontoFijo MapToEntityUpdate(UpdateCuponMontoFijoDto dto)
        {
            return new CuponMontoFijo
            {
                id = dto.Id,
                Monto = dto.Monto,
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
