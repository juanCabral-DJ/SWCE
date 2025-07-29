namespace SWCE.Web.Models.CuponPorcentaje
{
    public class UpdateCuponPorcentajeModel
    {
        public int id { get; set; }
        public decimal porcentaje { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }

    public class UpdateCuponPorcentajeResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CuponPorcentajeModel? data { get; set; }
    }
}
