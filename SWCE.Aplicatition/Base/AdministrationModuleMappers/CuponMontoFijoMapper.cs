using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base.AdministrationModuleMappers
{
    [Mapper]
    public partial class CuponMontoFijoMapper
    {
        public CuponMontoFijo MapToEntityCreate(CreateCuponMontoFijoDto dto)
        {
            return new CuponMontoFijo(
                id: dto.id,
                monto: dto.Monto,
                expiracion: dto.FechaExpiracion
            );
        }

        public CuponMontoFijo MapToEntity(UpdateCuponMontoFijoDto dto)
        {
            return new CuponMontoFijo(
                id: dto.id,
                monto: dto.Monto,
                expiracion: dto.FechaExpiracion
            );
        }
    }
}
