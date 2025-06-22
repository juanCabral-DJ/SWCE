using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class EnvioExpress : EnvioBase
    {
        public EnvioExpress(Guid pedidoId) : base(pedidoId)
        {
        }
        public override decimal CalcularCosto()
        {
            throw new NotImplementedException();
        }

    }
}
