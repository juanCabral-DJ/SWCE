using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Dtos.AdministracionModule.ProductoDtos
{
    internal class DisableProductoDto
    {
        public int Id { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime FechaDeshabilitacion { get; set; } = DateTime.UtcNow;
    }
}
