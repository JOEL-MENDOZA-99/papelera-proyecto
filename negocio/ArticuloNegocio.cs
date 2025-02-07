using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        
        private Articulo obtenerDatosArticuloDB() // captura los datos del articulo que estan en la BD 
        {
            Articulo aux = new Articulo();

            aux.id = (int)datos.Lector["Id"];
            aux.codigo = (String)datos.Lector["Codigo"];
            aux.nombre = (String)datos.Lector["Nombre"];
            aux.descripcion = (String)datos.Lector["Descripcion"];

            if (!(datos.Lector["ImagenUrl"] is DBNull))//manejo de la lectura de un NULL de una bd
              aux.urlImage = (String)datos.Lector["ImagenUrl"];

            aux.categoria = new Categoria();
            aux.categoria.id = (int)datos.Lector["IdCategoria"];
            aux.categoria.descripcion = (String)datos.Lector["Categoria"];

            aux.marca = new Marca();
            aux.marca.id = (int)datos.Lector["IdMarca"];
            aux.marca.descripcion = (String)datos.Lector["Marca"];

            aux.precioxmenor = (double)datos.Lector["PrecioXMenor"];
           
           
            aux.cantidadxmenor = (String)datos.Lector["CantidadXMenor"];

           //la cantidad lo casteamos como double para setearle a precioxmayor su valor
            aux.cantidadxmayor = (String)datos.Lector["CantidadXMayor"];
            double numeroCantidadMayor = Convert.ToDouble(aux.cantidadxmayor);
            aux.precioxcaja = (double)datos.Lector["PrecioXCaja"];

            if (numeroCantidadMayor != 0)
                aux.precioxmayor = aux.precioxcaja / numeroCantidadMayor;
            else
                aux.precioxmayor = 0;


            //transformar numeros en tipo moneda y mostrarlo en el form
            aux.precioMenor = aux.precioxmenor.ToString("C0", new System.Globalization.CultureInfo("en-US"));
            aux.precioMayor = aux.precioxmayor.ToString("C0", new System.Globalization.CultureInfo("en-US"));
            aux.precioCaja = aux.precioxcaja.ToString("C0", new System.Globalization.CultureInfo("en-US"));

            return aux;
        }
        public List<Articulo> listar()
        {

            List<Articulo> lista = new List<Articulo>();
            try
            {

                datos.setearConsulta("SELECT a.Id,a.IdMarca,a.IdCategoria,a.Codigo,a.Nombre,a.Descripcion,a.ImagenUrl ,c.Descripcion Categoria,m.Descripcion Marca,a.PrecioXMenor,a.PrecioXMayor,a.CantidadXMenor,a.CantidadXMayor,a.PrecioXCaja FROM ARTICULOS a INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id INNER JOIN MARCAS m ON a.IdMarca = m.Id;");
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

        public void agregar(Articulo nuevo)
        {
          
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo,Descripcion,Nombre,IdCategoria,IdMarca,ImagenUrl,PrecioXMayor,PrecioXMenor,CantidadXMenor,CantidadXMayor,PrecioXCaja) VALUES(@cod,@des,@nom,@idC,@idM,@url,@precio,@precioMenor,@cantMenor,@cantMayor,@precioCaja)");
                datos.setearParametro("@cod", nuevo.codigo);
                datos.setearParametro("@des", nuevo.descripcion);
                datos.setearParametro("@nom", nuevo.nombre);
                datos.setearParametro("@idC", nuevo.categoria.id);
                datos.setearParametro("@idM", nuevo.marca.id);
                datos.setearParametro("@url", nuevo.urlImage);
                datos.setearParametro("@precio", nuevo.precioxmayor);
                datos.setearParametro("@precioMenor", nuevo.precioxmenor);
                datos.setearParametro("@precioCaja", nuevo.precioxcaja);
                datos.setearParametro("@cantMenor", nuevo.cantidadxmenor);
                datos.setearParametro("@cantMayor", nuevo.cantidadxmayor);
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

        public void modificar(Articulo ar) {
           
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @cod,Descripcion = @des,Nombre=@nom,IdCategoria=@idC,IdMarca=@idM,ImagenUrl=@url,PrecioXMayor=@precio,PrecioXMenor=@precioMenor,CantidadXMayor=@cantMayor,CantidadXMenor = @cantMenor, PrecioXCaja=@precioCaja where id=" + ar.id + "");//vamos agregar el "tipo" de una forma y la "debilidad" de otra
                datos.setearParametro("@cod", ar.codigo);
                datos.setearParametro("@des", ar.descripcion);
                datos.setearParametro("@nom", ar.nombre);
                datos.setearParametro("@idC", ar.categoria.id);
                datos.setearParametro("@idM", ar.marca.id);
                datos.setearParametro("@url", ar.urlImage);
                datos.setearParametro("@precio", ar.precioxmayor);
                datos.setearParametro("@precioMenor", ar.precioxmenor);
                datos.setearParametro("@precioCaja", ar.precioxcaja);
                datos.setearParametro("@cantMenor", ar.cantidadxmenor);
                datos.setearParametro("@cantMayor", ar.cantidadxmayor);
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
        
        public void eliminar(int id)
        {
            
            try
            {
                datos.setearConsulta("delete from ARTICULOS where id=" + id + "");
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
        public List<Articulo> buscarRapido(string filtro)
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string consulta = "SELECT a.Id,a.IdMarca,a.IdCategoria,a.Codigo,a.Nombre,a.Descripcion,a.ImagenUrl ,c.Descripcion Categoria,m.Descripcion Marca,a.PrecioXMenor,a.PrecioXMayor,a.CantidadXMenor,a.CantidadXMayor, a.PrecioXCaja FROM ARTICULOS a INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id INNER JOIN MARCAS m ON a.IdMarca = m.Id where ";

                consulta += "a.Nombre like '%" + filtro + "%'";

                  

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
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
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            
            try
            {
                string consulta = "SELECT a.Id,a.IdMarca,a.IdCategoria,a.Codigo,a.Nombre,a.Descripcion,a.ImagenUrl ,c.Descripcion Categoria, m.Descripcion Marca, a.PrecioXMayor,a.PrecioXMenor,a.CantidadXMayor,a.CantidadXMenor FROM ARTICULOS a INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id INNER JOIN MARCAS m ON a.IdMarca = m.Id where ";

                switch (campo)
                {
                    case "PrecioXMayor":
                        switch (criterio) {
                            case "Mayor a":
                                consulta += "a.PrecioXMayor > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "a.PrecioXMayor < " + filtro;
                                break;
                            default:
                                consulta += "a.PrecioXMayor = " + filtro;
                                break;
                            
                        }
                        break;
                   
                    case "Nombre":
                        switch (criterio) {
                            case "Comienza con":
                                consulta += "a.Nombre like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "a.Nombre like '%" + filtro +"'";
                                break;
                            default:
                                consulta += "a.Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    default:
                        switch (criterio) {
                            case "Comienza con":
                                consulta += "m.Descripcion like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "m.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "m.Descripcion like '%" + filtro + "%'";
                                break;

                        }
                        break;
                
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
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
    }
}
