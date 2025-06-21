using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class User : Base.EntityBase<int>
    {
        public override int id {  get; set; }
        public int id_rol {  get; set; }
        public string name_user { get; set; }
        public string apellido {  get; set; }
        public string email {  get; set; }
        public string password { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        public User(int id, string name_user, string apellido, 
            string email, string password, DateTime fecha_Creacion, int id_rol) 
        {
            this.id = id;
            this.id_rol = id_rol;
            this.name_user = name_user;
            this.apellido = apellido;
            this.email = email;
            this.password = password;
            Fecha_Creacion = fecha_Creacion;
        }
    }
}
