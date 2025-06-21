using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Dtos.User
{
    public record UpdateUserDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
