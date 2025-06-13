using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public class User
    {
        private int id_user {  get; set; }
        private int id_rol {  get; set; }
        private string name_user { get; set; }
        private string apellido {  get; set; }
        private string email {  get; set; }
        private string password { get; set; }
        private DateTime Fecha_Creacion { get; set; }
        public virtual ICollection<Adress> Addresses { get; set; } = new List<Adress>();
        public virtual ICollection<WishListItem> WishlistItems { get; set; } = new List<WishListItem>();
        public User(int id_user, string name_user, string apellido, 
            string email, string password, DateTime fecha_Creacion, int id_rol) 
        {
            this.id_user = id_user;
            this.id_rol = id_rol;
            this.name_user = name_user;
            this.apellido = apellido;
            this.email = email;
            this.password = password;
            Fecha_Creacion = fecha_Creacion;
        }
    }
}
