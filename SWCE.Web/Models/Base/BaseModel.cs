namespace SWCE.Web.Models.Base
{
    public abstract class BaseModel
    {
        public int id { get; set; }
        public bool isDeleted { get; set; }
    }
}
