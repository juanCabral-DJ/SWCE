using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.ItemCarrito
{
    public record UpdateItemCantidadDto
    {
        public int ItemId { get; set; }
        public int NewCantidad { get; set; }
    }
}
