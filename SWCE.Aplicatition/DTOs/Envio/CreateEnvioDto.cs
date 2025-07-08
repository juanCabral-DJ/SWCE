namespace SWCE.Aplication.DTOs.Envio
{
    public record class CreateEnvioDto
    {
        public int UsuarioId { get; set; }
        public DateTime FechaPedido { get; set; }
        public string? Estado { get; set; }
        public decimal Costo { get; set; }
        public string? TipoEnvio { get; set; }

    }
}
