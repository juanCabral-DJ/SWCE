using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class EnvioEntity : Base.EntityBase<int>
    {
        public override int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public string? Estado { get; set; }
        public decimal Costo { get; set; }
        public string? TipoEnvio { get; set; }

    }
}
