using System.Data.SqlClient;
using Modelos;
using System;


namespace Controladores
{
    public static class ConexionBD
    {
        private static string connectionString = "Server=Miserver;Database=TiendaHardware;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}
