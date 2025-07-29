using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string TipoProducto { get; set; }
        public decimal ValorProducto { get; set; }
        public int StockProducto { get; set; }
        public string MarcaProducto { get; set; }
        public bool Estado { get; set; }
    }
}
