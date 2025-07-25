namespace SWCE.Web1.Models.WishListItem
{
    public class CreateItemModel
    {
        public int id_producto { get; set; }
        public int id_Usuario { get; set; }
    }

    public class CreateItemResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public ItemModel data { get; set; }
    }
}
