using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Categoria
{
    public abstract class CategoriaBaseModel : BaseModel
    {
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
    }
}
