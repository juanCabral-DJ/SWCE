 


namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class User : Base.EntityBase<int>
    {
        public override int id { get; set; }
        public int id_rol { get; set; }
        public string Nombre { get; set; }
        public string apellido { get; set; }
        public string email {  get; set; }
        public string password { get; set; }
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

    }
}
