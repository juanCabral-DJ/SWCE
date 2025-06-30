using Microsoft.Data.SqlClient;
using Riok.Mapperly.Abstractions;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Base
{
    [Mapper]
    public partial class ItemCarritoMapper
    {
        public partial ItemCarrito MapToEntity(AddItemCarritoDto dto);
        public partial GetItemCarritoDto MapToGetDto(ItemCarrito entity);
    }
}
