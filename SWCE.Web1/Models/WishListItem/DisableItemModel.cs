namespace SWCE.Web1.Models.WishListItem
{
    public class DisableItemModel
    {
            public int id  { get; set; }
         

        public class DisableItemResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public ItemModel data { get; set; }
        }
    }
}
