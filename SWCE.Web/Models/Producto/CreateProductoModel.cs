using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Producto
{
    public class CreateProductoModel : ProductoBaseModel
    {
        //Hereda todo de ProductoBaseModel hasta ahora
    }

    public class CreateProductoResponse : BaseApiResponseModel
    {
        public ProductoModel? data { get; set; }
    }
}
