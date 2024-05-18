using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Deudor
    {
        public int id { get; set; }
       
        [DisplayName("Nombre/Apellido")]
        public string nombreApellido { get; set; }

        [DisplayName("Alias")]
        public string alias { get; set; }

        [DisplayName("Telefono")]
        public int telefono { get; set; }

        [DisplayName("Monto De Deuda")]
        public string montoMostrable { get; set; }
        
        public double monto { get; set; }

        [DisplayName("Fecha")]
        public DateTime fecha { get; set; }
    }
}
