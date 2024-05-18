using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
   public class MarcaNegocio
    {
        public int id { get; set; }
        public string descripcion { get; set; }

        public override string ToString()
        {
            return this.descripcion;
        }

        public List<Marca> listar()
        {

            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT  id, Descripcion FROM MARCAS;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())//getLector del objeto "datos"
                {
                    Marca aux = new Marca();
                    aux.id = (int)datos.Lector["id"];
                    aux.descripcion = (String)datos.Lector["Descripcion"];





                    lista.Add(aux);
                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
