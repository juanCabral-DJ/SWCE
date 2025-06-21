using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class Address : Base.EntityBase<int>
    {
        public override int id { get; set; }
        public int id_user { get; set; }
        public string calle {  get; set; }
        public string ciudad {  get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais {  get; set; }
        public bool Es_predeterminada { get; set; }

        public Address(int id, string calle, string ciudad, string estado_provincia, 
        string codigo_postal, string pais, bool es_predeterminada, int id_user) 
        {
            this.id = id;
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
