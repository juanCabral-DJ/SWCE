using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Web.Models.Producto
{
    public class ProductoModel : ProductoBaseModel
    {
        public CategoriaModel? categoria { get; set; }
    }
}