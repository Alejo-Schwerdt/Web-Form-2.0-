using Controladores;
using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;    

namespace Vistas
{
    public partial class DetalleVentaUnica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idVenta = Convert.ToInt32(Request.QueryString["id"]);
                CargarDetalleVenta(idVenta);
            }
        }

        private void CargarDetalleVenta(int idVenta)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                // Obtener info de la venta
                string queryVenta = "SELECT IdVenta, FechaVenta, TotalVenta FROM Ventas WHERE IdVenta = @IdVenta";
                SqlCommand cmdVenta = new SqlCommand(queryVenta, conn);
                cmdVenta.Parameters.AddWithValue("@IdVenta", idVenta);

                using (SqlDataReader reader = cmdVenta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                            lblInfoVenta.Text = $"Venta ID: {reader["IdVenta"]} - Fecha: {reader["FechaVenta"]} - Total: ${reader["TotalVenta"]}";
                    }
                }

                // Obtener detalle
                string queryDetalle = @"SELECT d.Cantidad, d.PrecioUnitario, p.NombreProducto
                                    FROM DetalleVenta d
                                    JOIN Productos p ON d.IdProducto = p.IdProducto
                                    WHERE d.IdVenta = @IdVenta";

                SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conn);
                cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);

                List<DetalleVentaDTO> detalles = new List<DetalleVentaDTO>();
                using (SqlDataReader reader = cmdDetalle.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleVentaDTO
                        {
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                            Subtotal = Convert.ToInt32(reader["Cantidad"]) * Convert.ToDecimal(reader["PrecioUnitario"])
                        });
                    }
                }

                gvDetalles.DataSource = detalles;
                gvDetalles.DataBind();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DetalleVentas.aspx");
        }
    }
}