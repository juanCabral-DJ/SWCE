using SWCE.Web.Models.CuponMontoFijo;

namespace SWCE.Web.Models.CuponPorcentaje
{
    public class CuponPorcentajeModel
    {
        public int id { get; set; }
        public decimal porcentaje { get; set; }
        public DateTime fechaExpiracion { get; set; }
        public bool isDeleted { get; set; }
    }
    public class GetAllCuponPorcentajeResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public List<CuponPorcentajeModel>? data { get; set; }
    }

    public class GetByIdCuponPorcentajeResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CuponPorcentajeModel? data { get; set; }
    }
}
