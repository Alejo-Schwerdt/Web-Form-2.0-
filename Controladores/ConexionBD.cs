using System.Data.SqlClient;
using Modelos;
using System;


namespace Controladores
{
    public static class ConexionBD
    {
        private static string connectionString = "Server=DESKTOP-EIF8TVE\\SQLEXPRESS;Database=TiendaHardware;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}
