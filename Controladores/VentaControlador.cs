using Modelos;
using System;
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
    }
}