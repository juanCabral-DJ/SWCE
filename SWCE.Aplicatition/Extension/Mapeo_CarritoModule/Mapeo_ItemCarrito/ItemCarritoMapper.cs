using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Entities;

namespace SWCE.Application.Base
{
    public static class ItemCarritoMapper
    {
        public static ItemCarrito MapToEntity(AddItemCarritoDto dto)
        {
            return new ItemCarrito
            {
                CarritoId = dto.CarritoId,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad
                // PrecioUnitario y SubTotal los llenas luego en el servicio (como ya haces)
            };
        }

        public static GetItemCarritoDto MapToGetDto(ItemCarrito entity)
        {
            return new GetItemCarritoDto
            {
                Id = entity.Id,
                CarritoId = entity.CarritoId,
                IdProducto = entity.IdProducto,
                Cantidad = entity.Cantidad,
                NombreProducto = entity.NombreProducto,
                PrecioUnitario = entity.PrecioUnitario,
                Subtotal = entity.SubTotal
            };
        }
    }
}
