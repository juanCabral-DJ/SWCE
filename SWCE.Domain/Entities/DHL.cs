using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class DHL
    {
        public void Enviar(EnvioBase envio)
        {
            Console.WriteLine("Enviando con DHL...");
        }

        public string ObtenerEstado(string trackingId)
        {
            return "En tránsito";
        }
    }
}
