using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;

namespace SWCE.Web.Models.Producto
{
    public abstract class ProductoBaseModel : BaseModel
    {
        public string? nombre { get; set; }
        public string? marca { get; set; }
        public int idCategoria { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
    }
}