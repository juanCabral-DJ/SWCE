using SWCE.Web.Models.Base;
using SWCE.Web.Models.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Web.Models.Carrito
{
    public class CarritoModel : BaseModel
    {
        public decimal total { get; set; }
        public DateTime createAt { get; set; }
        public bool isDeleted { get; set; }
        public ProductoModel[] productos { get; set; }
        public int iD_Usuario { get; set; }
    }

}
