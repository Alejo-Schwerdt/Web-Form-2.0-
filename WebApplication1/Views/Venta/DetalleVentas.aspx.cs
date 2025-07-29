using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Controllers;
using System.Data.SqlClient;
using VentaModel = WebApplication1.Models.Venta;
using DetalleVentaModel = WebApplication1.Models.DetalleVenta;
using ProductoModel = WebApplication1.Models.Producto;

namespace WebApplication1.Views.Venta
{
    public partial class DetalleVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarVentas();
        }

        private void CargarVentas()
        {
            List<VentaModel> ventas = ObtenerVentasConDetalles();
            rptVentas.DataSource = ventas;
            rptVentas.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Inicio/Index.aspx");
        }

        private List<VentaModel> ObtenerVentasConDetalles()
        {
            List<VentaModel> ventas = new List<VentaModel>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                // Obtener ventas
                string queryVentas = "SELECT IdVenta, FechaVenta, TotalVenta FROM Ventas";
                SqlCommand cmdVentas = new SqlCommand(queryVentas, conn);
                using (SqlDataReader reader = cmdVentas.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VentaModel venta = new VentaModel
                        {
                            IdVenta = Convert.ToInt32(reader["IdVenta"]),
                            FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                            TotalVenta = Convert.ToDecimal(reader["TotalVenta"]),
                            Detalles = new List<DetalleVenta>()
                        };
                        ventas.Add(venta);
                    }
                }

                // Obtener detalles
                foreach (var venta in ventas)
                {
                    string queryDetalles = @"
                        SELECT d.IdProducto, d.Cantidad, d.PrecioUnitario, 
                               p.NombreProducto
                        FROM DetalleVenta d
                        JOIN Productos p ON d.IdProducto = p.IdProducto
                        WHERE d.IdVenta = @IdVenta";

                    SqlCommand cmdDetalles = new SqlCommand(queryDetalles, conn);
                    cmdDetalles.Parameters.AddWithValue("@IdVenta", venta.IdVenta);

                    using (SqlDataReader reader = cmdDetalles.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetalleVenta detalle = new DetalleVenta
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                Producto = new ProductoModel
                                {
                                    NombreProducto = reader["NombreProducto"].ToString()
                                }
                            };
                            venta.Detalles.Add(detalle);
                        }
                    }
                }
            }

            return ventas;
        }
    }
}