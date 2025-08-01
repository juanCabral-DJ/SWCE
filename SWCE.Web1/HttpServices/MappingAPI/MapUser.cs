using SWCE.Aplicatition.Dtos.User;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Models.User;

namespace SWCE.Web1.HttpServices.MappingAPI
{
    public class MapUser
    {
        public UserModel MapToModel(User source)
        {
            return new UserModel
            {
                id = source.id,
                nombre = source.Nombre,
                apellido = source.apellido,
                email = source.email,
                fecha_Creacion = source.Fecha_Creacion
            };  
    }

        public List<UserModel> MapToModelList(List<User> sourceList)
        {
            return sourceList.Select(MapToModel).ToList();
        }

        public UserModelCreate MapToUserModelCreate(User source)
        {
            return new UserModelCreate
            {
                id_rol = source.id_rol,
                nombre = source.Nombre,
                apellido = source.apellido,
                email = source.email,
                fecha_Creacion = source.Fecha_Creacion
            };  
    }

        public UserModelEdit MapToUserModelEdit(User source)
        {
            return new UserModelEdit
            {
                email = source.email,
                password = source.password
            };  
    }

        public DisableUserModel MapToDisableUserModel(User source)
        {
            return new DisableUserModel
            {
                id = source.id
            }; 
    }
        public CreateUserDto MapToCreateDto(UserModelCreate model)
        {
            return new CreateUserDto
            {
                Nombre = model.nombre,
                apellido = model.apellido,
                email = model.email,
                password = model.password,
                id_rol = model.id_rol,
                Fecha_Creacion = DateTime.Now
            };  
    }

        public UpdateUserDto MapToUpdateDto(UserModelEdit model)
        {
            return new UpdateUserDto
            {
                id = model.id,
                email = model.email,
                password = model.password
            }; 
    }

        public DisableUserDto MapToDisableDto(DisableUserModel model)
        {
            return new DisableUserDto
            {
                id = model.id
            };  
    }
    }
}
