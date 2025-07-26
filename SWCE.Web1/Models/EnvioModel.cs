namespace SWCE.Web.Models
{
    public class EnvioModel
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public DateTime fechaPedido { get; set; }
        public string? estado { get; set; }
        public decimal costo { get; set; }
        public string? tipoEnvio { get; set; }
    }
    public class GetAllEnvioModelResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public List<EnvioModel>? data { get; set; }
    }

    public class GetEnvioByIdModelResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public EnvioModel data { get; set; }
    }


}
