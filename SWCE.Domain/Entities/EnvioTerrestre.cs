using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class EnvioTerrestre : EnvioBase
    {
        public EnvioTerrestre(int usuarioId) : base(usuarioId) {}

        public override decimal CalcularCosto()
        {
            return 50.0m;
        }

    }
}
