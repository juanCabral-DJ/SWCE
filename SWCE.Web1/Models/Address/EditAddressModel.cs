namespace SWCE.Web1.Models.Address
{
    public class EditAddressModel
    {
        public int id { get; set; } 
    }

    public class EditAddressResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public object data { get; set; }
    }
}
