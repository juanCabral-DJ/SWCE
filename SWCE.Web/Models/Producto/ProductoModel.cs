using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Producto
{
    public class ProductoModel : BaseModel
    {
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subTotal { get; set; }
        public int carritoId { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
    }
}
