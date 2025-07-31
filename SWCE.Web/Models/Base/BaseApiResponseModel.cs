namespace SWCE.Web.Models.Base
{
    public abstract class BaseApiResponseModel
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
    }
}
