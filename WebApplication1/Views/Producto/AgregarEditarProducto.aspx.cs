using System;
using ProductoModel = WebApplication1.Models.Producto;
using WebApplication1.Controllers;


namespace WebApplication1.Views.Producto

{
    public partial class AgregarEditarProducto : System.Web.UI.Page
    {
        private int? IdProducto
        {
            get
            {
                if (int.TryParse(Request.QueryString["id"], out int id))
                    return id;
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && IdProducto.HasValue)
            {
                CargarProducto(IdProducto.Value);
                lblTitulo.Text = "Editar Producto";
            }
        }

        private void CargarProducto(int id)
        {
            ProductoModel producto = ProductoControlador.ObtenerTodos().Find(p => p.IdProducto == id);
            if (producto != null)
            {
                txtNombre.Text = producto.NombreProducto;
                txtDescripcion.Text = producto.DescripcionProducto;
                txtTipo.Text = producto.TipoProducto;
                txtValor.Text = producto.ValorProducto.ToString();
                txtStock.Text = producto.StockProducto.ToString();
                txtMarca.Text = producto.MarcaProducto;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoModel p = new ProductoModel
                {
                    NombreProducto = txtNombre.Text,
                    DescripcionProducto = txtDescripcion.Text,
                    TipoProducto = txtTipo.Text,
                    ValorProducto = Convert.ToDecimal(txtValor.Text),
                    StockProducto = Convert.ToInt32(txtStock.Text),
                    MarcaProducto = txtMarca.Text
                };

                if (IdProducto.HasValue)
                {
                    p.IdProducto = IdProducto.Value;
                    ProductoControlador.ActualizarProducto(p);
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Producto actualizado correctamente.";
                }
                else
                {
                    ProductoControlador.AgregarProducto(p);
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Producto agregado correctamente.";
                }

                Response.Redirect("~/Views/Producto/Productos.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Producto/Productos.aspx");
        }
    }
}