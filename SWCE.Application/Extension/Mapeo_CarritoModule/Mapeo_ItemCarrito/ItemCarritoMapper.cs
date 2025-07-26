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
                Cantidad = dto.Cantidad,

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
                NombreProducto = entity.Producto?.Nombre ?? "N/A",
                PrecioUnitario = entity.PrecioUnitario,
                SubTotal = entity.SubTotal
            };
        }
    }
}
