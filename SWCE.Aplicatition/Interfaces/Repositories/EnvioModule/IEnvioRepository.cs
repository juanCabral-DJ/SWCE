using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Interfaces.Repositories.EnvioModule
{
    public interface IEnvioRepository
    {
        Task<EnvioBase> ObtenerPorIdAsync(Guid id);
        Task<List<EnvioBase>> ObtenerTodosAsync();
        Task CrearAsync(EnvioBase envio);
        Task ActualizarAsync(EnvioBase envio);
        Task EliminarAsync(Guid id);
    }
}
