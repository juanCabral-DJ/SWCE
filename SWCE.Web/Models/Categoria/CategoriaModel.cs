using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Web.Models.Categoria
{
    public class CategoriaModel
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public bool isDeleted { get; set; }
    }

    public class GetAllCategoriaResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public List<CategoriaModel>? data { get; set; }
    }

    public class GetCategoriaByIdResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CategoriaModel? data { get; set; }
    }

    public class GetCategoriasActivasResponse
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public bool isDeleted { get; set; }
    }
}
