namespace SWCE.Web.Models.Producto
{
    public class CreateProductoModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class CreateProductoResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public ProductoModel? data { get; set; }
    }
}
