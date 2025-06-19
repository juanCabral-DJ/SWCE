using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class EnvioAereo : EnvioBase
    {
        public EnvioAereo(Pedido pedido) : base(pedido)
        {
        }

        public override decimal CalcularCosto()
        {
            throw new NotImplementedException();
        }

        public override string GenerarGuia()
        {
            throw new NotImplementedException();
        }
    }
}
