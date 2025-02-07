using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dominio
{
    public class Articulo : IComparable<Articulo>// IComparable<Articulo> : lo use para que pueda ordenar los objetos de este tipo
                                                 // con la funcion CompareTo y luego en el main aplicar solo un sort() y listo
    {
        public int id { get; set; }

        public string codigo { get; set; }

        [DisplayName("Nombre")]
        public string nombre { get; set; }
        
       
        public double precioxmenor { get; set; }

        [DisplayName("PrecioXUnidad")]
        public string precioMenor { get; set; }


        public double precioxcaja { get; set; }

        [DisplayName("Precio X Caja/Bolson")]
        public string precioCaja { get; set; }


        public double precioxmayor { get; set; }
        
        [DisplayName("PrecioXMayor")]
        public string precioMayor { get; set; }


       




        [DisplayName("CantidadXMenor")]
        public string cantidadxmenor { get; set; }

        [DisplayName("CantidadXMayor")]
        public string cantidadxmayor { get; set; }
        public string urlImage { get; set; }

        [DisplayName("Categoria")]
        public Categoria categoria { get; set; }

        [DisplayName("Marca")]
        public Marca marca { get; set; }


        [DisplayName("Descripcion")]
        public string descripcion { get; set; }

        public int CompareTo(Articulo other)
        {
            if (other == null) return 1;

            // Comparar primero por Categoría
            int categoriaComparison = string.Compare(this.categoria.descripcion, other.categoria.descripcion, StringComparison.OrdinalIgnoreCase);
            if (categoriaComparison != 0) return categoriaComparison;

            // Si las Categorías son iguales, comparar por Marca
            int nombreComparison = string.Compare(this.marca.descripcion ?? string.Empty, other.marca.descripcion ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            if (nombreComparison != 0) return nombreComparison;

            //si las Marcas son iguales entonces por Nombre
            return string.Compare(this.nombre ?? string.Empty, other.nombre ?? string.Empty, StringComparison.OrdinalIgnoreCase);

            

            throw new NotImplementedException();
        }
    }
}
