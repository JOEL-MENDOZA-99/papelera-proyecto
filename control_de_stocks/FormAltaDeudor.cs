using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace control_de_stocks
{
    public partial class FormAltaDeudor : Form
    {
        private Deudor deudor = null;

       
        public FormAltaDeudor(Deudor deu)
        {
            InitializeComponent();
            deudor = deu;
            this.Text = "Modificar Deudor";

        }
        public FormAltaDeudor()
        {
            InitializeComponent();
            this.Text = "Registrar Deudor";
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DeudorNegocio negocio = new DeudorNegocio();

            try
            {


                if (deudor == null)
                {
                    deudor = new Deudor();
                }
                if (validarFiltro())
                    return;

                deudor.nombreApellido = txtNombreApe.Text;
                deudor.alias = txtAlias.Text;//caja de texto(los datos de la caja de texto se lo asigno al objeto pokemon)

                deudor.monto = double.Parse(txtMonto.Text);
                deudor.telefono = int.Parse(txtTel.Text);

                deudor.fecha = DateTime.Parse(dtFecha.Text);
               
                //una vez capturados los datos del formulario
                //se los mando para que lo "agregue" a la bd

                if (deudor.id != 0)
                {
                    negocio.modificar(deudor);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(deudor);
                    MessageBox.Show("agregar exitoso");
                }


                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        private bool validarFiltro()
        {
            //ACA HAY QUE MODIFICAR TODO ..AHORA SOLO ES PARA PROBAR

            if (string.IsNullOrEmpty(txtMonto.Text) || string.IsNullOrEmpty(txtMonto.Text))
            {
                MessageBox.Show("Se solicita cargar los CAMPO PRECIO, no debe estar Vacio");
                return true;
            }
            //ES LA MISMA CONDICION DE ARRIBA PERO PARA LOS CAMPOS CANTIDAD (TRATAR DE SIMPLIFICAR LUEGO)
            if (string.IsNullOrEmpty(txtAlias.Text) || string.IsNullOrEmpty(txtNombreApe.Text))
            {
                MessageBox.Show("Se solicita cargar los CAMPO CANTIDAD, no debe estar Vacio");
                return true;
            }

            if (!(soloNumeros(txtMonto.Text)) || !(soloNumeros(txtTel.Text)))
            {
                MessageBox.Show("Solo Se Aceptan Numeros y/o un COMA (',') Para El CAMPO 'PRECIO' ");
                return true;
            }
            return false;
        }
        private bool soloNumeros(String cadena)
        {
            int contadorPuntos = -1;
            foreach (char caracter in cadena)
            {

                if (caracter == ',')
                {
                    contadorPuntos++;//modificar la frm alta con esto
                }
                if (contadorPuntos >= 1 || (caracter == '.' && !(caracter == ',' || char.IsDigit(caracter))))
                    return false;//tira cartel

            }
            return true;
        }

        private void FormAltaDeudor_Load(object sender, EventArgs e)
        {
           

            try
            {

                if (deudor != null)
                {
                    //cargarImagen(articulo.urlImage);
                    txtNombreApe.Text = deudor.nombreApellido;
                    txtAlias.Text = deudor.alias;

                    txtTel.Text = deudor.telefono.ToString();
                    txtMonto.Text = deudor.monto.ToString();

                     dtFecha.Value = deudor.fecha;

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
    }
}
