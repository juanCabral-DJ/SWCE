using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class EnvioEntity
    {
        public int Id { get; set; }
        public string TipoEnvio { get; set; } 
        public decimal Costo { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; } 
        public int UsuarioId { get; set; } 
    }
}
