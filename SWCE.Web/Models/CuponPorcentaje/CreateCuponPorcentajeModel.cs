namespace SWCE.Web.Models.CuponPorcentaje
{
    public class CreateCuponPorcentajeModel
    {
        public int id { get; set; }
        public decimal porcentaje { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }

    public class CreateCuponPorcentajeResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CuponPorcentajeModel? data { get; set; }
    }
}
