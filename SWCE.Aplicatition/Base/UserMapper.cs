using Riok.Mapperly.Abstractions;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Base
{
    [Mapper]
    public partial class UserMapper  
    {
        public partial User MapToEntity(UpdateUserDto dto);

        public User MapToEntityCreate(CreateUserDto dto)
        {
            {
                return new User
                {
                    id_rol = dto.id_rol,
                    Nombre = dto.name_user,
                    apellido = dto.apellido,
                    email = dto.email,
                    password = dto.password,
                    Fecha_Creacion = dto.Fecha_Creacion
                };
            }
        }
    }
}
