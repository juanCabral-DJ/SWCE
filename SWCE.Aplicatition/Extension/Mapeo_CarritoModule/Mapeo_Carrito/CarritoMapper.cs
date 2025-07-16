using Microsoft.Data.SqlClient;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Entities;
using System;

namespace SWCE.Application.Base
{
    public static class CarritoMapper
    {
        public static Carrito MapToEntity(CreateCarritoDto dto)
        {
            if (dto == null) return null;

            return new Carrito
            {
                IdUsuario = dto.ID_Usuario
            };
        }

        public static void MapToEntity(UpdateCarritoDto dto, Carrito entity)
        {
            if (dto == null || entity == null) return;

            entity.Id = dto.Id;
            entity.IdUsuario = dto.IdUsuario;
            entity.Total = dto.Total;
            entity.IsDeleted = dto.IsDeleted;
        }

        public static GetCarritoDto MapToGetCarritoDto(Carrito entity)
        {
            if (entity == null) return null;

            return new GetCarritoDto
            {
                Id = entity.Id,
                IdUsuario = entity.IdUsuario,
                Total = entity.Total,
                IsDeleted = entity.IsDeleted,
                Productos = entity.productos != null
                                ? entity.productos.Select(ItemCarritoMapper.MapToGetDto).ToList()
                                : new List<GetItemCarritoDto>()
            };
        }

        public static GetCarritoDto MapToGetCarritoDtoFromReader(SqlDataReader reader)
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
