namespace SWCE.Web.Models.Carrito
{
    public class CarritoCreateModel
    {
        public int iD_Usuario { get; set; }
    }

    public class CarritoCreateResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public CarritoModel data { get; set; }
    }
}
