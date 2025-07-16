using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_User
{
    public static class UserMapper
    {
        public static User MapToEntity(UpdateUserDto dto)
        {
            return new User
            {
                id = dto.id,
                email = dto.email,
                password = dto.password
            };
        }

        public static User MapToEntityCreate(CreateUserDto dto)
        {
            {
                return new User
                {
                    id_rol = dto.id_rol,
                    Nombre = dto.Nombre,
                    apellido = dto.apellido,
                    email = dto.email,
                    password = dto.password,
                    Fecha_Creacion = dto.Fecha_Creacion
                };
            }
        }

        public static User MapToEntityDisable(DisableUserDto dto)
        {
            {
                return new User
                {
                    id = dto.id,
                };
            }
        }
    }
}
