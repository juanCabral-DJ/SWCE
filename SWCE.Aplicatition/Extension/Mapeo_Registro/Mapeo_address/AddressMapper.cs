using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_address
{
    public static class AddressMapper
    {
        // Mapea de DTO a entidad
        public static Address MapToEntityCreate(CreateAddressDto dto)
        {
            return new Address
            {
                ID_Usuario = dto.ID_Usuario,
                calle = dto.calle,
                ciudad = dto.ciudad,
                estado_provincia = dto.estado_provincia,
                codigo_postal = dto.codigo_postal,
                pais = dto.pais,
                Es_predeterminada = dto.Es_predeterminada
            };
        }
        public static Address MapToEntity(UpdateOrDisableAddressDto dto)
        {
            return new Address
            {
                id = dto.id
            };
        }
    }
}
