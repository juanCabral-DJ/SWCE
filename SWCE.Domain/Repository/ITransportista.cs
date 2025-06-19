using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface ITransportista
    {
        void Enviar(EnvioBase envio);
        string ObtenerEstado(string trackingId);
    }
}
