using SWCE.Domain.Entities;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;

namespace SWCE.Application.Extension.Mapeo_Registro.Mapeo_Producto
{
    public static class ProductoMapper
    {
        public static Producto MapToEntityCreate(CreateProductoDto dto)
        {
            return new Producto
            {
                id = dto.Id,
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                IdCategoria = dto.IdCategoria,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }

        public static Producto MapToEntityUpdate(UpdateProductoDto dto)
        {
            return new Producto
            {
                id = dto.Id,
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                IdCategoria = dto.IdCategoria,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }

        public static Producto MapToEntityDisable(DisableProductoDto dto)
        {
            return new Producto
            {
                id = dto.Id
            };
        }
    }
}

