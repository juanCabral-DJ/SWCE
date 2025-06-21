using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.Address
{
    public record GetAddressDto
    {
        public int id { get; set; }
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais { get; set; }
        public string Es_predeterminada { get; set; }
    }
}
