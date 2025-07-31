using SWCE.Web.Models.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Web.Models.Categoria
{
    public class CategoriaModel : CategoriaBaseModel
    {
        // Se pueden agregar propiedades adicionales si es necesario
    }

    public class GetAllCategoriaResponse : BaseApiResponseModel
    {
        public List<CategoriaModel>? data { get; set; }
    }

    public class GetCategoriaByIdResponse : BaseApiResponseModel
    {
        public CategoriaModel? data { get; set; }
    }

    public class GetCategoriasActivasResponse : BaseApiResponseModel
    {
        public List<CategoriaModel>? data { get; set; }
    }
}
