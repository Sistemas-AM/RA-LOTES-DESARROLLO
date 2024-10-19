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

namespace Cotizacion2020
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        Metodos met = new Metodos();
        int cont = 0;
        int continua = 0;
        int error = 1;
        string idDocumento;
        string idMovimiento;
        string cteloc, idloc, eslocal, ultFol;
        string letra, cotizaContado, CotizaCredito, SerieCredito;
        DataTable dt2 = new DataTable();
        public static string sucursal { get; set; }

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

            //if (Principal.Variablescompartidas.sucural == "AUDITOR")
            //{
            //    comboBox1.Enabled = true;
            //}
            //else
            //{
            //    //comboBox1.Enabled = false;
            //comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
            //comboBox1.Text = "IGS";
            //}
        }

        private void llenarComboColoniasSINFILTRO()
        {
            try
            {
                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlCommand sc = new SqlCommand("select id, Colonia from codigosPostales order by colonia", Principal.Variablescompartidas.RepAmsaConnection);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(string));

                dt.Load(reader);

                Colonia.ValueMember = "id";
                Colonia.DisplayMember = "Colonia";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
                Colonia.DataSource = dt;


            }
            catch (Exception)
            {
                Colonia.Text = "";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //            Efectivo
            //Descuento 1.5
            //Descuento 3
            //Cheque
            //Cheque Plus
            //Tarjeta
            //Empleados
            //panel1.BackColor = ColorTranslator.FromHtml("#DDE4EE");
            llenarComboColoniasSINFILTRO();
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#CBD0D3");
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button2.BackColor = ColorTranslator.FromHtml("#3FACA9");
            button3.BackColor = ColorTranslator.FromHtml("#6DADA6");
            button4.BackColor = ColorTranslator.FromHtml("#D66F6F");
            button5.BackColor = ColorTranslator.FromHtml("#CE5248");

            CargaCombo();

            if (Principal.Variablescompartidas.Perfil_id =="33")
            {
                comboBox1.Enabled = true;
            }
            else
            {
                if (Principal.Variablescompartidas.sucural == "AUDITOR")
                {
                    comboBox1.Enabled = true;
                }
                else
                {
                    comboBox1.Enabled = false;
                    comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
                }
            }

           

            //cargaTras();
        }

        

        private void CargaCombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("SELECT sucursal, ultimoFolio, letra, idalmacen FROM folios", sqlConnection1);
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
                string colonia = "";
                //---------------------------Llenar el cliente ---------------------------------
                cmd.CommandText = "select CCODIGOCLIENTE,  CRAZONSOCIAL, CRFC, CEMAIL1, CNOMBRECALLE, CNUMEROEXTERIOR, CTELEFONO1, CCOLONIA, CCODIGOPOSTAL, CPAIS, CCIUDAD, CESTADO from admClientes inner join admDomicilios on admClientes.CIDCLIENTEPROVEEDOR = admDomicilios.CIDCATALOGO  where CCODIGOCLIENTE='" + codigo + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    CodigoCli.Text = reader["CCODIGOCLIENTE"].ToString();
                    NombreCli.Text = reader["CRAZONSOCIAL"].ToString();
                    Rfc.Text = reader["CRFC"].ToString();
                    Correo.Text = reader["CEMAIL1"].ToString();
                    direccion.Text = reader["CNOMBRECALLE"].ToString();
                    Numero.Text = reader["CNUMEROEXTERIOR"].ToString();
                    Telefono.Text = reader["CTELEFONO1"].ToString();
                   
                    Cp.Text = reader["CCODIGOPOSTAL"].ToString();
                    colonia = reader["CCOLONIA"].ToString();
                    //Colonia.Text = reader["CCOLONIA"].ToString();
                    Pais.Text = reader["cpais"].ToString();
                    Ciudad.Text = reader["cciudad"].ToString();
                    Estado.Text = reader["cestado"].ToString();


                }
                sqlConnection2.Close();

                if (!string.IsNullOrEmpty(Cp.Text))
                {

                    llenarComboColonias(Cp.Text);

                }
                Colonia.Text = colonia;
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
            Colonia.Text = "";
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
            cmd.CommandText = "select CBANVENTACREDITO from admClientes where CCODIGOCLIENTE = '"+codigo+"'";
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
                //direccion.Enabled = false;
                //Colonia.Enabled = false;
                //Telefono.Enabled = false;
                //Correo.Enabled = false;
                //Ciudad.Enabled = false;
                //Estado.Enabled = false;
                Rfc.Enabled = false;
                //Numero.Enabled = false;
                //Cp.Enabled = false;
                //Pais.Enabled = false;
                CodigoCli.Text = PantallaClientes.codigo;
                NombreCli.Text = PantallaClientes.Nombre;
                

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
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
            {
                PantallaProductos.Ocultamos = "0";
                PantallaProductos.Cancelaste = "0";
                PantallaProductos.sucursalViene = comboBox1.Text;
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value != null)
                {

                    using (PantallaProductos pp = new PantallaProductos())
                    {
                        pp.ShowDialog();
                    }
                    if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                    {
                        //dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = PantallaProductos.codigo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = PantallaProductos.nombre;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia(PantallaProductos.codigo);

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
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value = PantallaProductos.peso;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";

                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                        calculaTotales();
                    }



                }

                else
                {
                    while (PantallaProductos.Cancelaste != "1")
                    {
                        PantallaProductos.Ocultamos = "0";
                        using (PantallaProductos pp = new PantallaProductos())
                        {
                            pp.ShowDialog();
                        }
                        if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value = PantallaProductos.codigo;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value = PantallaProductos.nombre;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["exis"].Value = existencia(PantallaProductos.codigo);

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
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value = PantallaProductos.peso;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["importe"].Value.ToString()) * 0.16f, 2);
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                            calculaTotales();
                        } 
                    }
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
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

        private void comboBoxTipoDePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //try
            //{
            //    if (comboBoxTipoDePago.Text.Equals("Descuento 1.5"))
            //    {
            //        //float descuento = float.Parse(dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) * 0.015f;
            //        //comboBoxTipoDePago.Enabled = false;
            //        foreach (DataGridViewRow row in dataGridView1.Rows)
            //        {

            //            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO2 as descu from admProductos where CCODIGOPRODUCTO = '"+ row.Cells["Codigo"].Value.ToString() + "'";
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Connection = sqlConnection2;
            //            sqlConnection2.Open();
            //            reader = cmd.ExecuteReader();

            //            // Data is accessible through the DataReader object here.
            //            if (reader.Read())
            //            {
            //                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "1.5";
            //                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //              //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //        }
            //        sqlConnection2.Close();

            //            //if (dataGridView1.Rows[row.Index].Cells["Codigo"].Value.ToString().Substring(0, 1) != "X")
            //            //{
            //            //    dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "1.5";
            //            //    dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString()) * 0.015f, 4);
            //            //    dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());

            //            //}
            //            //else
            //            //{
            //            //    dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
            //            //    dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString());
            //            //    dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //            //}



            //          calculaTotales();

            //        }

            //    }
            //    else if (comboBoxTipoDePago.Text.Equals("Descuento 3"))
            //    {
            //        //comboBoxTipoDePago.Enabled = false;
            //        foreach (DataGridViewRow row in dataGridView1.Rows)
            //        {

            //            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO3 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Connection = sqlConnection2;
            //            sqlConnection2.Open();
            //            reader = cmd.ExecuteReader();

            //            // Data is accessible through the DataReader object here.
            //            if (reader.Read())
            //            {
            //                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "3";
            //                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO3"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //              //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //            }   
            //            sqlConnection2.Close();

            //            //if (dataGridView1.Rows[row.Index].Cells["Codigo"].Value.ToString().Substring(0, 1) != "X")
            //            //{
            //            //    dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "3";
            //            //    dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString()) * 0.03f, 4);
            //            //    dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //            //}
            //            //else
            //            //{
            //            //    dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
            //            //    dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Precio"].Value.ToString());
            //            //    dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //            //}


            //            calculaTotales();

            //        }
            //    }
            //    else if (comboBoxTipoDePago.Text.Equals("Empleados"))
            //    {
            //        //comboBoxTipoDePago.Enabled = false;
            //        foreach (DataGridViewRow row in dataGridView1.Rows)
            //        {

            //            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, (CPRECIO6/1.16) as CPRECIO6, CPRECIO6 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Connection = sqlConnection2;
            //            sqlConnection2.Open();
            //            reader = cmd.ExecuteReader();

            //            // Data is accessible through the DataReader object here.
            //            if (reader.Read())
            //            {
            //                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "3";
            //                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO6"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //            }
            //            sqlConnection2.Close();
                        
            //            calculaTotales();

            //        }
            //    }
            //    else
            //    {
            //        //comboBoxTipoDePago.Enabled = false;
            //        foreach (DataGridViewRow row in dataGridView1.Rows)
            //        {

            //            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, (CPRECIO6/1.16) as CPRECIO6, CPRECIO6 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Connection = sqlConnection2;
            //            sqlConnection2.Open();
            //            reader = cmd.ExecuteReader();

            //            // Data is accessible through the DataReader object here.
            //            if (reader.Read())
            //            {
            //                dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
            //                dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //                dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
            //                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
            //            }
            //            sqlConnection2.Close();

            //            calculaTotales();

            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    sqlConnection2.Close();

            //}
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
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()),2);
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
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
                FolioSolo.Text = CargaCotizacion.FolioSolo;
                textFecha.Text = DateTime.Parse(CargaCotizacion.date1).ToString("dd/MM/yyyy");
                if (CargaCotizacion.Local == "1")
                {
                    eslocal = "1";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarClienteLOC(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(CargaCotizacion.foliocot);
                    cargarDatos();
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarCliente(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(CargaCotizacion.foliocot);
                    cargarDatos();
                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);
                }
            }
        }

        private void cargaCotiza(string folio)
        {
            cont = 0;
            cmd.CommandText = "select CodigoProducto, Cantidad from CotizacionesDetalles where folioCotizacion = '" + folio + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["CodigoProducto"].ToString();
                    dataGridView1.Rows[cont].Cells["Cantidad"].Value = reader["Cantidad"].ToString();
                    dataGridView1.Rows[cont].Cells["Eliminar"].Value = "X";
                    
                    cont++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }


        private void cargaCotizaORI(string folio)
        {
            cont = 0;
            cmd.CommandText = "select * from cotizacionesoriginal where folioCotizacion = '" + folio + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["CodigoProducto"].ToString();
                    dataGridView1.Rows[cont].Cells["Nombre"].Value = reader["NombreProducto"].ToString();
                    dataGridView1.Rows[cont].Cells["exis"].Value = reader["Existencia"].ToString();
                    dataGridView1.Rows[cont].Cells["Precio"].Value = reader["Precio"].ToString();
                    dataGridView1.Rows[cont].Cells["Cantidad"].Value = reader["Cantidad"].ToString();
                    dataGridView1.Rows[cont].Cells["Descuento"].Value = reader["Descuento"].ToString();
                    if (reader["Descuento"].ToString() == "3")
                    {
                        comboBoxTipoDePago.Text = "Descuento 3";
                        comboBoxTipoDePago.Enabled = false;

                    }
                    else if (reader["Descuento"].ToString() == "1.5")
                    {
                        comboBoxTipoDePago.Text = "Descuento 1.5";
                        comboBoxTipoDePago.Enabled = false;
                    }
                    dataGridView1.Rows[cont].Cells["PrecioDesc"].Value = reader["PrecioDescuento"].ToString();
                    dataGridView1.Rows[cont].Cells["Peso"].Value = reader["Peso"].ToString();
                    dataGridView1.Rows[cont].Cells["Importe"].Value = reader["Importe"].ToString();
                    dataGridView1.Rows[cont].Cells["Eliminar"].Value = "X";


                    cont++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
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
                        row.Cells["Nombre"].Value = reader["CNOMBREPRODUCTO"].ToString();
                        //row.Cells["Exis"].Value = existencia(row.Cells["Codigo"].Value.ToString());
                        row.Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                        row.Cells["Peso"].Value = reader["CPRECIO10"].ToString();
                        row.Cells["Descuento"].Value = "0";
                        row.Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                        row.Cells["Importe"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4) * Convert.ToDouble(row.Cells["Cantidad"].Value);
                        row.Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
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
                    cargaCotizaORI(CargaCotizacion.foliocot);
                    //cargarDatos();
                    calculaTotales();
                    button5.Enabled = false;
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    textBox1.Text = CargaCotizacion.observaciones;
                    cargarCliente(CargaCotizacion.ClienteCodigo);
                    cargaCotizaORI(CargaCotizacion.foliocot);
                    //cargarDatos();
                    calculaTotales();
                    button5.Enabled = false;
                }
            }
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void elimina()
        {
            continua = 0;
            try
            {
                string sql = "delete from cotizacionesdetalles where folioCotizacion = @param1";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", textFolio.Text);


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                continua = 1;
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                MessageBox.Show(err.ToString(), "Error al Actualizar Encabezado");
            }
        }

        private void actualiza()
        {
            continua = 0;
            try
            {
                string sql = "update cotizaciones set codcliente = @param1, esLocal = @param2, observaciones = @Obs, fecha= @Fecha where folioCot = @param3";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@param2", eslocal);
                cmd.Parameters.AddWithValue("@param3", textFolio.Text);
                cmd.Parameters.AddWithValue("@Obs", textBox1.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha));

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                continua = 1;
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                MessageBox.Show(err.ToString(), "Error al Actualizar Encabezado");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoCli.Text))
            {
               MessageBox.Show("Selecciona un Cliente", "AVISO");

            }
            else
            {
                if (!string.IsNullOrEmpty(textFolio.Text))
                {
                    using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa))
                    {
                        string query = "select count(*) from cotizacionesdetalles where FolioCotizacion =@Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("Id", textFolio.Text);
                        conn.Open();

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            obtenultimofolio();
                            GuardaCotizacion();
                            if (continua == 1)
                            {
                                GuardaDetalles();
                            }
                            if (continua == 1)
                            {
                                GuardaOriginal();
                            }
                            textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
                        }

                        else
                        {
                            DialogResult result = MessageBox.Show("Ya existe una cotización con este folio \n ¿Desea Actualizarla?", "ACTUALIZAR COTIZACIÓN", MessageBoxButtons.YesNoCancel);

                            if (result == DialogResult.Yes)
                            {
                                elimina();
                                GuardaDetalles();
                                if (continua == 1)
                                {
                                    actualiza();
                                    if (continua == 1)
                                    {
                                        MessageBox.Show("Actualizado Correctamente!", "AVISO");
                                    }
                                }

                            }
                            else if (result == DialogResult.No)
                            {
                            }
                            
                        }

                    }
                }else
                {
                    obtenultimofolio();
                    textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
                    GuardaCotizacion();
                    if (continua == 1)
                    {
                        GuardaDetalles(); 
                    }
                    if (continua == 1)
                    {
                        GuardaOriginal();
                        if (continua == 1)
                        {
                            MessageBox.Show("Guardado Correctamente!", "AVISO");
                        }
                    }
                    textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
                    FolioSolo.Text = ultFol;
                }
                
                
            }
        }


        private void obtenultimofolio()
        {
            cmd.CommandText = "select ultimoFolio+1 as folio from folios where sucursal = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                ultFol = reader["folio"].ToString();

            }
            sqlConnection1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textFolio.Text))
            {
                cmd.CommandText = "select sucnom, direccion, colonia, lugar, telefono, celular, nombre, email from datger where sucursal = '" + comboBox1.Text + "'";
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


                infogerente.subtotal = Subtotal.Text;
                infogerente.iva = ivaText.Text;
                infogerente.total = totalText.Text;

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
                using (ImprimeCot RC = new ImprimeCot(dataGridView1))
                {
                    RC.ShowDialog();
                }
            }else
            {
                MessageBox.Show("Asegurate de guardar la cotización antes de imprimir", "AVISO");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Se borrara todo lo capturado \n ¿Desea Continuar?", "LIMPIAR", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                PantallaProductos.Cancelaste = "0";
                ClearTextBoxes();
                dataGridView1.Rows.Clear();
                comboBoxTipoDePago.Text = "";
                comboBoxTipoDePago.Enabled = true;
                textFecha.Text = DateTime.Now.ToShortDateString();
                cont = 0;
                continua = 0;
                cteloc = "";
                idloc = "";
                eslocal = "";
                ultFol = "";
                button5.Enabled = true;
                llenarComboColoniasSINFILTRO();

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

        private void obtenInfo()
        {
            cmd.CommandText = "select letra, cotizacontado, cotizaCredito, SerieCredito from folios where sucursal = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                letra = reader["letra"].ToString();
                cotizaContado = reader["cotizacontado"].ToString();
                CotizaCredito = reader["cotizaCredito"].ToString();
                SerieCredito = reader["SerieCredito"].ToString();

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
                            obtenInfo();
                            inserta(FolioSolo.Text, comboBox1.Text, Unidades.Text);

                            insertaDirecciones(idDocumento, 3, 1, direccion.Text, Numero.Text, Numero.Text, Colonia.Text, Cp.Text, Telefono.Text,
                            Correo.Text, Pais.Text, Estado.Text, Ciudad.Text, Ciudad.Text, comboBox1.Text);

                        int contadormov = 1;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                try
                                {

                                    //        insertaMovimiento(int mov, string iddocumento1, String idproducto, String idalmacen, String unidades, int afecta, int afectaSaldo, int movowner, int tipoTraspaso, int Oculto,
                                    //string precio, string preciocapturado, string neto, string impuesto1, string descuento1, string procentajedescuento, string total)
                                    insertaMovimiento(contadormov, idDocumento, row.Cells["codigo"].Value.ToString(), textSerie.Text, row.Cells["cantidad"].Value.ToString(), 3, 1, 0, 1, 0, row.Cells["precio"].Value.ToString(),
                                        row.Cells["preciocapturado"].Value.ToString(), row.Cells["importe"].Value.ToString(), row.Cells["cimpuesto1"].Value.ToString(),
                                        row.Cells["cdescuento1"].Value.ToString(), row.Cells["descuento"].Value.ToString(), (Convert.ToDouble(row.Cells["importe"].Value.ToString()) + Convert.ToDouble(row.Cells["Cimpuesto1"].Value.ToString())).ToString());

                                    //string movAnterior = idMovimiento;

                                    //insertaMovimiento(0, "0", row.Cells["idProducto"].Value.ToString(), idDestino, row.Cells["Cantidad"].Value.ToString(), 1, 0, Int32.Parse(movAnterior), 3, 1);

                                }
                                catch (NullReferenceException)
                                {

                                }
                                contadormov += 1;
                            }
                            string tipo = "";
                            string serie = "";
                            if (radioButton1.Checked)
                            {
                                tipo = "COT-";
                                serie = letra;
                            }
                            else if (radioButton2.Checked)
                            {
                                tipo = "COTC-";
                                serie = SerieCredito;
                            }

                            string datos = tipo + comboBox1.Text + " " + serie + " " + FolioSolo.Text;

                            guardaBitacora("Documento creado", datos, tipo + comboBox1.Text, serie, Principal.Variablescompartidas.usuario, Principal.Variablescompartidas.nombre);
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
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA ", "");
                cmd.Parameters.AddWithValue("@CTIMESTAMP ", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@CMUNICIPIO ", municipio);
                cmd.Parameters.AddWithValue("@CSUCURSAL", sucursal);

                sqlConnection2.Open();
                cmd.ExecuteNonQuery();
                sqlConnection2.Close();
                error = 0;
                MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error", ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
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
            VariablesCompartidas.Atencion = "-";
            VariablesCompartidas.Solicito ="-";
            VariablesCompartidas.Textos = textBox1.Text;
            VariablesCompartidas.SerieImp = textSerie.Text;

            exporta(dataGridView1);
            correo();
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

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                dt.Rows.Add(item.Cells[0].Value
                    , item.Cells[1].Value
                    , item.Cells[3].Value
                    , item.Cells[4].Value
                    , item.Cells[6].Value
                    , item.Cells[6].Value
                    , item.Cells[7].Value
                    , item.Cells[8].Value

                    );
            }
            ds.Tables.Add(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizacion2020\imprep.rpt");

            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            rd.SetParameterValue("AtencionPa", VariablesCompartidas.Atencion);
            rd.SetParameterValue("SolicitoPa", VariablesCompartidas.Solicito);
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

        private void correo()
        {
            string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            @profile_name = 'Notifications',
            @recipients =@Recipiente,
            @copy_recipients = @Copia,
            @subject = @Folio2, 
            @body = @Folio,
            @file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            cmd.Parameters.AddWithValue("@Recipiente", "yisusedward@gmail.com"); 
            cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx"); 
            cmd.Parameters.AddWithValue("@Folio", "Se envia la cotizacion: " +textFolio.Text); 
            cmd.Parameters.AddWithValue("@Folio2",  textFolio.Text); 


            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Cp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(Cp.Text))
                {
                    llenarComboColonias(Cp.Text);
                }

            }
        }


        private void llenarComboColonias(string cp)
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand sc = new SqlCommand("select id, Colonia from codigosPostales where codigo = '" + cp + "'", sqlConnection1);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(string));

                dt.Load(reader);

                Colonia.ValueMember = "id";
                Colonia.DisplayMember = "Colonia";
                sqlConnection1.Close();
                Colonia.DataSource = dt;


            }
            catch (Exception)
            {
                Colonia.Text = "";
                sqlConnection1.Close();
            }

        }

        private void Colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Colonia.SelectedValue.ToString()))
            {
                obtenDatosColonia();
            }
        }

        private void obtenDatosColonia()
        {
            try
            {
                cmd.CommandText = @"select codigospostales.*, folios.sucnom, isnull(folios.sucursal, 'No') as sucursal
                from codigospostales left join folios on codigospostales.idSucu = folios.idalmacen where id = '" + Colonia.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    Cp.Text = reader["Codigo"].ToString();
                    Ciudad.Text = reader["Ciudad"].ToString();
                    Estado.Text = reader["Estado"].ToString();
                    Pais.Text = reader["Pais"].ToString();
                    if (Principal.Variablescompartidas.Perfil_id =="33")
                    {
                        if (reader["Sucursal"].ToString().Trim() == "No")
                        {
                            if (Principal.Variablescompartidas.sucural == "AUDITOR")
                            {
                                comboBox1.Text = "IGS";
                            }
                            else
                            {
                                comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
                            }


                        }
                        else
                        {
                            comboBox1.Text = reader["Sucursal"].ToString();
                        }
                    }
                    
                    

                }
                sqlConnection1.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Ciudad.Clear();
                Estado.Clear();
                Pais.Clear();
                Cp.Clear();
                sqlConnection1.Close();
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void inserta(string folio, string Referencia, string unidades)
        {
            obtenidDocumento();
            error = 1;
            while (error == 1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insDocumento", sqlConnection2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CidDocumento", idDocumento);
                    if (radioButton1.Checked)
                    {
                        cmd.Parameters.AddWithValue("@cidconcepto", cotizaContado);
                        cmd.Parameters.AddWithValue("@serie", letra);
                    }
                    else if (radioButton2.Checked)
                    {
                        cmd.Parameters.AddWithValue("@cidconcepto", CotizaCredito);
                        cmd.Parameters.AddWithValue("@serie", SerieCredito);
                    }

                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@Referencia", Referencia);
                    cmd.Parameters.AddWithValue("@TotalUnidades", unidades);
                    cmd.Parameters.AddWithValue("@Usuario", Principal.Variablescompartidas.usuario);
                    cmd.Parameters.AddWithValue("@tipo", "1");
                    cmd.Parameters.AddWithValue("@idcliente", CodigoCli.Text);
                    cmd.Parameters.AddWithValue("@nombrecliente", NombreCli.Text);
                    cmd.Parameters.AddWithValue("@rfccliente", Rfc.Text);
                    cmd.Parameters.AddWithValue("@usacliente", "1");
                    cmd.Parameters.AddWithValue("@cneto", Subtotal.Text);
                    cmd.Parameters.AddWithValue("@cimpuesto", ivaText.Text);
                    cmd.Parameters.AddWithValue("@total", totalText.Text);
                    cmd.Parameters.AddWithValue("@observaciones", textBox1.Text);



                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    error = 0;
                    MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
            }
                catch (SqlException ex)
            {
                error = 1;
                obtenidDocumento();
            }
        }
        }

        private void insertaMovimiento(int mov, string iddocumento1, String idproducto, String idalmacen, String unidades, int afecta, int afectaSaldo, int movowner, int tipoTraspaso, int Oculto,
        string precio, string preciocapturado, string neto, string impuesto1, string descuento1, string procentajedescuento, string total)
        {
            obtenidMovimiento();
            error = 1;
            while (error == 1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insMovimientoCotizacion", sqlConnection2);
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
                    cmd.Parameters.AddWithValue("@tipo", "1");

                    cmd.Parameters.AddWithValue("@cprecio", precio);
                    cmd.Parameters.AddWithValue("@cpreciocapturado", preciocapturado);
                    cmd.Parameters.AddWithValue("@cneto", neto);
                    cmd.Parameters.AddWithValue("@cimpuesto1", impuesto1);
                    cmd.Parameters.AddWithValue("@cdescuento", descuento1);
                    cmd.Parameters.AddWithValue("@porcentajedescuento", procentajedescuento);
                    cmd.Parameters.AddWithValue("@ctotal", total);

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


        private void GuardaCotizacion()
        {
            try
            {
                string sql = @"insert into Cotizaciones values (@folioSuc, @Sucursal, @FolioCotizacion, @FolioCot, @codCliente, @Local, @Atencion, @Solicito, @observa, @fecha)
                update folios set ultimofolio = @FolioCotizacion where sucursal = @Sucursal";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@folioSuc", textSerie.Text);
                cmd.Parameters.AddWithValue("@Sucursal", comboBox1.Text);
                cmd.Parameters.AddWithValue("@FolioCotizacion", ultFol);
                cmd.Parameters.AddWithValue("@FolioCot", textFolio.Text);
                cmd.Parameters.AddWithValue("@codCliente", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@Local", eslocal);
                cmd.Parameters.AddWithValue("@Atencion", Atencion.Text);
                cmd.Parameters.AddWithValue("@Solicito", Solicito.Text);
                cmd.Parameters.AddWithValue("@observa", textBox1.Text);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(textFecha.Text).ToString("MM/dd/yyyy"));

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                continua = 1;
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                MessageBox.Show(err.ToString(), "Error al Guardar Encabezado");

            }
        }

        private void GuardaDetalles()
        {
            continua = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        string sql = @"insert into cotizacionesdetalles values (@FolioCotizacion, @CodigoProducto, @Cantidad)";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@FolioCotizacion", textFolio.Text);
                        cmd.Parameters.AddWithValue("@CodigoProducto", row.Cells["Codigo"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cantidad"].Value.ToString());

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        continua = 1;
                    }
                    catch (NullReferenceException)
                    {


                    }
                }
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                MessageBox.Show(err.ToString(), "Error al Guardar Detalles Cotizacion");
            }

        }

        private void GuardaOriginal()
        {
            continua = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        string sql = @"insert into cotizacionesoriginal values (@FolioCotizacion, @codigoProducto, @NombreProducto, @Precio, @Cantidad, @Descuento, @PrecioDescuento, @Peso, @Importe, @existencia)";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@FolioCotizacion", textFolio.Text);
                        cmd.Parameters.AddWithValue("@CodigoProducto", row.Cells["Codigo"].Value.ToString());
                        cmd.Parameters.AddWithValue("@NombreProducto", row.Cells["Nombre"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Precio", row.Cells["PRecio"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cantidad"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Descuento", row.Cells["Descuento"].Value.ToString());
                        cmd.Parameters.AddWithValue("@PrecioDescuento", row.Cells["PrecioDesc"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Peso", row.Cells["peso"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Importe", row.Cells["importe"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Existencia", row.Cells["exis"].Value.ToString());

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        continua = 1;
                    }
                    catch (NullReferenceException)
                    {


                    }
                }
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                continua = 0;
                MessageBox.Show(err.ToString(), "Error al Guardar Detalles Cotizacion");
            }

        }
    }
}