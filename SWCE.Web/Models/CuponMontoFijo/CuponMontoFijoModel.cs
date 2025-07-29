namespace SWCE.Web.Models.CuponMontoFijo
{
    public class CuponMontoFijoModel
    {
            public decimal monto { get; set; }
            public int id { get; set; }
            public DateTime fechaExpiracion { get; set; }

            public bool isDeleted { get; set; }
    }

    public class GetAllCuponMontoFijoResponse
    {
        public string? message { get; set; }
         public bool isSuccess { get; set; }
        public List<CuponMontoFijoModel>? data { get; set; }
    }

    public class GetByIdCuponMontoFijoResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public CuponMontoFijoModel? data { get; set; }
    }
}
