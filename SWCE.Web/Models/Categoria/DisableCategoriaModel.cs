using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Categoria
{
    public class DisableCategoriaModel : BaseModel
    { }

    public class DisableCategoriaResponse : BaseApiResponseModel
    {
        public DisableCategoriaModel? data { get; set; }
    }
}
