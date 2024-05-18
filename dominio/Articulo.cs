using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dominio
{
    public class Articulo
    {
        public int id { get; set; }

        public string codigo { get; set; }

        [DisplayName("Nombre")]
        public string nombre { get; set; }
        
       
        public double precioxmenor { get; set; }

        [DisplayName("PrecioXUnidad")]
        public string precioMenor { get; set; }


        
        public double precioxmayor { get; set; }
        
        [DisplayName("PrecioXBolson")]
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



    }
}
