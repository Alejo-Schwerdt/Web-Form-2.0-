using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Controladores
{
    public class VentaControlador
    {
        public static void RegistrarVenta(Venta venta)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open(); // ✅ ¡Esto es obligatorio!

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string insertVenta = @"INSERT INTO Ventas (FechaVenta, TotalVenta)
                                   OUTPUT INSERTED.IdVenta
                                   VALUES (@FechaVenta, @TotalVenta)";
                        SqlCommand cmdVenta = new SqlCommand(insertVenta, conn, trans);
                        cmdVenta.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                        cmdVenta.Parameters.AddWithValue("@TotalVenta", venta.TotalVenta);
                        int idVenta = (int)cmdVenta.ExecuteScalar();

                        foreach (var detalle in venta.Detalles)
                        {
                            string insertDetalle = @"INSERT INTO DetalleVenta
                                        (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                                         VALUES
                                        (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario)";
                            SqlCommand cmdDetalle = new SqlCommand(insertDetalle, conn, trans);
                            cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                            cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.Producto.IdProducto);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                            cmdDetalle.ExecuteNonQuery();

                            // Descontar stock
                            string updateStock = @"UPDATE Productos SET StockProducto = StockProducto - @Cantidad
                                       WHERE IdProducto = @IdProducto";
                            SqlCommand cmdStock = new SqlCommand(updateStock, conn, trans);
                            cmdStock.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            cmdStock.Parameters.AddWithValue("@IdProducto", detalle.Producto.IdProducto);
                            cmdStock.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Error al registrar la venta: " + ex.Message);
                    }
                }
            }
        }
        public static List<Venta> ObtenerVentasConDetalles()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                // Obtener ventas
                string queryVentas = "SELECT IdVenta, FechaVenta, TotalVenta, FechaVenta FROM Ventas";
                SqlCommand cmdVentas = new SqlCommand(queryVentas, conn);

                using (SqlDataReader reader = cmdVentas.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ventas.Add(new Venta
                        {
                            IdVenta = Convert.ToInt32(reader["IdVenta"]),
                            FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                            TotalVenta = Convert.ToDecimal(reader["TotalVenta"]),
                            Detalles = new List<DetalleVenta>()
                        });
                    }
                }

                // Obtener detalles de cada venta
                foreach (var venta in ventas)
                {
                    string queryDetalles = @"
                        SELECT d.IdProducto, d.Cantidad, d.PrecioUnitario, p.NombreProducto
                        FROM DetalleVenta d
                        JOIN Productos p ON d.IdProducto = p.IdProducto
                        WHERE d.IdVenta = @IdVenta";

                    SqlCommand cmdDetalles = new SqlCommand(queryDetalles, conn);
                    cmdDetalles.Parameters.AddWithValue("@IdVenta", venta.IdVenta);

                    using (SqlDataReader reader = cmdDetalles.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            venta.Detalles.Add(new DetalleVenta
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                Producto = new Producto
                                {
                                    NombreProducto = reader["NombreProducto"].ToString()
                                }
                            });
                        }
                    }
                }
            }

            return ventas;
        }
    }
}