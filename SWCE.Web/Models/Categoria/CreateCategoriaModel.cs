
using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Categoria
{
    public class CreateCategoria : CategoriaBaseModel
    {
        
    }

    public class CreateCategoriaResponse : BaseApiResponseModel
    {
        public CategoriaModel? data { get; set; }
    }
}
