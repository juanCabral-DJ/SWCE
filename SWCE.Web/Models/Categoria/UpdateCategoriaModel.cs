using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Categoria
{
    public class UpdateCategoriaModel : CategoriaBaseModel
    {

    }

    public class UpdateCategoriaResponse : BaseApiResponseModel
    {
        public CategoriaModel? data { get; set; }
    }
}
