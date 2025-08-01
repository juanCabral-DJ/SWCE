using SWCE.Aplicatition.Dtos.Address;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Models.Address;

namespace SWCE.Web1.HttpServices.MappingAPI
{
    public class MapAddress
    {
        public AddressModel MapToModel(Address source)
        {
            return new AddressModel
            {
                id = source.id,
                calle = source.calle,
                ciudad = source.ciudad,
                estado_provincia = source.estado_provincia,
                codigo_postal = source.codigo_postal,
                pais = source.pais,
                es_predeterminada = source.Es_predeterminada,
                iD_Usuario = source.ID_Usuario
            };  
    }

        public List<AddressModel> MapToModelList(List<Address> sourceList)
        {
            return sourceList.Select(MapToModel).ToList();
        }

        public CreateAddressModel MapToCreateAddressModel(Address source)
        {
            return new CreateAddressModel
            {
                calle = source.calle,
                ciudad = source.ciudad,
                estado_provincia = source.estado_provincia,
                codigo_postal = source.codigo_postal,
                pais = source.pais,
                iD_Usuario = source.ID_Usuario,
                es_predeterminada = source.Es_predeterminada
            };  
    }

        public EditAddressModel MapToEditAddressModel(Address source)
        {
            return new EditAddressModel
            {
                id = source.id
            };  
    }

        public DisableAddressModel MapToDisableAddressModel(Address source)
        {
            return new DisableAddressModel
            {
                id = source.id
            };
        }

             public CreateAddressDto MapToCreateDto(CreateAddressModel model)
        {
            return new CreateAddressDto
            {
                calle = model.calle,
                ciudad = model.ciudad,
                estado_provincia = model.estado_provincia,
                codigo_postal = model.codigo_postal,
                pais = model.pais,
                ID_Usuario = model.iD_Usuario,
                Es_predeterminada = model.es_predeterminada
            }; 
    }

        public UpdateOrDisableAddressDto MapToUpdateDto(EditAddressModel model)
        {
            return new UpdateOrDisableAddressDto
            {
                id = model.id,
                 
            };  
    }

        public UpdateOrDisableAddressDto MapToDisableDto(DisableAddressModel model)
        {
            return new UpdateOrDisableAddressDto
            {
                id = model.id,
            };  
    }
    }
    
}
