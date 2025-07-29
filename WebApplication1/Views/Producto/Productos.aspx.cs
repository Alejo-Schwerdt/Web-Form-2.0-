using System;
using ProductoModel = WebApplication1.Models.Producto;
using WebApplication1.Controllers;
using System.Collections.Generic;

namespace WebApplication1.Views.Producto
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
            List<ProductoModel> productos = ProductoControlador.ObtenerTodos();
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
                Response.Redirect("~/Views/Producto/AgregarEditarProducto.aspx?id=" + idProducto);
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Producto/AgregarEditarProducto.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Inicio/Index.aspx");
        }
    }
}