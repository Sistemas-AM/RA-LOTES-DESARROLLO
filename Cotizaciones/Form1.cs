using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Cotizaciones.DBHelper;

namespace Cotizaciones
{
    public partial class Form1 : Form
    {
        public int idalmacen;
        private List<Cotizacion> Cotizaciones = new List<Cotizacion>();

        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView1();
        }

        private void InitializeDataGridView1()
        {

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Text = "";
            btn.Name = "Codigo";
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[7].Width = 60;

            dataGridView1.Columns[1].Name = "Nombre";
            dataGridView1.Columns[2].Name = "Cantidad";
            dataGridView1.Columns[3].Name = "Existencia";
            dataGridView1.Columns[4].Name = "Precio";
            dataGridView1.Columns[5].Name = "% Descto";
            dataGridView1.Columns[6].Name = "Peso";
            dataGridView1.Columns[7].Name = "Importe";
        }
        private List<Sucursal> sucursales = getSucursales();
        private void Form1_Load(object sender, EventArgs e)
        {

            foreach (Sucursal folio in sucursales)
            {
                if (folio.sucursal != null)
                {
                    comboBoxSucursales.Items.Add(folio.sucursal.ToString());
                }
            }
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBoxSucursales.Enabled = true;
            }
            else
            {
                comboBoxSucursales.Enabled = false;
                comboBoxSucursales.Text = Principal.Variablescompartidas.sucursalcorta;
            }

        }



        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {


            guardaCotizaciones();
            //limpiaForm();

        }

        private void limpiaForm()
        {
            textBoxAtencion.Text = "";
            textBoxCiudad.Text = "";
            textBoxCliente.Text = "";
            textBoxColonia.Text = "";
            textBoxCP.Text = "";
            textBoxDireccion.Text = "";
            textBoxEstado.Text = "";
            textBoxFolioNuevo.Text = "";
            textBoxIVA.Text = "";
            textBoxMail.Text = "";
            textBoxNombre.Text = "";
            textBoxNumero.Text = "";
            textBoxObservaciones.Text = "";
            textBoxPais.Text = "";
            textBoxRFC.Text = "";
            textBoxSerie.Text = "";
            textBoxSolicito.Text = "";
            textBoxSubTotal.Text = "";
            textBoxTelefono.Text = "";
            textBoxTotal.Text = "";
            comboBoxTipoDePago.Enabled = true;
            comboBoxTipoDePago.Text = "Efectivo";
            
            dateTimePicker1.Value = DateTime.Now;
            dataGridView1.Rows.Clear();
            setFolioNuevo();

        }

        private void guardaCotizaciones()
        {
            bool guardado = false;
            // Solo cuando hay una sucursal seleccionada
            // Valida los campos de la cotización, si no está validado regresa sin guardar valores
            if (!comboBoxSucursales.Text.Equals(""))
            {
                if (!validaCotizacion(radioButton1.Checked))
                {
                    return;
                }

                if (existeCotizacion(Convert.ToInt32(textBoxFolioNuevo.Text), comboBoxSucursales.Text))
                {
                    DialogResult dialogresult = MessageBox.Show("Ya existe una cotización con el mismo folio... ¿Desea actualizarla?", "Ya existe cotización", MessageBoxButtons.OKCancel);
                    if (dialogresult == DialogResult.OK)
                    {
                        eliminaCotizaciones(Convert.ToInt32(textBoxFolioNuevo.Text), comboBoxSucursales.Text);
                    }
                    else if (dialogresult == DialogResult.Cancel)
                    {
                        return;
                    }

                }

                //Recorre la tabla de cotizaciones
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int idProducto = 0;
                        string sp = "";
                        idProducto = getProductoId(row.Cells[0].Value.ToString());
                        sp = getProductoSp(row.Cells[0].Value.ToString());
                        if (row.Cells[0].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null
                            && row.Cells[5].Value != null && row.Cells[6].Value != null && row.Cells[7].Value != null)
                        {
                            try
                            {
                                generaCotizacion(float.Parse(row.Cells[2].Value.ToString()), row.Cells[0].Value.ToString(),
                                idProducto, float.Parse(row.Cells[7].Value.ToString()), float.Parse(row.Cells[6].Value.ToString()),
                                float.Parse(row.Cells[4].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), sp, float.Parse(row.Cells[5].Value.ToString()));
                                guardado = true;
                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("Favor de ingresar el tipo de datos correcto en todos los campos.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Faltan datos en la tabla.");
                        }
                    }

                }
                if (guardado)
                {
                    actualizaFolio();

                }
            }
        }
        private bool validaCotizacion(bool isAdminPAQ)
        {

            if (isAdminPAQ)
            {
                if (textBoxCliente.Text.Equals(""))
                {
                    MessageBox.Show("Favor de seleccionar un cliente.");
                    return false;
                }
            }
            //if (textBoxAtencion.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta el campo de atención.");
            //    return false;
            //}
            //if (textBoxCiudad.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta el campo de ciudad.");
            //    return false;
            //}
            

            //if (textBoxColonia.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Colonia.");
            //    return false;
            //}

            //if (textBoxCP.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta CP.");
            //    return false;
            //}
            //if (textBoxDireccion.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Direccion.");
            //    return false;
            //}
            //if (textBoxEstado.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Estado.");
            //    return false;

            //}
            //if (textBoxIVA.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta IVA.");
            //    return false;
            //}
            //if (textBoxMail.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta E-Mail.");
            //    return false;
            //}
            if (textBoxNombre.Text.Equals(""))
            {
                MessageBox.Show("Falta Nombre.");
                return false;
            }
            //if (textBoxNumero.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Numero.");
            //    return false;
            //}
            //if (textBoxObservaciones.Text.Equals(""))
            //{
            //    MessageBox.Show("Faltan Observaciones.");
            //    return false;
            //}
            //if (comboBoxTipoDePago.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Pago.");
            //    return false;
            //}
            //if (textBoxPais.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Pais.");
            //    return false;
            //}
            //if (textBoxRFC.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta RFC.");
            //    return false;
            //}
            if (textBoxSerie.Text.Equals(""))
            {
                MessageBox.Show("Falta Serie.");
                return false;
            }
            //if (textBoxSolicito.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Solicito.");
            //    return false;
            //}
            //if (comboBoxSucursales.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Sucursal.");
            //    return false;
            //}
            //if (textBoxTelefono.Text.Equals(""))
            //{
            //    MessageBox.Show("Falta Telefono.");
            //    return false;
            //}
            if (textBoxFolioNuevo.Text.Equals(""))
            {
                return false;
            }
            return true;
        }
        private void generaCotizacion(float cantidad, string codigoproducto, int idproducto, float importe,
            float kilos, float precio, int existencia, string sp, float descto)
        {

            Cotizacion cotizacion = new Cotizacion();

            cotizacion.atencion = textBoxAtencion.Text;

            cotizacion.ciudad = textBoxCiudad.Text;

            if (radioButton1.Checked)
            {
                cotizacion.cliente = textBoxCliente.Text;
            }
            else
            {
                cotizacion.cliente = ".";
            }

            cotizacion.colonia = textBoxColonia.Text;

            cotizacion.cp = textBoxCP.Text;

            cotizacion.direccion = textBoxDireccion.Text;

            cotizacion.estado = textBoxEstado.Text;

            cotizacion.iva = float.Parse(textBoxIVA.Text);

            cotizacion.mail = textBoxMail.Text;

            cotizacion.nombre = textBoxNombre.Text;

            cotizacion.numero = textBoxNumero.Text;

            cotizacion.observa = textBoxObservaciones.Text;

            cotizacion.pago = comboBoxTipoDePago.Text;

            cotizacion.descto = 0;

            cotizacion.descto = descto;

            if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
            {
                cotizacion.descto = cotizacion.descto + 1.5f;
            }
            if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
            {
                cotizacion.descto = cotizacion.descto + 3;
            }



            cotizacion.pais = textBoxPais.Text;

            cotizacion.rfc = textBoxRFC.Text;

            cotizacion.serie = textBoxSerie.Text;

            cotizacion.solicito = textBoxSolicito.Text;

            cotizacion.sucursal = comboBoxSucursales.Text;

            cotizacion.telefono = textBoxTelefono.Text;

            cotizacion.folio = Convert.ToInt32(textBoxFolioNuevo.Text);

            cotizacion.fecha = dateTimePicker1.Value;
            cotizacion.cantidad = cantidad;
            cotizacion.codigopro = codigoproducto;
            cotizacion.idproducto = idproducto;
            cotizacion.importe = importe;
            cotizacion.kilos = kilos;
            cotizacion.precio = precio;
            cotizacion.surtida = existencia;
            //admProductos CTextoExtra01
            cotizacion.sp = sp;
            //No sirven de nada :(
            cotizacion.tipo = 0;
            cotizacion.tipocot = 0;


            guardaCotizacion(cotizacion);

        }

        private void actualizaFolio()
        {
            sucursales = getSucursales();
            var folio = sucursales.Find(x => x.sucursal == comboBoxSucursales.Text);
            if (Convert.ToInt32(folio.ultimoFolio) < Convert.ToInt32(textBoxFolioNuevo.Text))
            {
                if (!textBoxFolioNuevo.Text.Equals(""))
                {
                    updateFolio(comboBoxSucursales.Text, Convert.ToInt32(textBoxFolioNuevo.Text));
                    //textBoxFolioNuevo.Text = (Convert.ToInt32(textBoxFolioNuevo.Text) + 1).ToString();
                    //Cotizacion cotizacion = new Cotizacion();
                }
                else
                {
                    MessageBox.Show("Falta folio, favor de seleccionar una sucursal para asignar un folio nuevo.");
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea Guardar La Cotizacion?", "Imprimir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                guardaCotizaciones();

                //PDFHelper.creaPDF(Cotizaciones,comboBoxSucursales.Text, textBoxSubTotal.Text, textBoxIVA.Text, textBoxTotal.Text);
                //conexion
                infogerente.sucursal = "";
                infogerente.direccion = "";
                infogerente.colonia = "";
                infogerente.lugar = "";
                infogerente.telefono = "";
                infogerente.celular = "";
                infogerente.nombre = "";
                infogerente.email = "";

                SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                //Obtener valor o varios valores de la base de datos

                cmd.CommandText = "select sucnom, direccion, colonia, lugar, telefono, celular, nombre, email from datger where sucursal = '" + comboBoxSucursales.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    infogerente.sucursal = reader["sucnom"].ToString();
                    infogerente.direccion = reader["direccion"].ToString();
                    infogerente.colonia = reader["colonia"].ToString();
                    infogerente.lugar = reader["lugar"].ToString();
                    infogerente.telefono = reader["telefono"].ToString();
                    infogerente.celular = reader["celular"].ToString();
                    infogerente.nombre = reader["nombre"].ToString();
                    infogerente.email = reader["email"].ToString();


                }
                sqlConnection1.Close();

                
                infogerente.subtotal = textBoxSubTotal.Text;
                infogerente.iva = textBoxIVA.Text;
                infogerente.total = textBoxTotal.Text;
                VariablesCompartidas.Folio = textBoxFolioNuevo.Text;
                VariablesCompartidas.Fecha = dateTimePicker1.Text;
                VariablesCompartidas.Cliente = textBoxNombre.Text;
                VariablesCompartidas.Telefono = textBoxTelefono.Text;
                VariablesCompartidas.Direccion = textBoxDireccion.Text;
                VariablesCompartidas.Email = textBoxMail.Text;
                VariablesCompartidas.Atencion = textBoxAtencion.Text;
                VariablesCompartidas.Solicito = textBoxSolicito.Text;
                VariablesCompartidas.Textos = textBoxObservaciones.Text;
                VariablesCompartidas.SerieImp = textBoxSerie.Text;

                using (RepCrisRep RC = new RepCrisRep(dataGridView1))
                {
                    RC.ShowDialog();
                }
            }
            else if (result == DialogResult.No)
            {
               
            }

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            limpiaForm();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
                using (Clientesadm cl = new Clientesadm())
                {
                    cl.ShowDialog();
                }

                try
                {
                    textBoxCliente.Text = VariablesCompartidas.Codigo;
                    //---------------------------Llenar el cliente ---------------------------------
                    cmd.CommandText = "select CCODIGOCLIENTE,  CRAZONSOCIAL, CRFC, CEMAIL1, CNOMBRECALLE, CNUMEROEXTERIOR, CTELEFONO1, CCOLONIA, CCODIGOPOSTAL, CPAIS, CCIUDAD, CESTADO from admClientes inner join admDomicilios on admClientes.CIDCLIENTEPROVEEDOR = admDomicilios.CIDCATALOGO  where CCODIGOCLIENTE='" + VariablesCompartidas.Codigocte + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {

                        textBoxNombre.Text = reader["CRAZONSOCIAL"].ToString();
                        textBoxRFC.Text = reader["CRFC"].ToString();
                        textBoxMail.Text = reader["CEMAIL1"].ToString();
                        textBoxDireccion.Text = reader["CNOMBRECALLE"].ToString();
                        textBoxNumero.Text = reader["CNUMEROEXTERIOR"].ToString();
                        textBoxTelefono.Text = reader["CTELEFONO1"].ToString();
                        textBoxColonia.Text = reader["CCOLONIA"].ToString();
                        textBoxCP.Text = reader["CCODIGOPOSTAL"].ToString();
                        textBoxPais.Text = reader["cpais"].ToString();
                        textBoxCiudad.Text = reader["cciudad"].ToString();
                        textBoxEstado.Text = reader["cestado"].ToString();


                    }
                    sqlConnection2.Close();
                    //-----------------------------------------------------------------------------------------

                }
                catch (Exception)
                {

                }



            //}
            //else if (radioButton2.Checked)
            //{
            //    using(ctelocales cl = new ctelocales())
            //    {
            //        cl.ShowDialog();
            //    }
            //    try
            //    {
            //        textBoxCliente.Text = VariablesCompartidas.Codigo;
            //        //---------------------------Llenar el cliente ---------------------------------
            //        cmd.CommandText = "select nombre, rfc, direccion, numero, telefono, colonia, cp, email, pais, ciudad, estado from ctelocal where id_ctelocal='" + VariablesCompartidas.Codigocte + "'";
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlConnection1;
            //        sqlConnection1.Open();
            //        reader = cmd.ExecuteReader();

            //        // Data is accessible through the DataReader object here.
            //        if (reader.Read())
            //        {

            //            textBoxNombre.Text = reader["nombre"].ToString();
            //            textBoxRFC.Text = reader["rfc"].ToString();
            //            textBoxMail.Text = reader["email"].ToString();
            //            textBoxDireccion.Text = reader["direccion"].ToString();
            //            textBoxNumero.Text = reader["numero"].ToString();
            //            textBoxTelefono.Text = reader["telefono"].ToString();
            //            textBoxColonia.Text = reader["colonia"].ToString();
            //            textBoxCP.Text = reader["cp"].ToString();
            //            textBoxPais.Text = reader["pais"].ToString();
            //            textBoxCiudad.Text = reader["ciudad"].ToString();
            //            textBoxEstado.Text = reader["estado"].ToString();
                        

            //        }
            //        sqlConnection1.Close();
            //        //-----------------------------------------------------------------------------------------

            //    }
            //    catch (Exception)
            //    {

            //    }



            //}
        }

        private void cargaCliente()
        {
            using (ClientesForm cliente = new ClientesForm(radioButton1.Checked))
            {
                cliente.ShowDialog(this);
                bindClienteToForm(
                ClientesForm.CCODIGOCLIENTE,
                ClientesForm.CRAZONSOCIAL,
                ClientesForm.CNOMBRECALLE,
                ClientesForm.CTELEFONO01,
                ClientesForm.CEMAIL,
                ClientesForm.CCOLONIA,
                ClientesForm.CCIUDAD,
                ClientesForm.CESTADO,
                ClientesForm.CRFC,
                ClientesForm.CCODIGOPOSTAL,
                ClientesForm.CPAIS,
                ClientesForm.CNUMEROEXTERIOR,
                //Inicializa el tipo de pago en Efectivo
                comboBoxTipoDePago.Items[0].ToString(),
                "", "", "", "");
                //dataGridView1.Rows.Clear();

            }
        }

        private void bindClienteToForm(string cliente, string nombre, string direccion, string telefono, string mail, string colonia, string ciudad, string estado, string rfc, string cp, string pais, string numero, string tipoPago, string atencion, string solicito, string observa, string iva)
        {
            //En este metodo se lleva
            textBoxCliente.Text = cliente;
            textBoxNombre.Text = nombre;
            textBoxDireccion.Text = direccion;
            textBoxTelefono.Text = telefono;
            textBoxMail.Text = mail;
            textBoxColonia.Text = colonia;
            textBoxCiudad.Text = ciudad;
            textBoxEstado.Text = estado;
            textBoxRFC.Text = rfc;
            textBoxCP.Text = cp;
            textBoxPais.Text = pais;
            textBoxNumero.Text = numero;
            //Inicializa el tipo de pago en Efectivo
            comboBoxTipoDePago.Text = tipoPago;
            textBoxAtencion.Text = atencion;
            textBoxSolicito.Text = solicito;
            textBoxObservaciones.Text = observa;
            textBoxIVA.Text = iva;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargaCotizacion(false);
        }

        private void cargaCotizacion(bool esOriginal)
        {
            using (CotizacionesForm cotizaciones = new CotizacionesForm(comboBoxSucursales.Text, radioButton1.Checked))
            {
                cotizaciones.ShowDialog(this);


                dataGridView1.Rows.Clear();
                for (var i = 0; i < CotizacionesForm.Cotizacion.Count; i++)
                {
                    // --------------------------------------CLIENTE--------------------------
                    bindClienteToForm(CotizacionesForm.Cotizacion[i].cliente,
                        CotizacionesForm.Cotizacion[i].nombre,
                        CotizacionesForm.Cotizacion[i].direccion,
                        CotizacionesForm.Cotizacion[i].telefono,
                        CotizacionesForm.Cotizacion[i].mail,
                        CotizacionesForm.Cotizacion[i].colonia,
                        CotizacionesForm.Cotizacion[i].ciudad,
                        CotizacionesForm.Cotizacion[i].estado,
                        CotizacionesForm.Cotizacion[i].rfc,
                        CotizacionesForm.Cotizacion[i].cp,
                        CotizacionesForm.Cotizacion[i].pais,
                        CotizacionesForm.Cotizacion[i].numero,
                        CotizacionesForm.Cotizacion[i].pago,
                        CotizacionesForm.Cotizacion[i].atencion,
                        CotizacionesForm.Cotizacion[i].solicito,
                        CotizacionesForm.Cotizacion[i].observa,
                        CotizacionesForm.Cotizacion[i].iva.ToString());

                    Producto producto = new Producto();
                    producto = getProducto(CotizacionesForm.Cotizacion[i].codigopro.Replace(" ", ""));
                    textBoxFolioNuevo.Text = CotizacionesForm.Cotizacion[i].folio.ToString();

                    //-------------------------------------TABLA-DE-PRODUCTOS------------------------------
                    dataGridView1.Rows.Insert(i);
                    dataGridView1.Rows[i].Cells["Codigo"].Value = CotizacionesForm.Cotizacion[i].codigopro.Replace(" ", "");
                    dataGridView1.Rows[i].Cells[1].Value = producto.CNOMBREPRODUCTO;

                    dataGridView1.Rows[i].Cells[2].Value = CotizacionesForm.Cotizacion[i].cantidad;

                    if (!esOriginal)
                        {
                        dataGridView1.Rows[i].Cells[3].Value = getExistencias(producto.CIDPRODUCTO, idalmacen).ToString();
                        //dataGridView1.Rows[i].Cells[3].Value = producto.CCONTROLEXISTENCIA;
                        dataGridView1.Rows[i].Cells[4].Value = Math.Round (float.Parse(producto.CPRECIO1.ToString()) / 1.16,4 );

                        //dataGridView1.Rows[i].Cells[4].Value = Math.Round(float.Parse(CotizacionesForm.Cotizacion[i].precio.ToString()) / 1.16, 4);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[3].Value = CotizacionesForm.Cotizacion[i].surtida;
                        dataGridView1.Rows[i].Cells[4].Value = CotizacionesForm.Cotizacion[i].precio;
                        
                    }

                    dataGridView1.Rows[i].Cells[5].Value = CotizacionesForm.Cotizacion[i].descto;

                    dataGridView1.Rows[i].Cells[6].Value = CotizacionesForm.Cotizacion[i].kilos;
                    dataGridView1.Rows[i].Cells[7].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) * float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());

                    // Fecha
                    dateTimePicker1.Value = CotizacionesForm.Cotizacion[i].fecha;




                }
                this.Cotizaciones = CotizacionesForm.Cotizacion;
            }
        }

        private void comboBoxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFolioNuevo();
        }

        private void setFolioNuevo()
        {
            //Utiliza la información más actual del folio por sucursal para asignar un folio nuevo

            try
            {
                sucursales = getSucursales();
                var folio = sucursales.Find(x => x.sucursal == comboBoxSucursales.Text);
                idalmacen = Convert.ToInt32(folio.idalmacen);
                if (folio.ultimoFolio == null)
                {
                    textBoxFolioNuevo.Text = "1";
                }
                else
                {
                    textBoxFolioNuevo.Text = (Convert.ToInt32(folio.ultimoFolio) + 1).ToString();
                }
                textBoxSerie.Text = folio.letra;
            }
            catch (Exception)
            {

                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                using (ProductosForm producto = new ProductosForm(idalmacen))
                {
                    producto.ShowDialog(this);
                }
                if (VariablesCompartidas.cancelado != "0")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value == null)
                    {
                        dataGridView1.Rows.Insert(e.RowIndex);
                    }

                    try
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value = ProductosForm.CCODIGOPRODUCTO;
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = ProductosForm.CNOMBREPRODUCTO;
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = 1;
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = ProductosForm.CCONTROLEXISTENCIA;

                        if (ProductosForm.CCODIGOPRODUCTO.Substring(0,1) == "X" )
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((float.Parse(ProductosForm.CPRECIO1) / 1.16), 4).ToString();
                        }else
                        {
                            if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((float.Parse(ProductosForm.CPRECIO1) / 1.16) - ((float.Parse(ProductosForm.CPRECIO1) / 1.16) * 0.015f), 4).ToString();

                            }
                            else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((float.Parse(ProductosForm.CPRECIO1) / 1.16) - ((float.Parse(ProductosForm.CPRECIO1) / 1.16) * 0.03f), 4).ToString();

                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((float.Parse(ProductosForm.CPRECIO1) / 1.16), 4).ToString();

                            }
                        }

                        


                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = 0;
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = ProductosForm.CPESOPRODUCTO;
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    }
                    catch (Exception)
                    {


                    } 
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (!dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Equals("") && dataGridView1.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    try
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        //BoxTotal.Text = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()).ToString());
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Ingresa un número");
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = 1;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Ingresa un número");
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = 1;
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Ingresa un número... más pequeño");
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = 1;
                    }
                }
            }
            if (e.ColumnIndex == 5)
            {
                try
                {
                    float descuento = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) / 100;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = Math.Round((float.Parse(ProductosForm.CPRECIO1) / 1.16) - ((float.Parse(ProductosForm.CPRECIO1) / 1.16) * descuento), 4).ToString();
                    dataGridView1.Rows[e.RowIndex].Cells[7].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                catch (FormatException)
                {

                    MessageBox.Show("Ingresa un número");
                    dataGridView1.Rows[e.RowIndex].Cells[5].Value = 0;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult di = MessageBox.Show("¿Guardar al cliente?", "Nuevo Cliente", MessageBoxButtons.YesNo);
            if (di == DialogResult.Yes)
            {
                try
                {
                    string sql = "insert into ctelocal (nombre, rfc, direccion, numero, telefono, colonia, cp, email, pais, ciudad, estado) values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11)";
                    SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", textBoxNombre.Text); //nombre
                    cmd2.Parameters.AddWithValue("@param2", textBoxRFC.Text); //rfc
                    cmd2.Parameters.AddWithValue("@param3", textBoxDireccion.Text); //direccion
                    cmd2.Parameters.AddWithValue("@param4", textBoxNumero.Text); //numero
                    cmd2.Parameters.AddWithValue("@param5", textBoxTelefono.Text); //telefono
                    cmd2.Parameters.AddWithValue("@param6", textBoxColonia.Text); //colonia
                    cmd2.Parameters.AddWithValue("@param7", textBoxCP.Text); //cp
                    cmd2.Parameters.AddWithValue("@param8", textBoxMail.Text); //email
                    cmd2.Parameters.AddWithValue("@param9", textBoxPais.Text); //Pais
                    cmd2.Parameters.AddWithValue("@param10", textBoxCiudad.Text); //Ciudad
                    cmd2.Parameters.AddWithValue("@param11", textBoxEstado.Text); //estado

                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    //label25.Text = (row.Cells["Column1"].Value.ToString());
                    sqlConnection1.Close();
                    MessageBox.Show("Cliente Guardado Con Exito");
                }
                catch (Exception)
                {

                    MessageBox.Show("No se pudo grabar al cliente");
                }




            }
        }


        private void generaClienteNuevo()
        {
            ClienteActual cliente = new ClienteActual();

            cliente.CCIUDAD = textBoxCiudad.Text;
            cliente.CCOLONIA = textBoxColonia.Text;
            cliente.CCODIGOPOSTAL = textBoxCP.Text;
            cliente.CNOMBRECALLE = textBoxDireccion.Text;
            cliente.CESTADO = textBoxEstado.Text;
            cliente.CEMAIL = textBoxMail.Text;
            cliente.CRAZONSOCIAL = textBoxNombre.Text;
            cliente.CNUMEROEXTERIOR = textBoxNumero.Text;
            cliente.CPAIS = textBoxPais.Text;
            cliente.CTELEFONO01 = textBoxTelefono.Text;
            cliente.CRFC = textBoxRFC.Text;


            guardaCliente(cliente);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargaCotizacion(true);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 7)
            {
                calculaTotales();
            }

        }

        private void calculaTotales()
        {
            float subtotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                
                if (row.Cells[7].Value != null)
                {
                    subtotal += float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString());
                    //if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                    //{
                    //    float descuento = float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) * 0.015f;
                    //    subtotal = subtotal  - descuento;

                    //}
                    //else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                    //{
                    //    float descuento = float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) * 0.03f;
                    //    subtotal = subtotal  - descuento;

                    //}
                    //else
                    //{
                        subtotal = subtotal;
                    //}
                }
            }
            textBoxSubTotal.Text = Math.Round(subtotal, 2).ToString();
           // textBoxTotal.Text = subtotal.ToString();

            float iva = float.Parse(textBoxSubTotal.Text.ToString()) * 0.16f;
            //textBoxSubTotal.Text = iva.ToString();
            textBoxIVA.Text = Math.Round(iva , 2).ToString();
            textBoxTotal.Text = Math.Round(subtotal + iva,2).ToString();
            //textBoxIVA.Text = (float.Parse(textBoxTotal.Text) - float.Parse(textBoxSubTotal.Text)).ToString();

        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calculaTotales();
        }

        private void comboBoxTipoDePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(comboBoxTipoDePago.Text.Equals("Descuento 1.5") || comboBoxTipoDePago.Text.Equals("Descuento 3"))
            //{
            //    calculaTotales();
            //}
            try
            {
                if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                {
                    //float descuento = float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) * 0.015f;
                    comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells[0].Value.ToString().Substring(0,1) != "X")
                        {
                            dataGridView1.Rows[row.Index].Cells[4].Value = float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString()) - float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString()) * 0.015f;
                        }
                        
                        dataGridView1.Rows[row.Index].Cells[7].Value = float.Parse(dataGridView1.Rows[row.Index].Cells[2].Value.ToString()) * float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString());

                        calculaTotales();
                        
                    }

                }
                else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                {
                    comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells[0].Value.ToString().Substring(0, 1) != "X")
                        {
                            dataGridView1.Rows[row.Index].Cells[4].Value = float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString()) - float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString()) * 0.03f;
                        }
                        
                        dataGridView1.Rows[row.Index].Cells[7].Value = float.Parse(dataGridView1.Rows[row.Index].Cells[2].Value.ToString()) * float.Parse(dataGridView1.Rows[row.Index].Cells[4].Value.ToString());
                        calculaTotales();
                        
                    }
                }
            }
            catch (Exception)
            {

              
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                calculaTotales();
            }
            catch (Exception)
            {

            }
        }
    }
}