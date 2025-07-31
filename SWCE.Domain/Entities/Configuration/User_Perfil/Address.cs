 
using System.ComponentModel.DataAnnotations.Schema;
 

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class Address : Base.EntityBase<int>
    {
        public override int id { get; set; }
        [Column("ID_Usuario")]
        public int ID_Usuario { get; set; }
        public string calle {  get; set; }
        public string ciudad {  get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais {  get; set; }
        public bool Es_predeterminada { get; set; } = false;

    }
}
