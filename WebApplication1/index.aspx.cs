using System;
using Controladores;
using Modelos;

namespace Vistas { 
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarVenta.aspx");
        }

        protected void btnDetalleVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalleVentas.aspx");
        }
    }
}