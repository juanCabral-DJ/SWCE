using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Web.Models.Carrito
{
    public class CarritoModel
    {
        public int id { get; set; }
        public decimal total { get; set; }
        public DateTime createAt { get; set; }
        public bool isDeleted { get; set; }
        public Producto[] productos { get; set; }
        public int iD_Usuario { get; set; }
    }

    public class GetCarritoResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public CarritoModel data { get; set; }
    }

    public class GetAllCarritoResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public List<CarritoModel> data { get; set; }
    }

    public class Producto
    {
        public int id { get; set; }
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subTotal { get; set; }
        public int carritoId { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
    }

}
