using System;
using System.Collections.Generic;
using Modelos;

namespace Modelos
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal? TotalVenta { get; set; }

        // Relación: una venta puede tener muchos detalles
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }

    public class ItemVenta
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}