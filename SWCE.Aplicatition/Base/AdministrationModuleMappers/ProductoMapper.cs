using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base.AdministrationModuleMappers
{
    [Mapper]
    public partial class ProductoMapper
    {
        public Producto MapToEntityCreate(CreateProductoDto dto)
        {
            /*return new Producto(
                dto.id,
                dto.Nombre ?? string.Empty,
                dto.Marca ?? string.Empty,
                dto.Categoria ?? new Categoria(0, "Sin categoria", string.Empty),
                dto.Precio,
                dto.Stock
            );*/

            return new Producto
            {
                Nombre = dto.Nombre ?? string.Empty,
                Marca = dto.Marca ?? string.Empty,
                IdCategoria = dto.IdCategoria,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }

        public Producto MapToEntity(UpdateProductoDto dto, int id)
        {
            /*return new Producto(
                id,
                dto.Nombre ?? string.Empty,
                dto.Marca ?? string.Empty,
                dto.Categoria ?? new Categoria(0, "Sin categoria", string.Empty),
                dto.Precio,
                dto.Stock
            );*/

            return new Producto
            {
                id = id,
                Nombre = dto.Nombre ?? string.Empty,
                Marca = dto.Marca ?? string.Empty,
                IdCategoria = dto.IdCategoria,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }
    }
}
