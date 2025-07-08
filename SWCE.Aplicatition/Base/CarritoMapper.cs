using Microsoft.Data.SqlClient;
using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.Carrito;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base
{
    [Mapper]
    public partial class CarritoMapper
    {
        public partial Carrito MapToEntity(CreateCarritoDto dto);

        [MapProperty(nameof(UpdateCarritoDto.Id), nameof(Carrito.Id))]
        public partial void MapToEntity(UpdateCarritoDto dto, Carrito entity);

        [MapProperty(nameof(Carrito.Id), nameof(GetCarritoDto.Id))]
        public partial GetCarritoDto MapToGetCarritoDto(Carrito entity);


        public GetCarritoDto MapToGetCarritoDtoFromReader(SqlDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader), "El SqlDataReader no puede ser nulo.");
            }

            return new GetCarritoDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")), 
                IdUsuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                CreateAt = reader.GetDateTime(reader.GetOrdinal("CreateAt")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            };
        }
    }
}
