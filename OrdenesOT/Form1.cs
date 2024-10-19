using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace OrdenesOT
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);

        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        Metodos met = new Metodos();
        int cont = 0;
        int continua = 0;
        int error = 1;
        string idDocumento;
        string idMovimiento;
        string cteloc, idloc, eslocal, ultFol;
        string letra, destina, noguia, mensa, cuentamensa, cajas, peso;
        DataTable dt2 = new DataTable();
        public static string sucursal { get; set; }
        public static string CodigoPasa { get; set; }
        public static string DescripcionPasa { get; set; }
        public static string ObservaPasa { get; set; }
        public static string CantidadPasa { get; set; }
        public static string idClientePasa { get; set; }
        public static string idDireccionPasa { get; set; }

        public static string idEnvioPasa { get; set; }

        public static string MovPasa { get; set; }

        public static string idImpresion { get; set; }


        public static string idDocumentoPasa { get; set; }

        int permiteCerrar = 0;

        string conceptoPedido = "";
        string seriePedido = "";

        public Form1()
        {
            //27-04  1556
            InitializeComponent();
            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            textFecha.Text = DateTime.Now.ToShortDateString();
            comboBoxTipoDePago.Text = "Efectivo";

            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox1.Enabled = true;
            }
            else
            {
                //comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
                comboBox1.Text = "IGS";
            }

            //if (Principal.Variablescompartidas.Perfil_id == "36")
            //{
            //    Aceptadas.Visible = true;
            //    AutorizaBut.Visible = true;

            //    button8.Visible = false;
            //    materialRaisedButton1.Visible = false;
            //}
            //else
            //{
            //    Aceptadas.Visible = false;
            //    AutorizaBut.Visible = false;

            //    button8.Visible = true;
            //    materialRaisedButton1.Visible = true;
            //}



        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            permiteCerrar = 0;
            //panel1.BackColor = ColorTranslator.FromHtml("#DDE4EE");
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#CBD0D3");
            button1.BackColor = ColorTranslator.FromHtml("#06a038");

            button2.BackColor = ColorTranslator.FromHtml("#FFC11A");
            button3.BackColor = ColorTranslator.FromHtml("#049bdc");
            button4.BackColor = ColorTranslator.FromHtml("#d93a2c");
            button5.BackColor = ColorTranslator.FromHtml("#CE5248");
            button8.BackColor = ColorTranslator.FromHtml("#06a038");

            CargaCombo();
            CargaComboTipoCliente();

            //if (Principal.Variablescompartidas.sucural == "AUDITOR")
            //{
            //    comboBox1.Enabled = true;
            //}
            //else
            //{
            //    comboBox1.Enabled = false;
            //    comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
            //}

            //cargaTras();
        }



        private void CargaCombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("SELECT sucursal, ultimoFolio, letra, idalmacen FROM folios where idalmacen in (6,17)", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucursal", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "letra";
            comboBox1.DisplayMember = "sucursal";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();
        }

        private void CargaComboTipoCliente()
        {
            sqlConnection3.Open();
            SqlCommand sc = new SqlCommand("select id_tipoCli as id, Nombre from TipoCliente", sqlConnection3);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));

            dt.Load(reader);

            comboBox3.ValueMember = "id";
            comboBox3.DisplayMember = "Nombre";
            comboBox3.DataSource = dt;

            sqlConnection3.Close();
        }



        //protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        //{
        //    if ((keyData != Keys.F1) & (keyData != Keys.F2))
        //        return base.ProcessCmdKey(ref msg, keyData);

        //    if (keyData == Keys.F2)
        //    {
        //        using (PantallaProductos pp = new PantallaProductos())
        //        {
        //            pp.ShowDialog();
        //        }
        //        if (!string.IsNullOrEmpty(PantallaProductos.codigo))
        //        {
        //            dataGridView1.Select();
        //            dataGridView1.Rows.Add();
        //            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value = PantallaProductos.codigo;
        //            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value = PantallaProductos.nombre;

        //            if (PantallaProductos.codigo.Substring(0, 1) == "X")
        //            {
        //                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
        //                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
        //                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
        //            }
        //            else
        //            {
        //                if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
        //                {
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "1.5";
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.precio)) - ((Convert.ToDouble(PantallaProductos.precio)) * 0.015f), 4).ToString();

        //                }
        //                else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
        //                {
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.precio)) - ((Convert.ToDouble(PantallaProductos.precio)) * 0.03f), 4).ToString();

        //                }
        //                else
        //                {
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
        //                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);

        //                }

        //                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value = PantallaProductos.peso;
        //                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

        //            }

        //            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = "1";
        //            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);

        //        }
        //    }
        //    return true;
        //}

        private void cargarCliente(string codigo)
        {
            try
            {
                //---------------------------Llenar el cliente ---------------------------------
                cmd.CommandText = "select top 1 * from ClientesDirecciones where CCODIGOCLIENTE ='" + codigo + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    idCliente.Text = reader["cidclienteproveedor"].ToString();
                    CodigoCli.Text = reader["CCODIGOCLIENTE"].ToString();
                    NombreCli.Text = reader["CRAZONSOCIAL"].ToString();
                    Rfc.Text = reader["CRFC"].ToString();
                    Correo.Text = reader["CEMAIL1"].ToString();
                    direccion.Text = reader["CNOMBRECALLE"].ToString();
                    Numero.Text = reader["CNUMEROEXTERIOR"].ToString();
                    Telefono.Text = reader["CTELEFONO1"].ToString();
                    Colonia.Text = reader["CCOLONIA"].ToString();
                    Cp.Text = reader["CCODIGOPOSTAL"].ToString();
                    Pais.Text = reader["cpais"].ToString();
                    Ciudad.Text = reader["cciudad"].ToString();
                    Estado.Text = reader["cestado"].ToString();



                }
                sqlConnection2.Close();
                //-----------------------------------------------------------------------------------------

            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void cargarClienteLOC(string codigo)
        {
            try
            {
                //---------------------------Llenar el cliente ---------------------------------
                cmd.CommandText = "select * from ctelocal where codigocliente = '" + codigo + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    CodigoCli.Text = codigo;
                    NombreCli.Text = reader["nombre"].ToString();
                    Rfc.Text = reader["rfc"].ToString();
                    Correo.Text = reader["email"].ToString();
                    direccion.Text = reader["direccion"].ToString();
                    Numero.Text = reader["numero"].ToString();
                    Telefono.Text = reader["telefono"].ToString();
                    Colonia.Text = reader["colonia"].ToString();
                    Cp.Text = reader["cp"].ToString();
                    Pais.Text = reader["pais"].ToString();
                    Ciudad.Text = reader["ciudad"].ToString();
                    Estado.Text = reader["estado"].ToString();


                }
                sqlConnection1.Close();
                //-----------------------------------------------------------------------------------------

            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void limpiarCliente()
        {
            CodigoCli.Clear();
            NombreCli.Clear();
            direccion.Clear();
            Colonia.Clear();
            Telefono.Clear();
            Correo.Clear();
            Ciudad.Clear();
            Estado.Clear();
            Rfc.Clear();
            Numero.Clear();
            Cp.Clear();
            Pais.Clear();
        }

        private void esCredito(string codigo)
        {
            cmd.CommandText = "select CBANVENTACREDITO from admClientes where CCODIGOCLIENTE = '" + codigo + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                if (reader["CBANVENTACREDITO"].ToString() == "1")
                {
                    radioButton2.Checked = true;
                    radioButton1.Checked = false;
                }
                else if (reader["CBANVENTACREDITO"].ToString() == "0")
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = true;
                }

            }
            sqlConnection2.Close();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {

            using (PantallaClientes pc = new PantallaClientes())
            {
                pc.ShowDialog();
            }
            if (!string.IsNullOrEmpty(PantallaClientes.codigo))
            {
                limpiarCliente();
                CodigoCli.Enabled = false;
                NombreCli.Enabled = false;
                direccion.Enabled = false;
                Colonia.Enabled = false;
                Telefono.Enabled = false;
                Correo.Enabled = false;
                Ciudad.Enabled = false;
                Estado.Enabled = false;
                Rfc.Enabled = false;
                Numero.Enabled = false;
                Cp.Enabled = false;
                Pais.Enabled = false;
                CodigoCli.Text = PantallaClientes.codigo;
                NombreCli.Text = PantallaClientes.Nombre;
                idCliente.Text = PantallaClientes.IdPasa;
                comboBox2.Text = PantallaClientes.SegmentoPasa;


                if (PantallaClientes.esLocal == "1")
                {
                    eslocal = "1";
                    cargarClienteLOC(PantallaClientes.codigo);
                } else if (PantallaClientes.esLocal == "0")
                {
                    eslocal = "0";
                    cargarCliente(PantallaClientes.codigo);
                    esCredito(PantallaClientes.codigo);
                }

            }
        }

        public string segmento(string codigo)
        {
            cmd.CommandText = "select ctextoextra1 from admclientes where ccodigocliente = '"+codigo+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();
            string segmento = "";
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                segmento = reader["ctextoextra1"].ToString();

            }
            else
            {
                segmento = "";
            }
            sqlConnection2.Close();
            return segmento;
        }


        public double existencia(string codigo)
        {

            Double exis = 0;
            string sql = @"with periAnt as (
            select '1' as relaciona, 
            case when DATEPART(m, getdate()) = 1 then isnull(sum(CENTRADASINICIALES - CSALIDASINICIALES), 0) 
		    when DATEPART(m, getdate()) = 2 then isnull(sum(cEntradasPeriodo1 - cSalidasPeriodo1), 0)
		    when DATEPART(m, getdate()) = 3 then isnull(sum(cEntradasPeriodo2 - cSalidasPeriodo2), 0)
		    when DATEPART(m, getdate()) = 4 then isnull(sum(cEntradasPeriodo3 - cSalidasPeriodo3), 0)
		    when DATEPART(m, getdate()) = 5 then isnull(sum(cEntradasPeriodo4 - cSalidasPeriodo4), 0) 
		    when DATEPART(m, getdate()) = 6 then isnull(sum(cEntradasPeriodo5 - cSalidasPeriodo5), 0) 
		    when DATEPART(m, getdate()) = 7 then isnull(sum(cEntradasPeriodo6 - cSalidasPeriodo6), 0) 
		    when DATEPART(m, getdate()) = 8 then isnull(sum(cEntradasPeriodo7 - cSalidasPeriodo7), 0) 
		    when DATEPART(m, getdate()) = 9 then isnull(sum(cEntradasPeriodo8 - cSalidasPeriodo8), 0) 
		    when DATEPART(m, getdate()) = 10 then isnull(sum(cEntradasPeriodo9 - cSalidasPeriodo9), 0)
		    when DATEPART(m, getdate()) = 11 then isnull(sum
            (cEntradasPeriodo10 - cSalidasPeriodo10), 0) 
		    when DATEPART(m, getdate()) = 12 then isnull(sum(cEntradasPeriodo11 - cSalidasPeriodo11), 0) 

            end as cUnidadesAcumulado
            from admExistenciaCosto
            inner join admProductos on admExistenciaCosto.CIDPRODUCTO = admProductos.CIDPRODUCTO
            inner
            join ReportesAMSA.dbo.folios on folios.idalmacen = admExistenciaCosto.CIDALMACEN
             where CCODIGOPRODUCTO = @codigo and cIdEjercicio = @ejercicio and cTipoExistencia = 1
            and sucursal = @sucursal
            ),
            
            movs as (
            select isnull(sum( case when cAfectaExistencia = 1 then cUnidades
            when cAfectaExistencia = 2 then 0 - cUnidades else 0 end), 0)
            as cUnidadesMovto, '1' as relaciona from admMovimientos
            inner join admProductos on admMovimientos.CIDPRODUCTO = admProductos.CIDPRODUCTO
            inner
                                                join ReportesAMSA.dbo.folios on folios.idalmacen = admMovimientos.CIDALMACEN
            where (CCODIGOPRODUCTO = @codigo)
            and(cAfectadoInventario = 1 or cAfectadoInventario = 2)
            and(cFecha >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and cFecha <= GETDATE()) and(sucursal = @sucursal))
            
            select 
			isnull(cUnidadesAcumulado,0) + isnull(cUnidadesMovto,0)  as existencia from periAnt
            inner join movs on periAnt.relaciona = movs.relaciona
            ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@codigo", codigo); 
            cmd.Parameters.AddWithValue("@sucursal", comboBox1.Text);
            cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);

            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                exis = Double.Parse(reader["existencia"].ToString());

            }
            sqlConnection2.Close();

            return exis;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (valida())
            {
               
                
                if (idDocumentoText.Text.Length == 0)
                {
                    idDocumentoText.Text = ObtenIDDoc();

                    PantallaProductos.idDocumentoPasa = idDocumentoText.Text;
                    obtenultimofolio();
                    if (guardar())
                    {
                        // MessageBox.Show("Ok");
                        TipoText.Text = "2";
                        textFolio.Text = ultFol;
                        if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
                        {
                            CodigoPasa = "Nada";
                            
                            PantallaProductos.Cancelaste = "0";
                            PantallaProductos.sucursalViene = comboBox1.Text;
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value != null)
                            {
                                CodigoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                                ObservaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value.ToString();
                                CantidadPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString();
                                MovPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdMov"].Value.ToString();
                                DescripcionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();

                                using (PantallaProductos pp = new PantallaProductos())
                                {
                                    pp.ShowDialog();
                                }

                                if (PantallaProductos.Descarto == 1)
                                {
                                    try
                                    {
                                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                                        // METER METODO DE ELIMINAR AQUI
                                        calculaTotales();

                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                                if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                                {
                                  
                                        //dataGridView1.Rows.Add();
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = PantallaProductos.codigo;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = PantallaProductos.DescripcionPasa;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa;

                                        if (PantallaProductos.codigo.Substring(0, 1) == "X")
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "0";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                                        }
                                        else
                                        {
                                            if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "1.5";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento1Val)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio2val), 4);

                                            }
                                            else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "3";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento2Val)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio3val), 4);
                                            }
                                            else if (comboBoxTipoDePago.Text.Equals("Empleados"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.DescuentoCliente)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio6val), 4);
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "0";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                                            }


                                        }
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso1"].Value = PantallaProductos.peso;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";

                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;

                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);

                                        // METER METODO DE ACTUALIZAR AQUI

                                        calculaTotales();

                                        if (guardarMov(PantallaProductos.id_MovimientoPasaCompo, PantallaProductos.idPro, PantallaProductos.codigo, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString(), PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa, PantallaProductos.DescripcionPasa))
                                        {
                                            //MessageBox.Show("Si guardo");
                                        }
                                    }
                            }

                            else
                            {
                                

                                while (PantallaProductos.Cancelaste != "1")
                                {
                                    PantallaProductos.idDocumentoPasa = idDocumentoText.Text;
                                    using (PantallaProductos pp = new PantallaProductos())
                                    {
                                        pp.ShowDialog();
                                    }

                                    //AQUI INSERTAAAA
                                    if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                                    {
                                        dataGridView1.Rows.Add();

                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["IdPro"].Value = PantallaProductos.idPro;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["IdMov"].Value = PantallaProductos.id_MovimientoPasaCompo;

                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value = PantallaProductos.codigo;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value = PantallaProductos.DescripcionPasa;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Observa"].Value = PantallaProductos.ObservaPasa;

                                        if (PantallaProductos.codigo.Substring(0, 1) == "X")
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                                        }
                                        else
                                        {
                                            if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "1.5";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento1Val)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio2val), 4);

                                            }
                                            else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento2Val)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio3val), 4);

                                            }
                                            else if (comboBoxTipoDePago.Text.Equals("Empleados"))
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.DescuentoCliente)), 4).ToString();
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio6val), 4);
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                                            }



                                        }
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso1"].Value = PantallaProductos.peso;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                                        // METER METODO DE INSERTAR AQUI



                                        if (guardarMov(PantallaProductos.id_MovimientoPasaCompo, PantallaProductos.idPro, PantallaProductos.codigo, dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString(), PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa, PantallaProductos.DescripcionPasa))
                                        {
                                           // MessageBox.Show("Si guardo");


                                        }


                                        calculaTotales();

                                        //string id_Movimiento = ObtenIDMov();

                                        //guardarMov(id_Movimiento, PantallaProductos.idPro, PantallaProductos.codigo, PantallaProductos.precio, PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa);

                                        //foreach (var Valor in PantallaProductos.Componentes)
                                        //{

                                        //}

                                    }
                                }
                            }
                        }
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
                        {
                            try
                            {
                                if (DeleteProducto(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdMov"].Value.ToString()))
                                {
                                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                                    // METER METODO DE ELIMINAR AQUI
                                    calculaTotales();
                                }
                                

                            }
                            catch (Exception)
                            {

                            }
                        }

                    }

                }
                else
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
                    {
                        PantallaProductos.idDocumentoPasa = idDocumentoText.Text;
                        CodigoPasa = "Nada";



                        PantallaProductos.Cancelaste = "0";
                        PantallaProductos.sucursalViene = comboBox1.Text;
                        if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value != null)
                        {
                            CodigoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                            ObservaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value.ToString();
                            CantidadPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString();
                            MovPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdMov"].Value.ToString();
                            DescripcionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                            using (PantallaProductos pp = new PantallaProductos())
                            {
                                pp.ShowDialog();
                            }

                            if (PantallaProductos.Descarto == 1)
                            {
                                try
                                {
                                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                                    // METER METODO DE ELIMINAR AQUI
                                    calculaTotales();

                                }
                                catch (Exception)
                                {

                                }
                            }


                            if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                            {
                               
                                    //dataGridView1.Rows.Add();
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = PantallaProductos.codigo;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = PantallaProductos.DescripcionPasa;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa;

                                    if (PantallaProductos.codigo.Substring(0, 1) == "X")
                                    {
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "0";
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                                    }
                                    else
                                    {
                                        if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "1.5";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento1Val)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio2val), 4);

                                        }
                                        else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "3";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento2Val)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio3val), 4);
                                        }
                                        else if (comboBoxTipoDePago.Text.Equals("Empleados"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.DescuentoCliente)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio6val), 4);
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "0";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                                        }


                                    }
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso1"].Value = PantallaProductos.peso;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";

                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;

                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);

                                    // METER METODO DE ACTUALIZAR AQUI

                                    calculaTotales();

                                    if (guardarMov(PantallaProductos.id_MovimientoPasaCompo, PantallaProductos.idPro, PantallaProductos.codigo, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString(), PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa, PantallaProductos.DescripcionPasa))
                                    {
                                       // MessageBox.Show("Si guardo");
                                    }
                                
                            }



                        }

                        else
                        {
                            //AQUI INSERTAAAA

                            while (PantallaProductos.Cancelaste != "1")
                            {
                                PantallaProductos.idDocumentoPasa = idDocumentoText.Text;
                                using (PantallaProductos pp = new PantallaProductos())
                                {
                                    pp.ShowDialog();
                                }
                                if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                                {
                                    dataGridView1.Rows.Add();

                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["IdPro"].Value = PantallaProductos.idPro;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["IdMov"].Value = PantallaProductos.id_MovimientoPasaCompo;

                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value = PantallaProductos.codigo;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value = PantallaProductos.DescripcionPasa;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Observa"].Value = PantallaProductos.ObservaPasa;

                                    if (PantallaProductos.codigo.Substring(0, 1) == "X")
                                    {
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                                    }
                                    else
                                    {
                                        if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "1.5";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento1Val)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio2val), 4);

                                        }
                                        else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.descuento2Val)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio3val), 4);

                                        }
                                        else if (comboBoxTipoDePago.Text.Equals("Empleados"))
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "3";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(float.Parse(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round((Convert.ToDouble(PantallaProductos.DescuentoCliente)), 4).ToString();
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio6val), 4);
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                                        }



                                    }
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso1"].Value = PantallaProductos.peso;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                    dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);

                                    // METER METODO DE INSERTAR AQUI
                                    //guardarMov(ObtenIDMov(), PantallaProductos.idPro, PantallaProductos.codigo, PantallaProductos.precio, PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa);
                                    calculaTotales();

                                    if (guardarMov(PantallaProductos.id_MovimientoPasaCompo, PantallaProductos.idPro, PantallaProductos.codigo, dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString(), PantallaProductos.CantidadEnvio, PantallaProductos.ObservaPasa, PantallaProductos.DescripcionPasa))
                                    {
                                        //MessageBox.Show("Si guardo");
                                    }
                                }
                            }
                        }
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
                    {
                        try
                        {
                            if (DeleteProducto(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdMov"].Value.ToString()))
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                                // METER METODO DE ELIMINAR AQUI
                                calculaTotales();
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }
                }

            }else
            {
                MessageBox.Show("Captura o Selecciona los campos marcados en rojo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void comboBoxTipoDePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
                {
                    //float descuento = float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) * 0.015f;
                    //comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (dataGridView1.Rows[row.Index].Cells["idPro"].Value.ToString() != "3939")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO2 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "1.5";
                                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close(); 
                        }

                        

                      calculaTotales();

                    }

                }
                else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
                {
                    //comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (dataGridView1.Rows[row.Index].Cells["idPro"].Value.ToString() != "3939")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO3 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "3";
                                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO3"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close(); 
                        }

                       


                        calculaTotales();

                    }
                }
                else if (comboBoxTipoDePago.Text.Equals("Empleados"))
                {
                    //comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (dataGridView1.Rows[row.Index].Cells["idPro"].Value.ToString() != "3939")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, (CPRECIO6/1.16) as CPRECIO6, CPRECIO6 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "3";
                                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO6"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close(); 
                        }
                        
                        calculaTotales();

                    }
                }
                else
                {
                    //comboBoxTipoDePago.Enabled = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (dataGridView1.Rows[row.Index].Cells["idPro"].Value.ToString() != "3939")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, (CPRECIO6/1.16) as CPRECIO6, CPRECIO6 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
                                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close(); 
                        }

                        calculaTotales();

                    }
                }
            }
            catch (Exception)
            {
                sqlConnection2.Close();

            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 3) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            //{
            //    e.Handled = true;
            //}

            // allowed numeric and one dot  ex. 10.23
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                 && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void calculaTotales()
        {
            double subtotal = 0;
            double unidades = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Importe"].Value != null)
                {
                    subtotal += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Importe"].Value.ToString());
                    unidades += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString());
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
                    unidades = unidades;
                    //}
                }
            }

            Unidades.Text = unidades.ToString();
            Subtotal.Text = Math.Round(subtotal, 2).ToString();
            // textBoxTotal.Text = subtotal.ToString();

            Double iva = Convert.ToDouble(Subtotal.Text.ToString()) * 0.16f;
            //textBoxSubTotal.Text = iva.ToString();
            ivaText.Text = Math.Round(iva, 2).ToString();
            totalText.Text = Math.Round(subtotal + iva, 2).ToString();
            //textBoxIVA.Text = (float.Parse(textBoxTotal.Text) - float.Parse(textBoxSubTotal.Text)).ToString();

        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()),4);
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 4);
                calculaTotales();
            }
            catch (Exception)
            {


            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd.CommandText = "select idalmacen from folios where sucursal = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                textSerie.Text = reader["idalmacen"].ToString();
            }
            sqlConnection1.Close();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            limpiarCliente();
            using (AltaClienteLoc ac = new AltaClienteLoc())
            {
                ac.ShowDialog();
            }
            if (AltaClienteLoc.codigocli != "-")
            {
                eslocal = "1";
                cargarClienteLOC(AltaClienteLoc.codigocli);
            }
            //CodigoCli.Enabled = false;
            //NombreCli.Enabled = true;
            //direccion.Enabled = true;
            //Colonia.Enabled = true;
            //Telefono.Enabled = true;
            //Correo.Enabled = true;
            //Ciudad.Enabled = true;
            //Estado.Enabled = true;
            //Rfc.Enabled = true;
            //Numero.Enabled = true;
            //Cp.Enabled = true;
            //Pais.Enabled = true;
        }

        private void obtemSiguientecteloc()
        {
            cmd.CommandText = "select top 1 id_ctelocal + 1 as siguiente, 'CTLOC-'+CAST(id_ctelocal + 1 as nvarchar)  as cod from ctelocal order by id_ctelocal desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                cteloc = reader["cod"].ToString();
                idloc = reader["siguiente"].ToString();

            }
            sqlConnection1.Close();
        }

        private void insertaLocal()
        {
            try
            {
                string sql = "insert into ctelocal values (@id, @nombre, @rfc, @direccion, @numero, @telefono, @colonia, @cp, @email, @pais, @ciudad, @estado, @codigoCliente)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@id", idloc);
                cmd.Parameters.AddWithValue("@nombre", NombreCli.Text);
                cmd.Parameters.AddWithValue("@rfc", Rfc.Text);
                cmd.Parameters.AddWithValue("@direccion", direccion.Text);
                cmd.Parameters.AddWithValue("@numero", Numero.Text);
                cmd.Parameters.AddWithValue("@telefono", Telefono.Text);
                cmd.Parameters.AddWithValue("@colonia", Colonia.Text);
                cmd.Parameters.AddWithValue("@cp", Cp.Text);
                cmd.Parameters.AddWithValue("@email", Correo.Text);
                cmd.Parameters.AddWithValue("@pais", Pais.Text);
                cmd.Parameters.AddWithValue("@ciudad", Ciudad.Text);
                cmd.Parameters.AddWithValue("@estado", Estado.Text);
                cmd.Parameters.AddWithValue("@codigoCliente", cteloc);


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Guardado");
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;

                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }
        
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (idDocumentoText.Text.Length != 0)
            {
                if (existe(idDocumentoText.Text, "0"))
                {
                    DialogResult result = MessageBox.Show("La Orden no se guardo\n¿Desea Descartar la Orden?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (DeleteCotizacion(idDocumentoText.Text))
                        {

                            AbrirCargar();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        
                    }

                }
                else
                {
                    AbrirCargar();
                }
            }
            else
            {
                //Abrir
                AbrirCargar();
                
            }
            
        }


        private void AbrirCargar()
        {
            sucursal = comboBox1.Text;
            CargaCotizacion.estatus = "3";
            using (CargaCotizacion cc = new CargaCotizacion())
            {
                cc.ShowDialog();
            }

            if (CargaCotizacion.id_DocumentoPasa != "")
            {
                idDocumentoText.Text = CargaCotizacion.id_DocumentoPasa;
                TipoText.Text = CargaCotizacion.tipoDocu;
                if (TipoText.Text == "1")
                {
                    comboBoxTipoDePago.Enabled = false;
                    materialRaisedButton3.Enabled = false;
                }
                else
                {
                    comboBoxTipoDePago.Enabled = true;
                    materialRaisedButton3.Enabled = true;
                }
                //comboBoxTipoDePago.Enabled = true;
                //comboBoxTipoDePago.Text = "";
                dataGridView1.Rows.Clear();
                textFolio.Text = CargaCotizacion.FolioOTPasa;
                FolioSolo.Text = CargaCotizacion.FolioOTPasa;
                textFecha.Text = DateTime.Parse(CargaCotizacion.date1).ToString("dd/MM/yyyy");
                if (CargaCotizacion.Local == "1")
                {
                    eslocal = "1";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarClienteLOC(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(CargaCotizacion.foliocot);
                    cargarDatos();
                    cargaDatosEnv(CargaCotizacion.foliocot);
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    comboBoxTipoDePago.Text = CargaCotizacion.ListaPasa;
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarCliente(CargaCotizacion.ClienteCodigo);
                    comboBox2.Text = segmento(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(idDocumentoText.Text);
                    cargarDatos();
                    cargaDatosEnv(idDocumentoText.Text);
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);

                    if (CargaCotizacion.estatuspasa == "4")
                    {
                        button8.Enabled = false;
                        button1.Enabled = false;
                    }
                    else
                    {
                        button8.Enabled = true;
                        button1.Enabled = true;
                    }
                }
            }
        }


        private void cargaDatosEnv(string folio)
        {
            
            cmd.CommandText = "select * from CotizacionesOT where id_documento = '" + folio + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    
                    idDir.Text = reader["idDireccion"].ToString();
                    idEnvio.Text = reader["idEnvio"].ToString();
                    comboBox4.Text = reader["Prioridad"].ToString();
                    comboBox3.SelectedValue = reader["TipoCliente"].ToString();
                    comboBox5.Text = reader["Provee_Material"].ToString();
                    

                }
            }
            catch (Exception)
            {


            }
            sqlConnection3.Close();
        }

 

        private void cargaCotiza(string folio)
        {
            cont = 0;
            cmd.CommandText = "select id_mov, idProducto, CodigoProducto, Cantidad, Observaciones, precio, (Precio * Cantidad) as Importe, DescripcionPro from CotizacionesDetallesOT where id_documento = '" + folio + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells["idMov"].Value = reader["id_mov"].ToString();
                    dataGridView1.Rows[cont].Cells["idPro"].Value = reader["idProducto"].ToString();

                    dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["CodigoProducto"].ToString();

                    dataGridView1.Rows[cont].Cells["Nombre"].Value = reader["DescripcionPro"].ToString();
                    dataGridView1.Rows[cont].Cells["Precio"].Value = reader["Precio"].ToString();
                    dataGridView1.Rows[cont].Cells["PrecioDesc"].Value = reader["Precio"].ToString();
                    dataGridView1.Rows[cont].Cells["PrecioCapturado"].Value = reader["Precio"].ToString();
                    dataGridView1.Rows[cont].Cells["Importe"].Value = reader["Importe"].ToString();


                    dataGridView1.Rows[cont].Cells["Cantidad"].Value = reader["Cantidad"].ToString();
                    dataGridView1.Rows[cont].Cells["Observa"].Value = reader["Observaciones"].ToString();
                    dataGridView1.Rows[cont].Cells["Eliminar"].Value = "X";
                    
                    cont++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection3.Close();
        }


       

        private void cargarDatos()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    cmd.CommandText = "select CNOMBREPRODUCTO, CPRECIO1 / 1.16 as precio, CPRECIO10, CPRECIO1  from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        
                        //row.Cells["Exis"].Value = existencia(row.Cells["Codigo"].Value.ToString());

                        if (row.Cells["IdPro"].Value.ToString() != "3939")
                        {
                            row.Cells["Nombre"].Value = reader["CNOMBREPRODUCTO"].ToString();
                            row.Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                            row.Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                            row.Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                            row.Cells["Importe"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4) * Convert.ToDouble(row.Cells["Cantidad"].Value);
                        }

                        row.Cells["Peso1"].Value = reader["CPRECIO10"].ToString();
                        row.Cells["Descuento"].Value = "0";
                        
                       
                        
                    }
                    sqlConnection2.Close();
                }
                catch (NullReferenceException)
                {


                }

            }


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    row.Cells["Exis"].Value = existencia(row.Cells["Codigo"].Value.ToString());
                    row.Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                    row.Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                }
                catch (NullReferenceException)
                {


                }

            }
            }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            sucursal = comboBox1.Text;
            using (CargaCotizacion cc = new CargaCotizacion())
            {
                cc.ShowDialog();
            }

            if (CargaCotizacion.foliocot != "")
            {
                comboBoxTipoDePago.Enabled = true;
                comboBoxTipoDePago.Text = "";
                dataGridView1.Rows.Clear();
                textFolio.Text = CargaCotizacion.foliocot;
                textFecha.Text = DateTime.Parse(CargaCotizacion.date1).ToString("dd/MM/yyyy");
                if (CargaCotizacion.Local == "1")
                {
                    eslocal = "1";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarClienteLOC(CargaCotizacion.ClienteCodigo);
                    //cargaCotizaORI(CargaCotizacion.foliocot);
                    //cargaDatosEnvOri(CargaCotizacion.foliocot);
                    //cargarDatos();
                    calculaTotales();
                    button5.Enabled = false;
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarCliente(CargaCotizacion.ClienteCodigo);
                    //cargaCotizaORI(CargaCotizacion.foliocot);
                    //cargaDatosEnvOri(CargaCotizacion.foliocot);
                    //cargarDatos();
                    calculaTotales();
                    button5.Enabled = false;
                }
            }
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            if (idDocumentoText.Text.Length != 0)
            {
                if (existe(idDocumentoText.Text, "0"))
                {
                    DialogResult result = MessageBox.Show("La Orden no se guardo\n¿Desea Descartar la Orden?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (DeleteCotizacion(idDocumentoText.Text))
                        {
                            permiteCerrar = 1;
                            this.Close();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        permiteCerrar = 0;
                    }
                }
                else
                {
                    permiteCerrar = 1;
                    this.Close();
                }
            }
            else
            {
                permiteCerrar = 1;
                this.Close();
            }



        }

      



        private void actualiza(string id, string estatus, string folioOT)
        {
            continua = 0;
            try
            {
                string sql = @"update cotizacionesOT set codcliente = @param1, esLocal = @param2, observaciones = @Obs, fecha= @Fecha, idCliente = @idCliente,
                       idDireccion = @idDireccion, idEnvio = @idEnvio, importe = @Importe, Estatus = @Estatus, prioridad = @Prioridad, TipoCliente = @TipoCliente, Notas_Internas = @Notas_internas, Atencion = @Atencion, Solicito = @Solicito,
                Fecha_Entrega = @FechaEntrega, Provee_Material = @Prov, Autoriza3 = @Autoriza1, Usuario_Autoriza3 = @UA1, FolioOT = @FolioOT, lista_precios = @Lista
                where id_documento = @param3";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection3);
                cmd.Parameters.AddWithValue("@param1", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@param2", eslocal);
                cmd.Parameters.AddWithValue("@param3", id);
                cmd.Parameters.AddWithValue("@Obs", textBox1.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@idCliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@idDireccion", idDir.Text);
                cmd.Parameters.AddWithValue("@idEnvio", idEnvio.Text);
                cmd.Parameters.AddWithValue("@Importe", totalText.Text);
                cmd.Parameters.AddWithValue("@Prioridad", comboBox4.Text);
                cmd.Parameters.AddWithValue("@TipoCliente", comboBox3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Notas_internas", NIText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega", EntregaFec.Value.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@Prov", comboBox5.Text);
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Autoriza1", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@UA1", Principal.Variablescompartidas.usuario);
                cmd.Parameters.AddWithValue("@FolioOT", folioOT);
                cmd.Parameters.AddWithValue("@Atencion", Atencion.Text);
                cmd.Parameters.AddWithValue("@Solicito", Solicito.Text);

                cmd.Parameters.AddWithValue("@Lista", comboBoxTipoDePago.Text);

                sqlConnection3.Open();
                cmd.ExecuteNonQuery();
                sqlConnection3.Close();
                continua = 1;
                MessageBox.Show("Orden de Trabajo Guardada");
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                sqlConnection1.Close();
                MessageBox.Show(err.ToString(), "Error al Actualizar Encabezado");
            }
        }




        private void actualiza2(string id, string estatus, string folioOT)
        {
            continua = 0;
            try
            {
                string sql = @"update cotizacionesOT set codcliente = @param1, esLocal = @param2, observaciones = @Obs, fecha= @Fecha, idCliente = @idCliente,
                       idDireccion = @idDireccion, idEnvio = @idEnvio, importe = @Importe, Estatus = @Estatus, prioridad = @Prioridad, TipoCliente = @TipoCliente, Notas_Internas = @Notas_internas, Atencion = @Atencion, Solicito = @Solicito,
                Fecha_Entrega = @FechaEntrega, Provee_Material = @Prov, FolioOT = @FolioOT, tipo_Doc = '2', lista_precios = @Lista
                where id_documento = @param3";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection3);
                cmd.Parameters.AddWithValue("@param1", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@param2", eslocal);
                cmd.Parameters.AddWithValue("@param3", id);
                cmd.Parameters.AddWithValue("@Obs", textBox1.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@idCliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@idDireccion", idDir.Text);
                cmd.Parameters.AddWithValue("@idEnvio", idEnvio.Text);
                cmd.Parameters.AddWithValue("@Importe", totalText.Text);
                cmd.Parameters.AddWithValue("@Prioridad", comboBox4.Text);
                cmd.Parameters.AddWithValue("@TipoCliente", comboBox3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Notas_internas", NIText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega", EntregaFec.Value.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@Prov", comboBox5.Text);
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@FolioOT", folioOT);
                cmd.Parameters.AddWithValue("@Atencion", Atencion.Text);
                cmd.Parameters.AddWithValue("@Solicito", Solicito.Text);
                cmd.Parameters.AddWithValue("@Lista", comboBoxTipoDePago.Text);

                sqlConnection3.Open();
                cmd.ExecuteNonQuery();
                sqlConnection3.Close();
                continua = 1;
                MessageBox.Show("Orden de Trabajo Guardada");
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                sqlConnection1.Close();
                MessageBox.Show(err.ToString(), "Error al Actualizar Encabezado");
            }
        }


        private bool valida()
        {
            bool validado = false;
            int errores = 0;
            errorProvider1.Clear();

            if (comboBox2.Text.Length == 0)
            {
                errores += 1;
                errorProvider1.SetError(comboBox2, "Selecciona un Segmento");
            }

            if (comboBox3.Text.Length == 0)
            {
                errores += 1;
                errorProvider1.SetError(comboBox3, "Selecciona un Tipo de Cliente");
            }

            if (comboBox4.Text.Length == 0)
            {
                errores += 1;
                errorProvider1.SetError(comboBox4, "Selecciona una Prioridad");
            }

            if (CodigoCli.Text.Length == 0)
            {
                errores += 1;
                errorProvider1.SetError(CodigoCli, "Selecciona una Prioridad");
            }

            if (errores >= 1)
            {
                validado = false;
                return validado;
            }
            else
            {
                validado = true;
                return validado;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (idDocumentoText.Text.Length != 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (TipoText.Text == "1")
                    {
                        obtenultimofolio();
                        actualiza2(idDocumentoText.Text, "3", ultFol);
                        textFolio.Text = ultFol;
                        FolioSolo.Text = ultFol;
                        TipoText.Text = "2";
                        
                        if (updateCliente())
                        {
                            MessageBox.Show("Cliente Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        actualiza(idDocumentoText.Text, "3", textFolio.Text);
                        FolioSolo.Text = textFolio.Text;
                        if (updateCliente())
                        {
                            MessageBox.Show("Cliente Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                   
                }
                else
                {
                    MessageBox.Show("No se ha generado agregado ningun producto");
                }
            }
            else
            {
                MessageBox.Show("No se ha generado ninguna Cotizacion");
            }
            

            //if (!string.IsNullOrEmpty(Correo.Text))
            //{
            //    if (!string.IsNullOrEmpty(comboBox2.Text))
            //    {
            //        if (string.IsNullOrEmpty(CodigoCli.Text))
            //        {
            //            MessageBox.Show("Selecciona un Cliente", "AVISO");

            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(textFolio.Text))
            //            {
            //                using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa))
            //                {
            //                    string query = "select count(*) from cotizacionesdetallesOT where FolioCotizacion =@Id";
            //                    SqlCommand cmd = new SqlCommand(query, conn);
            //                    cmd.Parameters.AddWithValue("Id", textFolio.Text);
            //                    conn.Open();

            //                    int count = Convert.ToInt32(cmd.ExecuteScalar());
            //                    if (count == 0)
            //                    {
            //                        obtenultimofolio();
            //                        GuardaCotizacion();
            //                        if (continua == 1)
            //                        {
            //                            GuardaDetalles();
            //                        }
            //                        if (continua == 1)
            //                        {
            //                            GuardaOriginal();
            //                            if (continua == 1)
            //                            {
            //                                MessageBox.Show("Guardado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                                if (updateCliente())
            //                                {
            //                                    MessageBox.Show("Cliente Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                                }
            //                            }
            //                        }
            //                        textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
            //                    }

            //                    else
            //                    {
            //                        DialogResult result = MessageBox.Show("Ya existe una cotización con este folio \n ¿Desea Actualizarla?", "ACTUALIZAR COTIZACIÓN", MessageBoxButtons.YesNoCancel);

            //                        if (result == DialogResult.Yes)
            //                        {
            //                            elimina();
            //                            GuardaDetalles();
            //                            if (continua == 1)
            //                            {
            //                                actualiza();
            //                                if (continua == 1)
            //                                {
            //                                    MessageBox.Show("Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                                    if (updateCliente())
            //                                    {
            //                                        MessageBox.Show("Cliente Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                                    }
            //                                }
            //                            }

            //                        }
            //                        else if (result == DialogResult.No)
            //                        {
            //                        }

            //                    }

            //                }
            //            }
            //            else
            //            {
            //                obtenultimofolio();
            //                textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
            //                GuardaCotizacion();
            //                if (continua == 1)
            //                {
            //                    GuardaDetalles();
            //                }
            //                if (continua == 1)
            //                {
            //                    GuardaOriginal();
            //                    if (continua == 1)
            //                    {
            //                        MessageBox.Show("Guardado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                        if (updateCliente())
            //                        {
            //                            MessageBox.Show("Cliente Actualizado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                        }
            //                    }
            //                }
            //                textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
            //                FolioSolo.Text = ultFol;
            //            }


            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("SELECCIONA UN SEGMENTO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("AGREGA UN CORREO ELECTRONICO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        public bool updateCliente()
        {
            try
            {
                string sql = @"update admClientes set CEMAIL1 = @email, CTEXTOEXTRA1 = @Segmento
                where CCODIGOCLIENTE = @Codigo";

                SqlCommand cmd = new SqlCommand(sql, Principal.Variablescompartidas.AcerosConnection);
                cmd.Parameters.AddWithValue("@email", Correo.Text);
                cmd.Parameters.AddWithValue("@Segmento", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Codigo", CodigoCli.Text);

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("OCURRIO UN ERROR AL GUARDAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }


        private void obtenultimofolio()
        {
            cmd.CommandText = "select top 1 FolioOT+1 as folio from cotizacionesOT  order by FolioOT desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Close();
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                ultFol = reader["folio"].ToString();

            }
            sqlConnection3.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idDocumentoText.Text.Length != 0)
            {
                if (existe(idDocumentoText.Text, "4"))
                {
                    idImpresion = idDocumentoText.Text;
                    using (ImpOrden IOr = new ImpOrden())
                    {
                        IOr.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Esta orden no ha sido liberada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }





            

            //if (!string.IsNullOrEmpty(textFolio.Text))
            //{
            //    cmd.CommandText = "select sucnom, direccion, colonia, lugar, telefono, celular, nombre, email from datger where sucursal = '" + comboBox1.Text + "'";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = Principal.Variablescompartidas.RepAmsaConnection;
            //    Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
            //    reader = cmd.ExecuteReader();


            //    // Data is accessible through the DataReader object here.
            //    if (reader.Read())
            //    {
            //        infogerente.sucursal = reader["sucnom"].ToString();
            //        infogerente.direccion = reader["direccion"].ToString();
            //        infogerente.colonia = reader["colonia"].ToString();
            //        infogerente.lugar = reader["lugar"].ToString();
            //        infogerente.telefono = reader["telefono"].ToString();
            //        infogerente.celular = reader["celular"].ToString();

            //        infogerente.email = reader["email"].ToString();
            //    }
            //    Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);


            //    infogerente.subtotal = Subtotal.Text;
            //    infogerente.iva = ivaText.Text;
            //    infogerente.total = totalText.Text;
            //    infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper() ;

            //    VariablesCompartidas.Folio = textFolio.Text;
            //    VariablesCompartidas.Fecha = textFecha.Text;
            //    VariablesCompartidas.Cliente = NombreCli.Text;
            //    VariablesCompartidas.Telefono = Telefono.Text;
            //    VariablesCompartidas.Direccion = direccion.Text;
            //    VariablesCompartidas.Email = Correo.Text;
            //    VariablesCompartidas.Atencion = Atencion.Text;
            //    VariablesCompartidas.Solicito = Solicito.Text;
            //    VariablesCompartidas.Textos = textBox1.Text;
            //    VariablesCompartidas.SerieImp = textSerie.Text;
            //    using (ImprimeCot RC = new ImprimeCot(dataGridView1))
            //    {
            //        RC.ShowDialog();
            //    }
            //}else
            //{
            //    MessageBox.Show("Asegurate de guardar la cotización antes de imprimir", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Se borrara todo lo capturado \n ¿Desea Continuar?", "LIMPIAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                if (idDocumentoText.Text.Length != 0)
                {
                    if (existe(idDocumentoText.Text, "0"))
                    {
                        DialogResult result2 = MessageBox.Show("La cotización no se guardo\n¿Desea Descartar la cotización?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result2 == DialogResult.Yes)
                        {
                            if (DeleteCotizacion(idDocumentoText.Text))
                            {
                                PantallaProductos.Cancelaste = "0";
                                ClearTextBoxes();
                                dataGridView1.Rows.Clear();
                                comboBoxTipoDePago.Text = "Efectivo";
                                comboBox2.Text = "";
                                comboBoxTipoDePago.Enabled = true;
                                textFecha.Text = DateTime.Now.ToShortDateString();
                                EntregaFec.Value = DateTime.Now;
                                cont = 0;
                                continua = 0;
                                cteloc = "";
                                idloc = "";
                                eslocal = "";
                                ultFol = "";
                                button5.Enabled = true;

                                cmd.CommandText = "select idalmacen from folios where sucursal = '" + comboBox1.Text + "'";
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = sqlConnection1;
                                sqlConnection1.Close();
                                sqlConnection1.Open();
                                reader = cmd.ExecuteReader();

                                // Data is accessible through the DataReader object here.
                                if (reader.Read())
                                {
                                    textSerie.Text = reader["idalmacen"].ToString();
                                }
                                sqlConnection1.Close();
                            }
                        }
                        else if (result2 == DialogResult.No)
                        {
                           
                        }
                    }
                    else
                    {
                        PantallaProductos.Cancelaste = "0";
                        ClearTextBoxes();
                        dataGridView1.Rows.Clear();
                        comboBoxTipoDePago.Text = "Efectivo";
                        comboBox2.Text = "";
                        comboBoxTipoDePago.Enabled = true;
                        textFecha.Text = DateTime.Now.ToShortDateString();
                        cont = 0;
                        continua = 0;
                        cteloc = "";
                        idloc = "";
                        eslocal = "";
                        ultFol = "";
                        button5.Enabled = true;

                        cmd.CommandText = "select idalmacen from folios where sucursal = '" + comboBox1.Text + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Close();
                        sqlConnection1.Open();
                        reader = cmd.ExecuteReader();

                        // Data is accessible through the DataReader object here.
                        if (reader.Read())
                        {
                            textSerie.Text = reader["idalmacen"].ToString();
                        }
                        sqlConnection1.Close();
                    }
                }
                else
                {
                    PantallaProductos.Cancelaste = "0";
                    ClearTextBoxes();
                    dataGridView1.Rows.Clear();
                    comboBoxTipoDePago.Text = "Efectivo";
                    comboBox2.Text = "";
                    comboBoxTipoDePago.Enabled = true;
                    textFecha.Text = DateTime.Now.ToShortDateString();
                    cont = 0;
                    continua = 0;
                    cteloc = "";
                    idloc = "";
                    eslocal = "";
                    ultFol = "";
                    button5.Enabled = true;

                    cmd.CommandText = "select idalmacen from folios where sucursal = '" + comboBox1.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Close();
                    sqlConnection1.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        textSerie.Text = reader["idalmacen"].ToString();
                    }
                    sqlConnection1.Close();
                }

                
            }
            else if (result == DialogResult.No)
            {
            }
        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void obtenidDocumento()
        {
            cmd.CommandText = "select top 1 CIDDOCUMENTO+1 as siguiente from admDocumentos order by ciddocumento desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                idDocumento = reader["siguiente"].ToString();

            }
            sqlConnection2.Close();
        }

        private void obtenidMovimiento()
        {
            cmd.CommandText = "select top 1 CIDMOVIMIENTO+1 as siguiente from admmovimientos order by cidmovimiento desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                idMovimiento = reader["siguiente"].ToString();

            }
            sqlConnection2.Close();
        }

        private void obtenInfo(string id)
        {
            destina = "";
            noguia = "";
            mensa = "";
            cuentamensa = "";
            cajas = "";
            peso = "";

            cmd.CommandText = "select * from DatosEnvio where idEnvio = '"+id+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                destina = reader["Destinatario"].ToString();
                noguia = reader["NoGuia"].ToString();
                mensa = reader["Mensajeria"].ToString();
                cuentamensa = reader["CuentaMensajeria"].ToString();
                cajas = reader["Cajas"].ToString();
                peso = reader["Peso"].ToString();

            }
            sqlConnection1.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(textSerie.Text))
            //{
                if (!string.IsNullOrEmpty(textFolio.Text))
                {
                    if (!CodigoCli.Text.Contains("CTLOC"))
                    {
                        if (radioButton1.Checked != false || radioButton2.Checked != false)
                        {
                            obtenInfo(idEnvio.Text);
                            inserta(FolioSolo.Text, "", Unidades.Text, destina, noguia, mensa, cuentamensa, cajas, peso);
                            insertaDirecciones(idDocumento, 3, 0, direccion.Text, Numero.Text, Numero.Text, Colonia.Text, Cp.Text, Telefono.Text,
                            Correo.Text, Pais.Text, Estado.Text, Ciudad.Text, Ciudad.Text, comboBox1.Text);
                        
                            DirEnvio(idDir.Text);


                            int contadormov = 1;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                try
                                {
                                    insertaMovimiento(contadormov, idDocumento, row.Cells["codigo"].Value.ToString(), textSerie.Text, row.Cells["cantidad"].Value.ToString(), 3, 1, 0, 1, 0, row.Cells["precio"].Value.ToString(),
                                        row.Cells["preciocapturado"].Value.ToString(), row.Cells["importe"].Value.ToString(), row.Cells["cimpuesto1"].Value.ToString(),
                                        row.Cells["cdescuento1"].Value.ToString(), row.Cells["descuento"].Value.ToString(), (Convert.ToDouble(row.Cells["importe"].Value.ToString()) + Convert.ToDouble(row.Cells["Cimpuesto1"].Value.ToString())).ToString(), row.Cells["Observa"].Value.ToString());
                                

                                }
                                catch (NullReferenceException)
                                {

                                }
                                contadormov += 1;
                            }
                            
                            string datos = "Pedido " + comboBox1.Text + " " + "PPL" + " " + FolioSolo.Text;

                            guardaBitacora("Documento creado", datos, "PPL" + comboBox1.Text, "Pedido", Principal.Variablescompartidas.usuario, Principal.Variablescompartidas.nombre);
                            MessageBox.Show("Guardado en Comercial");
                        }
                        else
                        {
                            MessageBox.Show("Selecciona si es de contado o credito");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puede guardar con cliente local");
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona una cotización");
                }
            //}else
            //{
            //    MessageBox.Show("No se puede guardar, no se cuenta con Almacen");
            //}
        }

        private void insertaDirecciones(string idDocumento, int tipoCatalogo, int tipoDireccion, string nombreCalle, string numExte, string numInte,
            string colonia, string cp, string telefono1, string email, string pais, string estado, string ciudad, string municipio, string sucursal)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insDirecciones", sqlConnection2);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CIDCATALOGO ", idDocumento);
                cmd.Parameters.AddWithValue("@CTIPOCATALOGO ", tipoCatalogo);
                cmd.Parameters.AddWithValue("@CTIPODIRECCION ", tipoDireccion);
                cmd.Parameters.AddWithValue("@CNOMBRECALLE ", nombreCalle);
                cmd.Parameters.AddWithValue("@CNUMEROEXTERIOR ", numExte);
                cmd.Parameters.AddWithValue("@CNUMEROINTERIOR ", numInte);
                cmd.Parameters.AddWithValue("@CCOLONIA ", colonia);
                cmd.Parameters.AddWithValue("@CCODIGOPOSTAL ", cp);
                cmd.Parameters.AddWithValue("@CTELEFONO1 ", telefono1);
                cmd.Parameters.AddWithValue("@CTELEFONO2 ", "");
                cmd.Parameters.AddWithValue("@CTELEFONO3", "");
                cmd.Parameters.AddWithValue("@CTELEFONO4 ", "");
                cmd.Parameters.AddWithValue("@CEMAIL ", email);
                cmd.Parameters.AddWithValue("@CDIRECCIONWEB ", "");
                cmd.Parameters.AddWithValue("@CPAIS ", pais);
                cmd.Parameters.AddWithValue("@CESTADO ", estado);
                cmd.Parameters.AddWithValue("@CCIUDAD ", ciudad);
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA ", idDocumento);
                cmd.Parameters.AddWithValue("@CTIMESTAMP ", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@CMUNICIPIO ", municipio);
                cmd.Parameters.AddWithValue("@CSUCURSAL", sucursal);

                sqlConnection2.Open();
                cmd.ExecuteNonQuery();
                sqlConnection2.Close();
                error = 0;
                //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error", ex.ToString());
            }
        }

        private bool guardar()
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("InsertaCotizacionesOT", sqlConnection3);
                
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Tipo_Doc", "2");
                cmd.Parameters.AddWithValue("@FolioSuc", textSerie.Text);
                cmd.Parameters.AddWithValue("@Sucursal", comboBox1.Text);
                cmd.Parameters.AddWithValue("@FolioCotizacion", "");
                cmd.Parameters.AddWithValue("@FolioCot", "");
                cmd.Parameters.AddWithValue("@FolioOT", ultFol);
                cmd.Parameters.AddWithValue("@CodCliente", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@esLocal", "0");
                cmd.Parameters.AddWithValue("@Atencion", Atencion.Text);
                cmd.Parameters.AddWithValue("@Solicito", Solicito.Text);
                cmd.Parameters.AddWithValue("@Observaciones", textBox1.Text);
                cmd.Parameters.AddWithValue("@Notas_Internas", NIText.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(textFecha.Text).ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@idCliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@idDireccion", idDir.Text);
                cmd.Parameters.AddWithValue("@idEnvio", idEnvio.Text);
                cmd.Parameters.AddWithValue("@Importe", totalText.Text);
                cmd.Parameters.AddWithValue("@Estatus", "0");

                cmd.Parameters.AddWithValue("@Provee_Material", comboBox5.Text);
                cmd.Parameters.AddWithValue("@TipoCliente", comboBox3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Prioridad", comboBox4.Text);
                cmd.Parameters.AddWithValue("@Fecha_Entrega", EntregaFec.Value.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@Fecha_Ingreso_Produ", "");
                cmd.Parameters.AddWithValue("@Fecha_Cierre", "");
                cmd.Parameters.AddWithValue("@Folio_Anticipo", "0");
                cmd.Parameters.AddWithValue("@Autoriza1", "");
                cmd.Parameters.AddWithValue("@Autoriza2", "");
                cmd.Parameters.AddWithValue("@Autoriza3", "");
                cmd.Parameters.AddWithValue("@Autoriza4", "");
                cmd.Parameters.AddWithValue("@Autoriza5", "");
                cmd.Parameters.AddWithValue("@Autoriza6", "");
                cmd.Parameters.AddWithValue("@Autoriza7", "");
                cmd.Parameters.AddWithValue("@Autoriza8", "");
                cmd.Parameters.AddWithValue("@Autoriza9", "");
                cmd.Parameters.AddWithValue("@Autoriza10", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza1", "Master");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza2", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza3", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza4", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza5", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza6", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza7", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza8", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza9", "");
                cmd.Parameters.AddWithValue("@Usuario_Autoriza10", "");

                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }
            
        }


        private bool guardarMov(string id_Mov, string idPro, string CodigoPro, string Precio, string cantidad, string observa, string Descrip)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[CotizacionesDetallesOT]
   SET
      [FolioCotizacion] = @FolioCotizacion
      ,[idProducto] = @idProducto
      ,[CodigoProducto] = @CodigoProducto
      ,[Precio] = @Precio
      ,[Cantidad] = @Cantidad
      ,[Observaciones] = @Observaciones
      ,[DescripcionPro] = @Descrip
 WHERE id_mov = @id_mov", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                //cmd.Parameters.AddWithValue("@id_Documento", idDocumentoText.Text);
                cmd.Parameters.AddWithValue("@id_Mov", id_Mov);
                cmd.Parameters.AddWithValue("@FolioCotizacion", textFolio.Text);
                cmd.Parameters.AddWithValue("@idProducto", idPro);
                cmd.Parameters.AddWithValue("@CodigoProducto", CodigoPro);
                cmd.Parameters.AddWithValue("@Precio", Precio);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Observaciones", observa);
                cmd.Parameters.AddWithValue("@Descrip", Descrip);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }

        



        private string ObtenIDDoc()
        {
            cmd.CommandText = @"declare @id_doc int

             if ((select count(*) from CotizacionesOT) = 0)
	            begin
             set @id_doc = (select 1 as siguiente);
                     end
	            else
	            begin
             set @id_doc = (select top 1(id_documento + 1) as siguiente from CotizacionesOT order by id_Documento desc);
            end

            select @id_doc as siguiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            string idDoc = "";
            if (reader.Read())
            {
                idDoc = reader["siguiente"].ToString();

            }
            sqlConnection3.Close();

            return idDoc;
        }


        private string ObtenIDMov()
        {
            cmd.CommandText = @"declare @id_doc int

	if((select count(*) from cotizacionesDetallesOT) =0)
	begin
	set @id_doc = (select 1 as siguiente);
	end 
	else
	begin
	set @id_doc = (select top 1 (id_Mov + 1) as siguiente from cotizacionesDetallesOT order by id_Documento desc);
	end

	select @id_doc as siguiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            string idDoc = "";
            if (reader.Read())
            {
                idDoc = reader["siguiente"].ToString();

            }
            sqlConnection3.Close();

            return idDoc;
        }



        private void DirEnvio(string id)
        {
            cmd.CommandText = "select * from direcciones where ciddireccion = '"+id+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            string calle, numexte, numinte, colonia, telefono, email , estado, ciudad, textoextra, municipio, cp, paisp;
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                calle = reader["CNOMBRECALLE"].ToString();
                numexte = reader["CNUMEROEXTERIOR"].ToString();
                numinte = reader["CNUMEROINTERIOR"].ToString();
                colonia = reader["CCOLONIA"].ToString();
                telefono = reader["CTELEFONO1"].ToString();
                email = reader["CEMAIL"].ToString();
                estado = reader["CESTADO"].ToString();
                ciudad = reader["CCIUDAD"].ToString();
                textoextra = reader["CTEXTOEXTRA"].ToString();
                municipio = reader["CMUNICIPIO"].ToString();
                paisp = reader["CPAIS"].ToString();
                cp = reader["CCODIGOPOSTAL"].ToString();

                insertaDirecciones(idDocumento, 3, 1, calle, numexte, numinte, colonia, cp, telefono, email, paisp, estado, ciudad, municipio, comboBox1.Text);
            }
            else
            {
                insertaDirecciones(idDocumento, 3, 1, direccion.Text, Numero.Text, Numero.Text, Colonia.Text, Cp.Text, Telefono.Text,
                                Correo.Text, Pais.Text, Estado.Text, Ciudad.Text, Ciudad.Text, comboBox1.Text);
            }
            sqlConnection1.Close();

            
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            Correo.Enabled = true;
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idCliente.Text))
            {
                idClientePasa = idCliente.Text;

                if (string.IsNullOrEmpty(idDir.Text))
                {
                    idDireccionPasa = "0";
                }else
                {
                    idDireccionPasa = idDir.Text;
                }


                if (string.IsNullOrEmpty(idEnvio.Text))
                {
                    idEnvioPasa = "0";
                }
                else
                {
                    idEnvioPasa = idEnvio.Text;
                }

                using (DireccionEnvio DE = new DireccionEnvio())
                {
                    DE.ShowDialog();
                }

                idDir.Text = DireccionEnvio.direccionCotiza;
                idEnvio.Text = DireccionEnvio.EnvioCotiza;

            }else
            {
                MessageBox.Show("SELECCIONA UN CLIENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Correo.Text))
            {
                cmd.CommandText = "select sucnom, direccion, colonia, lugar, telefono, celular, nombre, email from datger where sucursal = '" + comboBox1.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Principal.Variablescompartidas.RepAmsaConnection;
                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
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

                    infogerente.email = reader["email"].ToString();
                }
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);


                infogerente.subtotal = Subtotal.Text;
                infogerente.iva = ivaText.Text;
                infogerente.total = totalText.Text;
                infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();

                VariablesCompartidas.Folio = textFolio.Text;
                VariablesCompartidas.Fecha = textFecha.Text;
                VariablesCompartidas.Cliente = NombreCli.Text;
                VariablesCompartidas.Telefono = Telefono.Text;
                VariablesCompartidas.Direccion = direccion.Text;
                VariablesCompartidas.Email = Correo.Text;
                VariablesCompartidas.Atencion = Atencion.Text;
                VariablesCompartidas.Solicito = Solicito.Text;
                VariablesCompartidas.Textos = textBox1.Text;
                VariablesCompartidas.SerieImp = textSerie.Text;

                exporta(dataGridView1);
                correo();
            }
            else
            {
                MessageBox.Show("Ingresa un correo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void exporta(DataGridView datagridview1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(string));
            dt.Columns.Add("cantidad", typeof(string));
            dt.Columns.Add("descuento", typeof(double));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("importe", typeof(string));
            dt.Columns.Add("ObservaPro", typeof(string));

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                dt.Rows.Add(item.Cells["Codigo"].Value
                    , item.Cells["Nombre"].Value
                    , item.Cells["PrecioDesc"].Value
                    , item.Cells["cantidad"].Value
                    , item.Cells["descuento"].Value
                    , item.Cells["Precio"].Value
                    , item.Cells["Peso1"].Value
                    , item.Cells["importe"].Value
                    , item.Cells["Observa"].Value

                    );
            }
            ds.Tables.Add(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\OrdenesOT\imprep.rpt");

            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            rd.SetParameterValue("AtencionPa", "");
            rd.SetParameterValue("SolicitoPa", "");
            rd.SetParameterValue("textos", VariablesCompartidas.Textos);

            rd.SetParameterValue("sucursal", infogerente.sucursal);
            rd.SetParameterValue("direccion", infogerente.direccion);
            rd.SetParameterValue("colonia", infogerente.colonia);
            rd.SetParameterValue("lugar", infogerente.lugar);
            rd.SetParameterValue("telefono", infogerente.telefono.Trim());
            rd.SetParameterValue("celular", infogerente.celular.Trim());
            rd.SetParameterValue("nombre", infogerente.nombre.Trim());
            rd.SetParameterValue("email", infogerente.email.Trim());

            rd.SetParameterValue("subtotal", infogerente.subtotal);
            rd.SetParameterValue("iva", infogerente.iva);
            rd.SetParameterValue("total", infogerente.total);

            //rd.SetParameterValue("Existencia", VariablesCompartidas.existencia);
            //rd.SetParameterValue("FeHOY", DateTime.Now.ToString("dd/MM/yyyy"));
            //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
            //DateTime.Now.ToString("HH:mm:ss")

            //crystalReportViewer1.ReportSource = rd;

            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf");

            //MessageBox.Show("Exported Successful");


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar == 0)
            {
               
                e.Cancel = true;
            }
            else
            {
                //this.Close();
            }
        }

        private void correo()
        {
            string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            @profile_name = 'Notifications',
            @recipients =@Recipiente,
            @subject = @Folio2, 
            @body_format = 'html',
            @body = @Folio,
            @file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            cmd.Parameters.AddWithValue("@Recipiente", Correo.Text);
            //cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx");
            cmd.Parameters.AddWithValue("@Folio", "<h1>Se envia la cotizacion: </h1>" + "<h2>"+textFolio.Text+"</h2>");
            cmd.Parameters.AddWithValue("@Folio2", textFolio.Text);


            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textFolio.Text))
            {
                string sql = "update cotizacionesOT set estatus = 'C' where foliocot = @Folio";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Folio", textFolio.Text);


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("PEDIDO CREADO CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("SELECCIONA UNA COTIZACION", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (TipoText.Text == "2")
            {
                if (idDocumentoText.Text.Length != 0)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        if (existe(idDocumentoText.Text, "3"))
                        {
                            DialogResult result = MessageBox.Show("¿Deseas Liberar esta Orden?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                if (Autoriza2(idDocumentoText.Text))
                                {

                                    obtenInfo(idEnvio.Text);

                                    if (comboBox1.Text == "PLA")
                                    {
                                        conceptoPedido = "3231";
                                        seriePedido = "PPL";
                                    }
                                    else if (comboBox1.Text == "ME ")
                                    {
                                        conceptoPedido = "3099";
                                        seriePedido = "PME";
                                    }

                                    inserta(FolioSolo.Text, "", Unidades.Text, destina, noguia, mensa, cuentamensa, cajas, peso);
                                    insertaDirecciones(idDocumento, 3, 0, direccion.Text, Numero.Text, Numero.Text, Colonia.Text, Cp.Text, Telefono.Text,
                                    Correo.Text, Pais.Text, Estado.Text, Ciudad.Text, Ciudad.Text, comboBox1.Text);

                                    DirEnvio(idDir.Text);


                                    int contadormov = 1;
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        try
                                        {
                                            insertaMovimiento(contadormov, idDocumento, row.Cells["codigo"].Value.ToString(), textSerie.Text, row.Cells["cantidad"].Value.ToString(), 3, 1, 0, 1, 0, row.Cells["precio"].Value.ToString(),
                                                row.Cells["preciocapturado"].Value.ToString(), row.Cells["importe"].Value.ToString(), row.Cells["cimpuesto1"].Value.ToString(),
                                                row.Cells["cdescuento1"].Value.ToString(), row.Cells["descuento"].Value.ToString(), (Convert.ToDouble(row.Cells["importe"].Value.ToString()) + Convert.ToDouble(row.Cells["Cimpuesto1"].Value.ToString())).ToString(), row.Cells["Observa"].Value.ToString());


                                        }
                                        catch (NullReferenceException)
                                        {

                                        }
                                        contadormov += 1;
                                    }

                                    string datos = "Pedido " + comboBox1.Text + " " + "PPL" + " " + FolioSolo.Text;

                                    guardaBitacora("Documento creado", datos, "PPL" + comboBox1.Text, "Pedido", Principal.Variablescompartidas.usuario, Principal.Variablescompartidas.nombre);
                                    MessageBox.Show("Guardado en Comercial");



                                    MessageBox.Show("La Orden ha sido liberada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    Limpiar();
                                }
                            }
                            else if (result == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("La Orden no ha sigo guardada o convertida", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }
                    else
                    {
                        MessageBox.Show("No se ha agregado agregado ningun producto");
                    }
                }
                else
                {
                    MessageBox.Show("No se ha generado ninguna Orden");
                } 
            }
            else
            {
                MessageBox.Show("No se ha convertido la Orden");
            }



        }

        private void Limpiar()
        {
            PantallaProductos.Cancelaste = "0";
            ClearTextBoxes();
            dataGridView1.Rows.Clear();
            comboBoxTipoDePago.Text = "Efectivo";
            comboBox2.Text = "";
            comboBoxTipoDePago.Enabled = true;
            textFecha.Text = DateTime.Now.ToShortDateString();
            cont = 0;
            continua = 0;
            cteloc = "";
            idloc = "";
            eslocal = "";
            ultFol = "";
            button5.Enabled = true;

            cmd.CommandText = "select idalmacen from folios where sucursal = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                textSerie.Text = reader["idalmacen"].ToString();
            }
            sqlConnection1.Close();
        }

        private void inserta(string folio, string Referencia, string unidades, string destina, string guia, string mensa, string cuenta, string cajas, string peso)
        {
            obtenidDocumento();
            error = 1;
            while (error == 1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insDocumentoPedido", sqlConnection2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CidDocumento", idDocumento);

                    cmd.Parameters.AddWithValue("@cidconcepto", conceptoPedido);
                    cmd.Parameters.AddWithValue("@serie", seriePedido);
                    

                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@Referencia", Referencia);
                    cmd.Parameters.AddWithValue("@TotalUnidades", unidades);
                    cmd.Parameters.AddWithValue("@Usuario", Principal.Variablescompartidas.usuario);
                    cmd.Parameters.AddWithValue("@tipo", "2");
                    cmd.Parameters.AddWithValue("@idcliente", CodigoCli.Text);
                    cmd.Parameters.AddWithValue("@nombrecliente", NombreCli.Text);
                    cmd.Parameters.AddWithValue("@rfccliente", Rfc.Text);
                    cmd.Parameters.AddWithValue("@usacliente", "1");
                    cmd.Parameters.AddWithValue("@cneto", Subtotal.Text);
                    cmd.Parameters.AddWithValue("@cimpuesto", ivaText.Text);
                    cmd.Parameters.AddWithValue("@total", totalText.Text);
                    cmd.Parameters.AddWithValue("@observaciones", textBox1.Text);

                    cmd.Parameters.AddWithValue("@Destinatario", destina);
                    cmd.Parameters.AddWithValue("@Guia", guia);
                    cmd.Parameters.AddWithValue("@Mensajeria", mensa);
                    cmd.Parameters.AddWithValue("@CuentaMensa", cuentamensa);
                    cmd.Parameters.AddWithValue("@Cajas", cajas);
                    cmd.Parameters.AddWithValue("@Peso", peso);




                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    error = 0;
                    //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
            }
                catch (SqlException ex)
            {
                error = 1;
                    SqlError err = ex.Errors[0];
                    MessageBox.Show(err.ToString());
                obtenidDocumento();
            }
        }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (existe(idDocumentoText.Text, "2"))
            {
                idDocumentoPasa = idDocumentoText.Text;
                Adjunta.FolioCotiza = textFolio.Text;
                using (Adjunta ad = new Adjunta())
                {
                    ad.ShowDialog();
                }

            }
            else if (existe(idDocumentoText.Text, "3"))
            {
                idDocumentoPasa = idDocumentoText.Text;
                Adjunta.FolioCotiza = textFolio.Text;
                using (Adjunta ad = new Adjunta())
                {
                    ad.ShowDialog();
                }
            }
            else if (existe(idDocumentoText.Text, "4"))
            {
                idDocumentoPasa = idDocumentoText.Text;
                Adjunta.FolioCotiza = textFolio.Text;
                using (Adjunta ad = new Adjunta())
                {
                    ad.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("La Orden no ha sido guardada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Aceptadas_Click(object sender, EventArgs e)
        {
            sucursal = comboBox1.Text;
            CargaCotizacion.estatus = "2";
            using (CargaCotizacion cc = new CargaCotizacion())
            {
                cc.ShowDialog();
            }

            if (CargaCotizacion.id_DocumentoPasa != "")
            {
                idDocumentoText.Text = CargaCotizacion.id_DocumentoPasa;
                comboBoxTipoDePago.Enabled = true;
                comboBoxTipoDePago.Text = "";
                dataGridView1.Rows.Clear();
                textFolio.Text = CargaCotizacion.foliocot;
                FolioSolo.Text = CargaCotizacion.FolioSolo;
                textFecha.Text = DateTime.Parse(CargaCotizacion.date1).ToString("dd/MM/yyyy");
                if (CargaCotizacion.Local == "1")
                {
                    eslocal = "1";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarClienteLOC(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(CargaCotizacion.foliocot);
                    cargarDatos();
                    cargaDatosEnv(CargaCotizacion.foliocot);
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarCliente(CargaCotizacion.ClienteCodigo);
                    comboBox2.Text = segmento(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(idDocumentoText.Text);
                    cargarDatos();
                    cargaDatosEnv(idDocumentoText.Text);
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);
                }
            }
        }

        private void AutorizaBut_Click(object sender, EventArgs e)
        {
            if (idDocumentoText.Text.Length !=0)
            {
                using (Contraseña co = new Contraseña())
                {
                    co.ShowDialog();
                }

                if (Contraseña.Validado != 0)
                {
                    if (Autoriza3(idDocumentoText.Text))
                    {
                        MessageBox.Show("La cotizacion ha sido autorizada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Limpiar();
                    }
                }
            }
            else
            {
                 MessageBox.Show("Selecciona una cotización", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void insertaMovimiento(int mov, string iddocumento1, String idproducto, String idalmacen, String unidades, int afecta, int afectaSaldo, int movowner, int tipoTraspaso, int Oculto,
            string precio, string preciocapturado, string neto, string impuesto1, string descuento1, string procentajedescuento, string total, string obsMov)
        {
            obtenidMovimiento();
            error = 1;
            while (error == 1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insMovimientoPedido", sqlConnection2);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cidMovimiento", idMovimiento);
                    cmd.Parameters.AddWithValue("@CidDocumento", iddocumento1);
                    cmd.Parameters.AddWithValue("@numMov", mov);
                    cmd.Parameters.AddWithValue("@idProducto", idproducto);
                    cmd.Parameters.AddWithValue("@idAlmacen", idalmacen);
                    cmd.Parameters.AddWithValue("@TotalUnidades", unidades);
                    cmd.Parameters.AddWithValue("@Afecta", afecta);
                    cmd.Parameters.AddWithValue("@AfectaSaldo", afectaSaldo);
                    cmd.Parameters.AddWithValue("@movOwner", movowner);
                    cmd.Parameters.AddWithValue("@tipoTraspaso", tipoTraspaso);
                    cmd.Parameters.AddWithValue("@Oculto", Oculto);
                    cmd.Parameters.AddWithValue("@tipo", "2");

                    cmd.Parameters.AddWithValue("@cprecio", precio);
                    cmd.Parameters.AddWithValue("@cpreciocapturado", preciocapturado);
                    cmd.Parameters.AddWithValue("@cneto", neto);
                    cmd.Parameters.AddWithValue("@cimpuesto1", impuesto1);
                    cmd.Parameters.AddWithValue("@cdescuento", descuento1);
                    cmd.Parameters.AddWithValue("@porcentajedescuento", procentajedescuento);
                    cmd.Parameters.AddWithValue("@ctotal", total);
                    cmd.Parameters.AddWithValue("@ObsMov", obsMov);



                    //                @cprecio float, @cpreciocapturado float,
                    //@cneto float, @cimpuesto1 float, @cdescuento float, @porcentajedescuento float, @ctotal float


                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    error = 0;
                    //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
                }
                catch (SqlException ex)
                {
                    error = 1;
                    obtenidMovimiento();
                    //MessageBox.Show(ex.ToString());
                }

            }
        }


        private void guardaBitacora(string proceso, string datos, string extra, string serie, string usuario, string nombre)
        {
            //       @Proceso nvarchar(100), @Datos nvarchar(100), @Extra nvarchar(100), @serie nvarchar(5),
            //@usuario nvarchar(25), @nombre nvarchar(100)

            SqlCommand cmd = new SqlCommand("InsBitacora", sqlConnection2);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Proceso", proceso);
            cmd.Parameters.AddWithValue("@Datos", datos);
            cmd.Parameters.AddWithValue("@Extra", extra);
            cmd.Parameters.AddWithValue("@serie", serie);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@nombre", nombre);


            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        

        private bool DeleteProducto(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("delete from cotizacionesDetallesOT where id_mov = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }


        private bool DeleteCotizacion(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("delete from cotizacionesOT where id_Documento = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }



        private bool Autoriza2(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand(@"Update cotizacionesOT set Autoriza4 = @Autoriza2,  Usuario_Autoriza4 = @UA2,
                  Estatus = '4', fecha_Ingreso_Produ = @Fecha  where id_Documento = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Autoriza2", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@UA2", Principal.Variablescompartidas.usuario);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }




        private bool Autoriza3(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand(@"Update cotizacionesOT set Autoriza3 = @Autoriza2,  Usuario_Autoriza3 = @UA2,
                  Estatus = '3'  where id_Documento = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Autoriza2", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@UA2", Principal.Variablescompartidas.usuario);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }



        private bool existe(string id, string estatus)
        {
            using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo))
            {
                string query = "select count(*) from cotizacionesOT where id_Documento =@Id and estatus = @estatus";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@estatus", estatus);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    return false;
                }

                else
                {
                    return true;
                }

            }
        }
    }
}