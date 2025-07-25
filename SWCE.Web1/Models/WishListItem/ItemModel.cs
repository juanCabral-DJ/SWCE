namespace SWCE.Web1.Models.WishListItem
{
    public class ItemModel
    {
            public int id { get; set; }
            public int id_producto { get; set; }
            public int id_Usuario { get; set; }
            public bool isDeleted { get; set; }
 
        public class GetAllItemResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public List<ItemModel> data { get; set; }
        }

        public class GetByUseridItemResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public List<ItemModel> data { get; set; }
        }

        public class GetByIdItemResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public ItemModel  data { get; set; }
        }

    }
}
