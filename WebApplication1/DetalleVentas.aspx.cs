using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Controladores;
using Modelos;

namespace Vistas
{
    public partial class DetalleVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Valor por defecto de orden
                ddlOrdenar.SelectedValue = "FechaDesc";
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            List<Modelos.Venta> ventas = ObtenerVentasConDetalles();

            // Ordenamiento según selección
            switch (ddlOrdenar.SelectedValue)
            {
                case "FechaAsc":
                    ventas = ventas.OrderBy(v => v.FechaVenta).ToList();
                    break;
                case "FechaDesc":
                    ventas = ventas.OrderByDescending(v => v.FechaVenta).ToList();
                    break;
                case "TotalAsc":
                    ventas = ventas.OrderBy(v => v.TotalVenta ?? 0m).ToList();
                    break;
                case "TotalDesc":
                    ventas = ventas.OrderByDescending(v => v.TotalVenta ?? 0m).ToList();
                    break;
            }

            rptVentas.DataSource = ventas;
            rptVentas.DataBind();
        }

        protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVentas();
        }

        // Vincula el Repeater hijo (detalles) por cada venta
        protected void rptVentas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var venta = e.Item.DataItem as Modelos.Venta;
            var rptDetalles = e.Item.FindControl("rptDetalles") as Repeater;
            if (venta != null && rptDetalles != null)
            {
                rptDetalles.DataSource = venta.Detalles;
                rptDetalles.DataBind();
            }
        }

        // Maneja el botón Ver Detalle
        protected void rptVentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);
                // Redirige a tu página única de detalle pasando id
                Response.Redirect("~/DetalleVentaUnica.aspx?id=" + idVenta);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Ajustá la ruta según dónde esté tu Index.aspx
            Response.Redirect("~/Index.aspx");
        }

        // --- tu método original para leer ventas desde DB. Mantenerlo o usar el existente ---
        private List<Modelos.Venta> ObtenerVentasConDetalles()
        {
            // Si ya tienes este método en tu code-behind original, úsalo.
            // Aquí asumimos que existe y funciona (tal como lo tenías antes).
            // Si no, pega aquí tu implementación previa (que consultaba Ventas y DetalleVenta).
            return VentaControlador.ObtenerVentasConDetalles(); // si lo tienes en el controlador
        }
    }
}