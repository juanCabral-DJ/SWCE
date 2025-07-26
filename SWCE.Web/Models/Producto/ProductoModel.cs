using SWCE.Web.Models.Categoria;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Web.Models.Producto
{
    public class ProductoModel
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? marca { get; set; }
        public int idCategoria { get; set; }
        public CategoriaModel? categoria { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public bool isDeleted { get; set; }
    }

    public class GetProductoByIdResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public ProductoModel? data { get; set; }
    }

    public class GetAllProductoResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public List<ProductoModel>? data { get; set; }
    }
}
