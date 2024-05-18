using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DeudorNegocio
    {
        private AccesoDatos datos = new AccesoDatos();


        private Deudor obtenerDatosArticuloDB() // captura los datos del articulo que estan en la BD 
        {
            Deudor aux = new Deudor();

            aux.id = (int)datos.Lector["Id"];
            aux.nombreApellido = (String)datos.Lector["NombreApellido"];
            aux.alias = (String)datos.Lector["Alias"];
            aux.telefono = (int)datos.Lector["Telefono"];

            aux.monto = (double)datos.Lector["MontoDeuda"];
            aux.montoMostrable = aux.monto.ToString("C0", new System.Globalization.CultureInfo("en-US"));

            aux.fecha = (DateTime)datos.Lector["Fecha"];

            return aux;
        }
        public List<Deudor> listar()
        {

            List<Deudor> lista = new List<Deudor>();
            try
            {

                datos.setearConsulta("SELECT d.Id,d.NombreApellido,d.Alias,d.Telefono,d.MontoDeuda,d.Fecha FROM DEUDOR d;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())//getLector del objeto "datos"
                {
                    lista.Add(obtenerDatosArticuloDB());
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




        //----------------------------------------------------------------------BD-------------
        public void agregar(Deudor nuevo)
        {

            try
            {
                datos.setearConsulta("INSERT INTO DEUDOR(NombreApellido,Alias,Telefono,MontoDeuda,Fecha) VALUES(@nom,@alias,@tel,@monto,@fecha)");
                datos.setearParametro("@nom", nuevo.nombreApellido);
                datos.setearParametro("@alias", nuevo.alias);
                datos.setearParametro("@tel", nuevo.telefono);
                datos.setearParametro("@monto", nuevo.monto);
                datos.setearParametro("@fecha", nuevo.fecha);
                
               
                datos.ejecutarAccion();
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

        public void modificar(Deudor ar)
        {

            try
            {
                datos.setearConsulta("update DEUDOR set NombreApellido = @nom,Alias = @ali,Telefono=@tel,MontoDeuda=@monto,Fecha=@fecha where id=" + ar.id + "");//vamos agregar el "tipo" de una forma y la "debilidad" de otra
                datos.setearParametro("@nom", ar.nombreApellido);
                datos.setearParametro("@ali", ar.alias);
                datos.setearParametro("@tel", ar.telefono);
                datos.setearParametro("@monto", ar.monto);
                datos.setearParametro("@fecha", ar.fecha);
                
            
                datos.ejecutarAccion();
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
