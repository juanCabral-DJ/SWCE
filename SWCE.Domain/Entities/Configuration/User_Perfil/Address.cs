using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public class Address 
    {
     private int id_direccion {  get; set; }
     private int id_user { get; set; }
     private string calle {  get; set; }
     private string ciudad {  get; set; }
     private string estado_provincia { get; set; }
     private string codigo_postal { get; set; }
     private string pais {  get; set; }
     private string Es_predeterminada { get; set; }
     public virtual User User { get; set; }

        public Address(int id_direccion, string calle, string ciudad, string estado_provincia, 
        string codigo_postal, string pais, string es_predeterminada, int id_user) 
        {
            this.id_direccion = id_direccion;
            this.id_user = id_user;
            this.calle = calle;
            this.ciudad = ciudad;
            this.estado_provincia = estado_provincia;
            this.codigo_postal = codigo_postal;
            this.pais = pais;
            Es_predeterminada = es_predeterminada;
        }
    }
}
