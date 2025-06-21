using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.Address
{
    public record UpdateAddressDto
    {
        public int id { get; set; } 
        public bool Es_predeterminada { get; set; }
    }
}
