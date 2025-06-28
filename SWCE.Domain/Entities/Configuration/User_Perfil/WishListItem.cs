 
using System.ComponentModel.DataAnnotations.Schema;
 

namespace SWCE.Domain.Entities.Configuration.User_Perfil
{
    public sealed class WishListItem : Base.EntityBase<int>
    {
        [Column("Id_Lista_Deseos")]
        public override int id { get; set; }
        public int id_producto { get; set; }
        [Column("Id_Usuario")]
        public int Id_Usuario { get; set; }

     }   

}
