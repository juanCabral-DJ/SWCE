using SWCE.Domain.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Entities
{
    public sealed class Producto : EntityBase<int>
    {
        public override int id { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }

        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public Producto() { }

        public Producto(int id, string nombre, string marca, int idCategoria, decimal precio, int stock)
        {
            this.id = id;
            Nombre = nombre;
            Marca = marca;
            IdCategoria = idCategoria;
            Precio = precio;
            Stock = stock;
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
