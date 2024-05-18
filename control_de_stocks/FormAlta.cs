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
    public partial class FormAlta : Form
    {
        private Articulo articulo = null;

        public FormAlta()
        {
            InitializeComponent();
            this.Text = "Registrar Articulo";

        }
        public FormAlta(Articulo ar)
        {
            InitializeComponent();
            articulo = ar;
            this.Text = "Modificar Articulo";

        }



        //FILTROS Y VALIDACION
        private bool soloNumeros(String cadena)
        {
            int contadorPuntos=-1;
            foreach (char caracter in cadena)
            {
                
                    if (caracter == ',')
                    {
                        contadorPuntos++;//modificar la frm alta con esto
                    }
                //if (contadorPuntos >= 1 || (caracter == '.' && !(caracter == ',' || char.IsDigit(caracter))))
                if(!char.IsDigit(caracter)) {
                    if (!(caracter == ',' && contadorPuntos < 1))
                        return false;
                }

              
                
                if(caracter == '.')
                    return false;//tira cartel
                
              
                

            }
            return true;
        }
        private bool validarFiltro()
        {


            if (string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtPrecioXMenor.Text))
            {
                MessageBox.Show("Se solicita cargar los CAMPO PRECIO, no debe estar Vacio");
                return true;
            }
            //ES LA MISMA CONDICION DE ARRIBA PERO PARA LOS CAMPOS CANTIDAD (TRATAR DE SIMPLIFICAR LUEGO)
            if (string.IsNullOrEmpty(txtCantidadXMayor.Text) || string.IsNullOrEmpty(txtCantidadXMenor.Text))
            {
                MessageBox.Show("Se solicita cargar los CAMPO CANTIDAD, no debe estar Vacio");
                return true;
            }

            if (!(soloNumeros(txtPrecio.Text)) || !(soloNumeros(txtPrecioXMenor.Text)))
            {
                MessageBox.Show("Solo Se Aceptan Numeros y/o un COMA (',') Para El CAMPO 'PRECIO' ");
                return true;
            }
            return false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {


                if (articulo == null) {
                   articulo = new Articulo();
                }
                if (validarFiltro())
                    return;

                articulo.nombre = txtNombre.Text;
                articulo.codigo = txtCodigo.Text;//caja de texto(los datos de la caja de texto se lo asigno al objeto pokemon)
                
                articulo.precioxmayor = double.Parse(txtPrecio.Text);
                articulo.precioxmenor = double.Parse(txtPrecioXMenor.Text);

                articulo.cantidadxmayor = txtCantidadXMayor.Text;
                articulo.cantidadxmenor = txtCantidadXMenor.Text;

                articulo.descripcion = txtDescripcion.Text;//caja de texto(los datos de la caja de texto se lo asigno al objeto pokemon)
                articulo.urlImage = txtUrlImagen.Text; //(los datos de la caja de texto se lo asigno al objeto pokemon)
                articulo.marca = (Marca)cbxMarca.SelectedItem;//caja de seleccion de items/desplegables
                articulo.categoria = (Categoria)cbxCategoria.SelectedItem;//caja de seleccion de items/desplegables

                //una vez capturados los datos del formulario
                //se los mando para que lo "agregue" a la bd

                if (articulo.id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else{
                    negocio.agregar(articulo);
                    MessageBox.Show("agregar exitoso");     
                }
                
              
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void FormAlta_Load(object sender, EventArgs e)
        {

            MarcaNegocio negocioM = new MarcaNegocio();
            CategoriaNegocio negecioC = new CategoriaNegocio();

            try
            {
                cbxMarca.DataSource = negocioM.listar();
                cbxMarca.ValueMember = "id";
                cbxMarca.DisplayMember = "descripcion";

                cbxCategoria.DataSource = negecioC.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    //cargarImagen(articulo.urlImage);
                    txtCodigo.Text = articulo.codigo;
                    txtNombre.Text = articulo.nombre;

                    txtPrecio.Text = articulo.precioxmayor.ToString();
                    txtPrecioXMenor.Text = articulo.precioxmenor.ToString();

                    txtCantidadXMayor.Text = articulo.cantidadxmayor;
                    txtCantidadXMenor.Text = articulo.cantidadxmenor;

                    txtDescripcion.Text = articulo.descripcion;
                    txtUrlImagen.Text = articulo.urlImage;
                    

                    cbxMarca.SelectedValue = articulo.marca.id;
                    cbxCategoria.SelectedValue = articulo.categoria.id;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           



        }

    

        
        //EVENTO DE CARGA DE IMAGEN (APENAS SALGA DEL RECUADRO (IMG URL:) Y PUSE ALGO, EL EVENTO SE DISPARA CARGANDO UNA IMAGEN)
       // private void txtUrlImagen_Leave(object sender, EventArgs e)
        //{
         //   cargarImagen(txtUrlImagen.Text);
        //}
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxArticulo.Load("https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
 }

