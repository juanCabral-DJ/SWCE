using SWCE.Web.Models.Base;
using SWCE.Web.Models.Categoria;

namespace SWCE.Web.Models.Producto
{
    public class UpdateProductoModel : ProductoBaseModel
    {
        public CategoriaModel? categoria { get; set; }
    }

    public class UpdateProductoResponse : BaseApiResponseModel
    {
        public ProductoModel? data { get; set; }
    }
}
