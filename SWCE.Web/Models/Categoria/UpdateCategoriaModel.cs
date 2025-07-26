namespace SWCE.Web.Models.Categoria
{
    public class UpdateCategoriaModel
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
    }

    public class UpdateCategoriaResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CategoriaModel? data { get; set; }
    }
}
