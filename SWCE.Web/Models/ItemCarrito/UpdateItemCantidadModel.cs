using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.ItemCarrito
{
    public class UpdateItemCantidadModel : BaseModel
    {
        public int NewCantidad { get; set; }
        public int CarritoId { get; set; } 
    }
}
