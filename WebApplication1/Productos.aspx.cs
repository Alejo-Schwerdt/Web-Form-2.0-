using System;
using Controladores;
using Modelos;
using System.Collections.Generic;

namespace Vistas.Productos
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            List<Modelos.Producto> productos = ProductoControlador.ObtenerTodos();
            GridViewProductos.DataSource = productos;
            GridViewProductos.DataBind();
        }

        protected void GridViewProductos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idProducto = Convert.ToInt32(GridViewProductos.DataKeys[index].Value);

            if (e.CommandName == "Eliminar")
            {
                ProductoControlador.EliminarProducto(idProducto);
                CargarProductos();
            }
            else if (e.CommandName == "Editar")
            {
                // Redirigir a la página de edición con el ID del producto
                Response.Redirect("AgregarEditarProducto.aspx?id=" + idProducto);
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditarProducto.aspx");
        }

    }
}