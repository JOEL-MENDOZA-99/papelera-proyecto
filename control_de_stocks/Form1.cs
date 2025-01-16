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
//HOLA PRUEBAAAAAAA PARA SUBIR GIT

//librerias para la impresion de pdf
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace control_de_stocks
{
    public partial class frmGestor : Form
    {
        //Estas variables van a hacer globales dentro de esta clase para todos los metodos y eventos
        private List<Articulo> articulos;
        private ArticuloNegocio negocioArt = new ArticuloNegocio();
       
        //--------------------------------------------------------------------------
        
        // Estas variables van a hacer utilizadas en 2 eventos (juego de mas de 1 evento)
        private bool flag_cell_edited;
        private int numeroFila;
        private int numeroColumna;
        private DataGridViewCell celdaActual; //Esto es la celda en la que estoy "parado"
                                              //La cual puedo obtener su contenido(con .Value.ToString) o establecerlo (Con Value))
                                              //si bien es un objeto
                                              //el tipo de dato que le puedo setear va a depender del tipo de dato que es la celda 
                                              //ejemplo: si es de tipo entero --> solo numeros 
        public frmGestor()
        {
            InitializeComponent();
            
        }

        //SUBMETODOS DE LOS METODOS DE EVENTO Y CONTROLADORES 
        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por Favor, Seleccione el CAMPO para filtrar");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por Favor, Seleccione el CRITERIO para filtrar");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "PrecioXMayor")
            {

                if (string.IsNullOrEmpty(txtFiltro.Text))
                {
                    MessageBox.Show("Se solicita cargar el filtro, no pude estar 'VACIO'");
                    return true;
                }

                if (!(soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Solo Se Aceptan NUMEROS y/o un PUNTO '.' ,Para El CAMPO 'PRECIO' ");
                    return true;
                }
            }

            return false;
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pcxArticulo.Load(imagen);

            }
            catch (Exception ex)
            {
                pcxArticulo.Load("https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg");
            }

        }

        //REPITO 2 LISTAS IGUALES CORREGIR
        AutoCompleteStringCollection AutoCompleteVenta = new AutoCompleteStringCollection();
        List<string> listaSugerencias = new List<string>();
        private void cargarAutocompletables() {
           

            foreach (Articulo articulo in articulos)
            {
                AutoCompleteVenta.Add(articulo.nombre);
                listaSugerencias.Add(articulo.nombre);
            }

            txtNombreVenta.AutoCompleteCustomSource = AutoCompleteVenta;
            
           
            
            txtNombreVenta.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNombreVenta.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void cargarGrillaDeArticulos()
        {

            try
            {
                articulos = negocioArt.listar();
                dgvArticulos.DataSource = articulos;
                //MessageBox.Show(dgvArticulos.Rows[0].Cells[2].Value.ToString()); OBTENER UNA CELDA DEL DATAGRIDVIEW
                //double i = 123.25;
                //MessageBox.Show(i.ToString("C",new System.Globalization.CultureInfo("en-US")));
                ocultarYRestringirColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarYRestringirColumnas()
        {
            //OCULTO COLUMNAS
            dgvArticulos.Columns["urlImage"].Visible = false;
            dgvArticulos.Columns["codigo"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["precioxmenor"].Visible = false;
            dgvArticulos.Columns["precioxmayor"].Visible = false;

            //RESTRINGO COLUMNAS PARA NO EDITARLAS
            dgvArticulos.Columns["nombre"].ReadOnly = true;
            dgvArticulos.Columns["categoria"].ReadOnly = true;
            dgvArticulos.Columns["marca"].ReadOnly = true;
           
        }

        private bool soloNumeros(String cadena)
        {
            int contadorPuntos = -1;

            foreach (char caracter in cadena)
            {
                if (caracter == '.')
                    contadorPuntos++;//modificar la frm alta con esto
                //CONTROL DE LOS PUNTOS,COMAS Y LETRAS
                if (cadena.Substring(0, 1) == "." || (contadorPuntos >= 1 || (caracter == ',' && !(caracter == '.' || char.IsNumber(caracter)) || char.IsLetter(caracter))))
                    return false;

            }
            return true;
        }
        private double pesosANumeros(String cadena)
        {
            string numeroAConvertir = "";

            foreach (char caracter in cadena)
            {
                if (!(caracter == ',' || caracter == '$'))
                    numeroAConvertir = numeroAConvertir + caracter;
            }
            return double.Parse(numeroAConvertir);
        }
        private void modificarCeldaDePrecios(Articulo a, double nuemero, int columna)
        {
            if (columna == 4)
            {
                a.precioxmenor = nuemero;
            }
            else
            {
                a.precioxmayor = nuemero;
            }
        }
//--------------------------------------------------------------------------------------------------------------

        // METODOS DE LOS CONTROLADORES Y EVENTOS

        private void frmGestor_Load(object sender, EventArgs e)
        {

            cargarGrillaDeArticulos();
            cargarAutocompletables();
            //AGREGAR LOS ITEMS DE LOS DESPLEGABLES
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Marcas");
            cboCampo.Items.Add("PrecioXMayor");
            
            //CENTRAR COLUMNAS ESPECIFICAS
            dgvArticulos.Columns["precioMenor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvArticulos.Columns["precioMayor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvArticulos.Columns["cantidadxmenor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvArticulos.Columns["cantidadxmayor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //AGREGADO DE COLUMNAS PARA EL DATAGRIDVIEW VENTAS
            dgvArticulosVenta.Columns.Add("cantidadVenta","Cantidad");
            dgvArticulosVenta.Columns.Add("nombreVenta","Nombre Producto");
            dgvArticulosVenta.Columns.Add("importe","Importe");
            //ESTAS COLUMNAS SE PUEDEN ACTUALIZAR MOVIENDOLOS EN UN CLASE A PARTE (CLASE : "ITEM")
            //PORQUE SI QUIERO HACER REPORTES DE VENTAS POR MES (por ejemplo)
          
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAlta f = new FormAlta();
            f.ShowDialog();
            
            cargarGrillaDeArticulos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null) {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                FormAlta fm = new FormAlta(seleccionado);
                fm.ShowDialog();
                cargarGrillaDeArticulos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           try
            {
                if (dgvArticulos.CurrentRow != null)
                {
                    DialogResult respuesta = MessageBox.Show("Quieres eliminarlo?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                    if (respuesta == DialogResult.Yes)
                    {
                        Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                        negocioArt.eliminar(seleccionado.id);
                        cargarGrillaDeArticulos();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {

            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
               
                FormDetalle fd = new FormDetalle(seleccionado);
                fd.ShowDialog();
            }
        }
     


        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();

            if (opcion == "PrecioXMayor")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
      
      
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarFiltro())
                    return;
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;

                dgvArticulos.DataSource = negocioArt.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "") {
                dgvArticulos.DataSource = articulos;
                ocultarYRestringirColumnas();
                
            }
        }

        private void salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBucadorRapido_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "")
            {
                dgvArticulos.DataSource = articulos;
                ocultarYRestringirColumnas();

            }
            try
            {
              
                string filtro = txtBucadorRapido.Text;

                dgvArticulos.DataSource = negocioArt.buscarRapido(filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }


        

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
           
         //ESTE METODO HACE QUE EL FOCO "AZUL" (que colorea la celda seleccionada) se mantenga en el mismo lugar luego EDITAR y apretar ENTER 

            /*  if (dgvArticulos.CurrentRow != null)
              {
                  Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                  // cargarImagen(seleccionado.urlImage);
              }*/
            if (flag_cell_edited)
            {
                dgvArticulos.CurrentCell = celdaActual;
         
                flag_cell_edited = false;
            }

        }
        
        //EVENTO QUE SE DISPARA LUEGO DE TERMINAR DE EDITAR LA CELDA SELECCIONADA ("terminar" significa apretar ENTER O ALGUNA FLECHA)
        private void dgvArticulos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            flag_cell_edited = true;
            numeroColumna = e.ColumnIndex;
            numeroFila = e.RowIndex;
            celdaActual = dgvArticulos.Rows[numeroFila].Cells[numeroColumna];
            
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            if (numeroColumna == 4 || numeroColumna == 6)
            {
                dgvArticulos.ReadOnly = false;
            //"double.TryParse" verifica si el contenido editado (luego de apretar enter) es un numero o no, si es un numero me lo delovuelve(con el "out double" que esta en el segundo parametro)
                bool isDouble = double.TryParse(celdaActual.Value.ToString(), out double resultadoNumerico);

                if (!isDouble)
                {
                    MessageBox.Show("Debe Ser Numerico El Valor Ingresado");
                }
                else
                {
                    //En esta parte "juego" con las columnas de los precios: coloco los precios de la columna StringPrecio a doublePrecio y se guarda en la bd  (menor o mayor)
                    modificarCeldaDePrecios(seleccionado, resultadoNumerico, numeroColumna);
                    negocioArt.modificar(seleccionado);
                }
                //aca le seteo a la CELDA correspondiente(que es de tipo String)el "valor" que va a contener y transformo  Double en un String de tipo moneda para que se vea en el dgvArticulos
                celdaActual.Value = resultadoNumerico.ToString("C0", new System.Globalization.CultureInfo("en-US"));
            }

          
            
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deudoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvArticulos.CurrentRow != null)
            {
                FormDeudores fd = new FormDeudores();
                fd.ShowDialog();
            }
        }

        
  //----------------------------------------SECCION VENTA----------------------------------------------------
       
       private double total = 0;
       private double importeAntiguo;
       private DataGridViewCell celdaActualVenta;
        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            if (txtPrecioVenta.Text != "")
            {
                int indiceFila = dgvArticulosVenta.Rows.Add();
                DataGridViewRow fila = dgvArticulosVenta.Rows[indiceFila];

           


            fila.Cells["cantidadVenta"].Value = txtCantidadVenta.Text;
            fila.Cells["nombreVenta"].Value = txtNombreVenta.Text;
            fila.Cells["importe"].Value = txtPrecioVenta.Text;



            
                double saldo = double.Parse(txtPrecioVenta.Text);
                
                //meter en una funcion
                total = total + saldo;
                lblTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("en-US"));

                
            }
          
        }
      
     

         private void dgvArticulosVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //ver si se puede simplificar porque esas 4 varibles se repiten en eñ evento "CELLBEING_EDIT"
            flag_cell_edited = true;
            numeroColumna = e.ColumnIndex;//actual
            numeroFila = e.RowIndex;
         
            celdaActualVenta = dgvArticulosVenta.Rows[numeroFila].Cells[numeroColumna];

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            if ( numeroColumna == 2)
            {
                dgvArticulosVenta.ReadOnly = false;
                //"double.TryParse" verifica si el contenido editado (luego de apretar enter) es un numero o no, si es un numero me lo delovuelve(con el "out double" que esta en el segundo parametro)
                bool isDouble = double.TryParse(celdaActualVenta.Value.ToString(), out double resultadoNumerico);

                if (!isDouble)
                {
                    MessageBox.Show("Debe Ser Numerico El Valor Ingresado");
                }

                
                    //ESTO SE PODRIA METER EN UN METODO! 
                    total = (total -importeAntiguo)+ resultadoNumerico;
                    lblTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("en-US"));

                //ESTO SE PODRIA METER EN UN METODO! 
                //aca le seteo a la CELDA correspondiente(que es de tipo String)el "valor" que va a contener y transformo  Double en un String de tipo moneda para que se vea en el dgvArticulos
                celdaActualVenta.Value = resultadoNumerico.ToString("C0", new System.Globalization.CultureInfo("en-US"));
            }

        }

        //BOTON PARA IMPRESION DE PDF (antes descargar nugets(2) y hacer imports(4) )
        //ItextSharp e ItextSharp.xmlworker  vers-5.5.13 GESTIONA LA CREACION DE PDF
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //CREACION DEL ARCHIVO (con nombre y extencion)
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Filter = "|*.pdf|*.txt|";//falta como cambiar la extension que quiero
            guardarArchivo.FileName = txtClienteVenta.Text+"-"+DateTime.Now.ToString("dd-MM-yyyy");

            //string ruta = @"C:\Users\Joel\Desktop\gh\ort\oem";
            // string rutaCompleta = Path.Combine(ruta, guardarArchivo.FileName);




            //ESCRIBIENDO PDF CON HTML
            //(linea 498)convierto nuestra pagina Html en una cadena de texto para "escribirlo" en el pdf
            string paginaHtml_texto = Properties.Resources.index.ToString();

            //MODELO MI HTML REEMPLAZANDO SUS VALORES CON LOS DE MI FORMULARIO------- 
            paginaHtml_texto = paginaHtml_texto.Replace("@CLIENTE",txtClienteVenta.Text);
            //  PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DOCUMENTO", txt.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            string filas = string.Empty;
           
            foreach (DataGridViewRow row in dgvArticulosVenta.Rows)
            {
                if (row.Index < dgvArticulosVenta.Rows.Count-1 )
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["cantidadVenta"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["nombreVenta"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["importe"].Value.ToString() + "</td>";
                    //filas += "<td>" + row.Cells["Importe"].Value.ToString() + "</td>";
                    filas += "</tr>";
                    //total += decimal.Parse(row.Cells["Importe"].Value.ToString());
                }
            }
            paginaHtml_texto = paginaHtml_texto.Replace("@FILAS", filas);
            paginaHtml_texto = paginaHtml_texto.Replace("@TOTAL", total.ToString("C0", new System.Globalization.CultureInfo("en-US")));

           
            //-----------------------------------------------------------------
            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                //Documentar--
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    //pdfDoc.Add(new Phrase("HOLAAA MUNDO")); agrego contenido y escribo un pdf


                    using (StringReader sr = new StringReader(paginaHtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();

                    stream.Close();
                }
                
                
                Process.Start(guardarArchivo.FileName);//abre el pdf automaticamente una vez guardado

                //// Esperar 3 segundoS para que el visor de PDF se abra completamente
                System.Threading.Thread.Sleep(1000);

                // Enviar el comando de impresión al visor de PDF
                SendKeys.Send("^p");// ^p es el atajo de teclado para imprimir en la mayoría de los visores de PDF
            }
           
        }
        //con este EVENTO RECUPERO EL CONTENIDO DE MI CELDA ACTUAL ANTES DE EDITARLO

        private void dgvArticulosVenta_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            numeroColumna = e.ColumnIndex;
            numeroFila = e.RowIndex;

            //variable se repite varias veces para dgvVentas...modificar y simplificar
            celdaActualVenta = dgvArticulosVenta.Rows[numeroFila].Cells[numeroColumna];
            if (numeroColumna == 2)
            {
                if (dgvArticulosVenta.Rows[numeroFila].Cells["importe"].Value != null)
                {
                    int numeroFilaActual = dgvArticulosVenta.CurrentRow.Index;
                    //recupero el contenido(string formato moneda)de mi celda actual(por defecto estan en string)
                    string saldo = dgvArticulosVenta.Rows[numeroFilaActual].Cells["importe"].Value.ToString();
                    //transformo el string en formato moneda a numeero (de tipo double)
                    importeAntiguo = pesosANumeros(saldo);

                }
                else
                {
                    importeAntiguo = 0;
                }
            }

        }

        //Metodo que  te devuelve el control o la celda qeu vas a editar
       
        
           

    private void dgvArticulosVenta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            var dataGridView = sender as DataGridView;




            if (e.Control is DataGridViewTextBoxEditingControl && dataGridView.CurrentCell.ColumnIndex == 1)
            {

                //CASTEO el control o celda a un TextBox y le seteo la lista "AutoCompleteVenta" al "e.Control"

                //se duplican los datos al agregar a la lista, corregirrr!!! (ya sea agregando un boton que limpie mis sugerencias)

               ((TextBox)e.Control).AutoCompleteCustomSource.AddRange(listaSugerencias.ToArray());//CON ESTO ME FUNCIONO EL "LAG"..CORREGIR DECISION DE COLUMNAS, AHORA SI DEBERIA PODER USAR LA COLUMNA 1 SIN LAG---> HACER ESTA TAREA

                ((TextBox)e.Control).AutoCompleteMode = AutoCompleteMode.Suggest;
                ((TextBox)e.Control).AutoCompleteSource = AutoCompleteSource.CustomSource;
              



            }
            else
            {

                ((TextBox)e.Control).AutoCompleteMode = AutoCompleteMode.None;
                ((TextBox)e.Control).AutoCompleteSource = AutoCompleteSource.None;

            }
            
               
            
           





        }

        private void btnImprimirLista_Click(object sender, EventArgs e)
        {
            //CREACION DEL ARCHIVO (con nombre y extencion)
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Filter = "|*.pdf|*.txt|";//falta como cambiar la extension que quiero
            guardarArchivo.FileName = txtClienteVenta.Text + "-" + DateTime.Now.ToString("dd-MM-yyyy");

            //string ruta = @"C:\Users\Joel\Desktop\gh\ort\oem";
            // string rutaCompleta = Path.Combine(ruta, guardarArchivo.FileName);




            //ESCRIBIENDO PDF CON HTML
            //(linea 498)convierto nuestra pagina Html en una cadena de texto para "escribirlo" en el pdf
            string paginaHtml_texto = Properties.Resources.listaPrecio.ToString();

            //MODELO MI HTML REEMPLAZANDO SUS VALORES CON LOS DE MI FORMULARIO------- 
            paginaHtml_texto = paginaHtml_texto.Replace("@CLIENTE", txtClienteVenta.Text);
            //  PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DOCUMENTO", txt.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                if (row.Index < dgvArticulos.Rows.Count - 1)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["nombre"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["precioxmenor"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["precioMenor"].Value.ToString() + "</td>";
                    //filas += "<td>" + row.Cells["Importe"].Value.ToString() + "</td>";
                    filas += "</tr>";
                    //total += decimal.Parse(row.Cells["Importe"].Value.ToString());
                }
            }
            paginaHtml_texto = paginaHtml_texto.Replace("@FILAS", filas);
            


            //-----------------------------------------------------------------
            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                //Documentar--
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    //pdfDoc.Add(new Phrase("HOLAAA MUNDO")); agrego contenido y escribo un pdf


                    using (StringReader sr = new StringReader(paginaHtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();

                    stream.Close();
                }


                Process.Start(guardarArchivo.FileName);//abre el pdf automaticamente una vez guardado

                //// Esperar 3 segundoS para que el visor de PDF se abra completamente
                System.Threading.Thread.Sleep(1000);

                // Enviar el comando de impresión al visor de PDF
                SendKeys.Send("^p");// ^p es el atajo de teclado para imprimir en la mayoría de los visores de PDF
            }

        }
    }
}


        /*  private bool esColumnaEditable() {
bool puedeEditar = false;
if( numeroColumna

retun
}*/




    


