using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }
       

        public AccesoDatos()
        {
            
            conexion = new SqlConnection("server=Joel-PC\\SQLEXPRESS; database=db_papelera; integrated security=true");
            comando = new SqlCommand();
        }

       
        public void setearConsulta(String consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

       
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();//linea agregada para que pueda hacer consultas continuas, ya que reseteo las variables que deberian ser unicas en las consultas
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void cerrarConexion()
        {

            if (lector != null)
                lector.Close();
                
            
            conexion.Close();
        }
        public void setearParametro(String nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
    }
}
