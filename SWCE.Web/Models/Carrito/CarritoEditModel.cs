namespace SWCE.Web.Models.Carrito
{
    public class CarritoEditModel
    {
            public int iD_Usuario { get; set; }
            public int id { get; set; }
            public int total { get; set; }
            public bool isDeleted { get; set; }

    }

    public class CarritoEditResponse 
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public CarritoModel data { get; set; }
    }
}
