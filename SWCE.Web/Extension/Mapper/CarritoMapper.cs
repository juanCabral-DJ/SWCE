using SWCE.Web.Models.Carrito;

namespace SWCE.Web.Extension.Mapper
{
    public static class CarritoMapper
    {
        public static EditCarritoModel ToEditViewModel(this CarritoModel model)
        {
            return new EditCarritoModel
            {
                Id = model.Id,
                total = (int)model.total,
                isDeleted = model.isDeleted,
                iD_Usuario = model.iD_Usuario
            };
        }
    }
}
