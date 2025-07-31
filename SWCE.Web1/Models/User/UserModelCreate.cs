namespace SWCE.Web1.Models.User
{
    public class UserModelCreate
    {
            public int id_rol { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public DateTime fecha_Creacion { get; set; }
        }

     
}
