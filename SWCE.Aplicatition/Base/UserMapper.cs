using Riok.Mapperly.Abstractions;
 
using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Entities.Configuration.User_Perfil;
 

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
