namespace SWCE.Web.Models.Producto
{
    public class UpdateProductoModel
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? marca { get; set; }
        public int idCategoria { get; set; }
        public object? categoria { get; set; }
        public int precio { get; set; }
        public int stock { get; set; }
    }

    public class UpdateProductoResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public ProductoModel? data { get; set; }
    }
}
