﻿using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public class EnvioTerrestre : EnvioBase
    {
        public EnvioTerrestre(Pedido pedido) : base(pedido)
        {
        }

        public override decimal CalcularCosto()
        {
            return 50.0m;
        }

        public override string GenerarGuia()
        {
            return "GT10322";
        }
    }
}
