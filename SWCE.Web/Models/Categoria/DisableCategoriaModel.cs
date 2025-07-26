namespace SWCE.Web.Models.Categoria
{
    public class DisableCategoriaModel
    {
        int id { get; set; }
    }

    public class DisableCategoriaResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public object? data { get; set; }
    }
}
