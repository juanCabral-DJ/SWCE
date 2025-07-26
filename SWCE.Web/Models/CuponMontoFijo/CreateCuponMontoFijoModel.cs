namespace SWCE.Web.Models.CuponMontoFijo
{
    public class CreateCuponMontoFijoModel
    {
        public int monto { get; set; }
        public int id { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }

    public class CreateCuponMontoFijoResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CuponMontoFijoModel? data { get; set; }
    }
}
