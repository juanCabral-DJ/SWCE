using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplication.DTOs.Envio
{
    public record class UpdateEnvioDto
    {
        public Guid Id { get; set; }
        public string? Estado { get; set; }
    }
}
