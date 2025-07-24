namespace SWCE.Web1.Models.User
{
    public class DisableUserModel
    {
            public int id { get; set; }
    }

    public class DisableUserResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public object data { get; set; }
    }
}
