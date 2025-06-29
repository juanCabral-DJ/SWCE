using SWCE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface ITransportista
    {
        void Enviar(EnvioEntity envio);
        string ObtenerEstado(string trackingId);
    }
}
