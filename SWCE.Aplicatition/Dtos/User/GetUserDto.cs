using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.User
{
    public record GetUserDto
    {
        public int id { get; set; }
        public int id_rol { get; set; }
        public string name_user { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }
}
