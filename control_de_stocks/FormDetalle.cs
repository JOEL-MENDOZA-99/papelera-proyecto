using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
namespace control_de_stocks
{
    public partial class FormDetalle : Form
    {
        Articulo artDetalle;
        public FormDetalle(Articulo ar)
        {
            InitializeComponent();
            artDetalle = ar;

        }

        private void FormDetalle_Load(object sender, EventArgs e)
        {
            label8.Text = artDetalle.codigo;
            lblNombre.Text = artDetalle.nombre;
            lblDescripcion.Text = artDetalle.descripcion;

            lblPrecio.Text = artDetalle.precioxmayor.ToString();
            lblPrecioMenor.Text = artDetalle.precioxmenor.ToString();

            lblCantMayor.Text = artDetalle.cantidadxmayor;
            lblCantMenor.Text = artDetalle.cantidadxmenor;

            lblCategoria.Text = artDetalle.categoria.descripcion;
            lblMarca.Text = artDetalle.marca.descripcion;

          

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
