using SWCE.Web.Models.Base;

namespace SWCE.Web.Models.Carrito
{
    public class EditCarritoModel : BaseModel
    {
            public int iD_Usuario { get; set; }
            public int total { get; set; }
            public bool isDeleted { get; set; }

    }
}
