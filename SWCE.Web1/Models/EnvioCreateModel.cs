namespace SWCE.Web.Models
{
    public class EnvioCreateModel
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public DateTime fechaPedido { get; set; }
        public string? estado { get; set; }
        public decimal costo { get; set; }
        public string? tipoEnvio { get; set; }
    }

    public class EnvioCreateModelResponse
    {
        public string? message { get; set; }
        public bool isSuccess { get; set; }
        public object? data { get; set; }
    }
}
