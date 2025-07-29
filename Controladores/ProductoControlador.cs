using Modelos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;


namespace Controladores
{
    public class ProductoControlador
    {
        public static List<Producto> ObtenerTodos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Productos WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto p = new Producto
                    {
                        IdProducto = (int)reader["IdProducto"],
                        NombreProducto = reader["NombreProducto"].ToString(),
                        DescripcionProducto = reader["DescripcionProducto"].ToString(),
                        TipoProducto = reader["TipoProducto"].ToString(),
                        ValorProducto = (decimal)reader["ValorProducto"],
                        StockProducto = (int)reader["StockProducto"],
                        MarcaProducto = reader["MarcaProducto"].ToString(),
                        Estado = (bool)reader["Estado"]
                    };
                    lista.Add(p);
                }
            }

            return lista;
        }
        public static void AgregarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection conn = ConexionBD.ObtenerConexion())
                {
                    string query = @"INSERT INTO Productos 
                (NombreProducto, DescripcionProducto, TipoProducto, ValorProducto, StockProducto, MarcaProducto) 
                VALUES 
                (@NombreProducto, @DescripcionProducto, @TipoProducto, @ValorProducto, @StockProducto, @MarcaProducto)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                    cmd.Parameters.AddWithValue("@DescripcionProducto", producto.DescripcionProducto);
                    cmd.Parameters.AddWithValue("@TipoProducto", producto.TipoProducto);
                    cmd.Parameters.AddWithValue("@ValorProducto", producto.ValorProducto);
                    cmd.Parameters.AddWithValue("@StockProducto", producto.StockProducto);
                    cmd.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el producto: " + ex.Message);
            }
        }
        public static void EliminarProducto(int id)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE Productos SET Estado = 0 WHERE IdProducto = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public static void ActualizarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"UPDATE Productos SET 
                NombreProducto = @NombreProducto, 
                DescripcionProducto = @DescripcionProducto, 
                TipoProducto = @TipoProducto, 
                ValorProducto = @ValorProducto, 
                StockProducto = @StockProducto, 
                MarcaProducto = @MarcaProducto 
                WHERE IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                    cmd.Parameters.AddWithValue("@DescripcionProducto", producto.DescripcionProducto);
                    cmd.Parameters.AddWithValue("@TipoProducto", producto.TipoProducto);
                    cmd.Parameters.AddWithValue("@ValorProducto", producto.ValorProducto);
                    cmd.Parameters.AddWithValue("@StockProducto", producto.StockProducto);
                    cmd.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto);
                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message);
            }
        }
    }
}