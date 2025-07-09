using SWCE.Aplication.DTOs.Envio;
using SWCE.Domain.Entities;


namespace SWCE.Aplication.Extension.MapeoEnvio
{
    public static class EnvioMapper
    {
        public static EnvioEntity MapToEntity(UpdateEnvioDto Dto)
        {
            return new EnvioEntity
            {
                Id = Dto.Id,
                Estado = Dto.Estado
            };
        }
        public static EnvioEntity MapToEntityCreate(CreateEnvioDto Dto)
        {
            {
                return new EnvioEntity
                {
                    UsuarioId = Dto.UsuarioId,
                    FechaPedido = Dto.FechaPedido,
                    Estado = Dto.Estado,
                    Costo = Dto.Costo,
                    TipoEnvio = Dto.TipoEnvio
                };
            }
        }
    }
}
