using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.Address
{
    public record CreateAddressDto
    {
        public int id_user { get; set; }
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais { get; set; }
        public bool Es_predeterminada { get; set; }
    }
}
