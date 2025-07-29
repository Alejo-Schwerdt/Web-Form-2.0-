using System;
using WebApplication1.Models;
using WebApplication1.Controllers;

namespace WebApplication1.Views.Inicio
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Producto/Productos.aspx");
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Venta/RegistrarVenta.aspx");
        }

        protected void btnDetalleVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Venta/DetalleVentas.aspx");
        }
    }
}