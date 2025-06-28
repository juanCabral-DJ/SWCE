
using Riok.Mapperly.Abstractions;
using SWCE.Aplicatition.Dtos.Address;

using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Base
{
    [Mapper]
    public partial class AddressMapper
    {
        // Mapea de DTO a entidad
        public partial Address MapToEntityCreate(CreateAddressDto dto);
        public partial Address MapToEntity(UpdateAddressDto dto);

        // Mapea de entidad a DTO
         //public partial Address MapToAddress(Address address);
        //public partial List<GetAddressDto> MapToDtoList(List<Address> addresses);
    }
}
