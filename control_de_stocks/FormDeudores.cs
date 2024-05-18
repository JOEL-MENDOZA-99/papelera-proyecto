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
using negocio;

namespace control_de_stocks
{
    public partial class FormDeudores : Form
    {
        public FormDeudores()
        {
            InitializeComponent();
        }
        private List<Deudor> deudores;
        private DeudorNegocio negocioDeu = new DeudorNegocio();

       

        private void cargarGrillaDeDeudores()
        {

            try
            {

                deudores = negocioDeu.listar();
                dgvDeudores.DataSource = deudores;


                //MessageBox.Show(dgvArticulos.Rows[0].Cells[2].Value.ToString()); OBTENER UNA CELDA DEL DATAGRIDVIEW

                //double i = 123.25;
                //MessageBox.Show(i.ToString("C",new System.Globalization.CultureInfo("en-US")));
                ocultarColumnas();





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
           
            dgvDeudores.Columns["id"].Visible = false;
            dgvDeudores.Columns["monto"].Visible = false;

        }

        private void FormDeudores_Load(object sender, EventArgs e)
        {
            cargarGrillaDeDeudores();
        }

        private void btnDeudor_Click(object sender, EventArgs e)
        {
            FormAltaDeudor f = new FormAltaDeudor();
            f.ShowDialog();

            cargarGrillaDeDeudores();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.CurrentRow != null)
            {
                Deudor seleccionado = (Deudor)dgvDeudores.CurrentRow.DataBoundItem;
                FormAltaDeudor fm = new FormAltaDeudor(seleccionado);
                fm.ShowDialog();
                cargarGrillaDeDeudores();
            }
        }
    }
}
