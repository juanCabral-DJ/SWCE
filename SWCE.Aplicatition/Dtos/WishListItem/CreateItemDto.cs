 

namespace SWCE.Aplicatition.Dtos.WishListItem
{
    public record CreateItemDto
    {
        public int id_producto { get; set; }
        public int Id_Usuario { get; set; }
    }
}
