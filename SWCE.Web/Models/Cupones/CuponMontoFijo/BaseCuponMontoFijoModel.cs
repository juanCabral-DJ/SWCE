using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Cupones.CuponMontoFijo
{
    public abstract class BaseCuponMontoFijoModel : BaseCupon
    {
        public decimal monto { get; set; }
    }
}
