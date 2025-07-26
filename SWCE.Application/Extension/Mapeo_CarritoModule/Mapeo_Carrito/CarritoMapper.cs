using Microsoft.Data.SqlClient;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Entities;
using System;

namespace SWCE.Application.Base
{
    public static class CarritoMapper
    {

        public static GetCarritoDto MapToGetCarritoDto(SqlDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader), "El SqlDataReader no puede ser nulo.");
            }

            return new GetCarritoDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                ID_Usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                CreateAt = reader.GetDateTime(reader.GetOrdinal("CreateAt")),
                Productos = new List<GetItemCarritoDto>()
            };
        }
        public static UpdateCarritoDto MapToUpdateDto(GetCarritoDto getDto, decimal newTotal)
        {
            return new UpdateCarritoDto
            {
                Id = getDto.Id,
                ID_Usuario = getDto.ID_Usuario,
                Total = newTotal,
                IsDeleted = getDto.IsDeleted
            };
        }

    }
}
