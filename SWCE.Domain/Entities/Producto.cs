using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        
        public Producto(string nombre, string marca, Categoria categoria, decimal precio, int stock, int stockMinimo)
        {
            Id = Id;
            Nombre = nombre;
            Marca = marca;
            Categoria = categoria;
            Precio = 0;
            Stock = 0;
        }

        public void AjustarStock(int cantidad)
        {
            if (Stock + cantidad < 0)
                throw new ArgumentException("No hay suficiente stock disponible");

            Stock += cantidad;
        }

        public bool TieneStockBajo() => Stock < 50;

        public void ActualizarProducto(string nombre, string marca, decimal precio, Categoria categoria, int stock)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del producto es requerido");

            if (precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero");

            Nombre = nombre;
            Marca = marca;
            Categoria = categoria;
            Precio = precio;
            Stock = stock;
        }
    }
}
