using SWCE.Web1.Models.WishListItem;

namespace SWCE.Web1.Models.Address
{
    public class DisableAddressModel
    {
        public int id { get; set; }


        public class DisableAddressResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public AddressModel data { get; set; }
        }
    }
}
