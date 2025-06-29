using Riok.Mapperly.Abstractions;
using SWCE.Domain.Entities;
using SWCE.Aplication.DTOs.Envio;

namespace SWCE.Aplication.Base
{
    [Mapper]
    public partial class EnvioMapper
    {
        public EnvioEntity MapToEntity(UpdateEnvioDto Dto)
        {
            return new EnvioEntity
            {
                Id = Dto.Id,
                Estado = Dto.Estado
            };
        }
        public EnvioEntity MapToEntityCreate(CreateEnvioDto Dto) {
            {
                return new EnvioEntity
                {
                    Id = Dto.Id,
                    UsuarioId = Dto.UsuarioId,
                    FechaPedido = Dto.FechaPedido,
                    Costo = Dto.Costo,
                    Estado = Dto.Estado,
                    TipoEnvio = Dto.TipoEnvio
                };
            }
        }

        
    }
}
