
namespace SWCE.Web.Models.Categoria
{
    public class CreateCategoria
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
    }

    public class CreateCategoriaResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CategoriaModel? data { get; set; }
    }
}
