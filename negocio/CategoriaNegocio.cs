using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public int id { get; set; }
        public string descripcion { get; set; }

        public override string ToString()
        {
            return this.descripcion;
        }

        public List<Categoria> listar()
        {

            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT  id, Descripcion FROM CATEGORIAS;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())//getLector del objeto "datos"
                {
                    Categoria aux = new Categoria();
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
