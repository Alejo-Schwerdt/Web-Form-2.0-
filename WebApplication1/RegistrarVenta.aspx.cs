using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Controladores;
using Modelos;


namespace Vistas.Venta
{
    public partial class RegistrarVenta : System.Web.UI.Page
    {
        private List<Modelos.DetalleVenta> Carrito
        {
            get
            {
                if (Session["Carrito"] == null)
                    Session["Carrito"] = new List<Modelos.DetalleVenta>();
                return (List<Modelos.DetalleVenta>)Session["Carrito"];
            }
            set
            {
                Session["Carrito"] = value;
            }
        }
        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStockDisponible();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                MostrarCarrito();
                ActualizarStockDisponible();
            }
        }
        private void ActualizarStockDisponible()
        {
            int idProducto;
            if (int.TryParse(ddlProductos.SelectedValue, out idProducto))
            {
                var producto = ProductoControlador.ObtenerTodos().FirstOrDefault(p => p.IdProducto == idProducto);
                if (producto != null)
                {
                    lblStockDisponible.Text = $"Stock disponible: {producto.StockProducto}";
                }
                else
                {
                    lblStockDisponible.Text = "";
                }
            }
            else
            {
                lblStockDisponible.Text = "";
            }
        }
        private void CargarProductos()
        {
            ddlProductos.DataSource = ProductoControlador.ObtenerTodos();
            ddlProductos.DataTextField = "NombreProducto";
            ddlProductos.DataValueField = "IdProducto";
            ddlProductos.DataBind();
        }

        private void MostrarCarrito()
        {
            var lista = Carrito.Select(d => new
            {
                d.Producto.NombreProducto,
                d.Cantidad,
                d.PrecioUnitario,
                Subtotal = d.Cantidad * d.PrecioUnitario
            }).ToList();

            gvCarrito.DataSource = lista;
            gvCarrito.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int idProducto = int.Parse(ddlProductos.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            Modelos.Producto producto = ProductoControlador.ObtenerTodos().FirstOrDefault(p => p.IdProducto == idProducto);
            if (producto == null || cantidad <= 0 || cantidad > producto.StockProducto)
            {
                lblMensaje.Text = "❌ Cantidad inválida o sin stock suficiente.";
                return;
            }

            Carrito.Add(new Modelos.DetalleVenta
            {
                Producto = producto,
                IdProducto = producto.IdProducto,
                Cantidad = cantidad,
                PrecioUnitario = producto.ValorProducto
            });

            lblMensaje.Text = "";
            MostrarCarrito();

        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            if (Carrito.Count == 0)
            {
                lblMensaje.Text = "❌ Debe agregar al menos un producto.";
                return;
            }

            Modelos.Venta venta = new Modelos.Venta
            {
                FechaVenta = DateTime.Now,
                TotalVenta = Carrito.Sum(d => d.Cantidad * d.PrecioUnitario),
                Detalles = Carrito
            };

            try
            {
                VentaControlador.RegistrarVenta(venta);
                Carrito = new List<Modelos.DetalleVenta>();
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "✅ Venta registrada correctamente.";
                MostrarCarrito();
                ActualizarStockDisponible();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Carrito = new List<Modelos.DetalleVenta>();
            Response.Redirect("Index.aspx");
        }
    }
}