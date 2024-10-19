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
using Microsoft.Reporting.WinForms;
using System.Text.RegularExpressions;

namespace Cotizacion2022
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

        public static string CodigoPasa { get; set; }
        public static string DescripcionPasa { get; set; }
        public static string ObservaPasa { get; set; }
        public static string CantidadPasa { get; set; }
        public static string DescuentoPasa { get; set; }

        public static string CorreoPasa { get; set; }
        public static string CorreoPasa2 { get; set; }


        //Clasificaciones PRUEBA
        //public static int SOBREPEDIDO { get; set; } = 315;
        //public static int ACCESORIOS { get; set; } = 316;
        //public static int PRODUCTOAMSA { get; set; } = 317;
        //public static int ORDENESEXPRES { get; set; } = 318;
        //public static int LINEA { get; set; } = 319;

        //Clasificaciones REALES
        public static int SOBREPEDIDO { get; set; } = 317;
        public static int ACCESORIOS { get; set; } = 318;
        public static int PRODUCTOAMSA { get; set; } = 319;
        public static int ORDENESEXPRES { get; set; } = 320;
        public static int LINEA { get; set; } = 321;


        //Perfiles PRUEBA
        //public static int DIRCOMER { get; set; } = 24;
        //public static int GERENTESUCU { get; set; } = 8;
        //public static int VENTASESPECIALES { get; set; } = 12;
        //public static int CALL_CENTER { get; set; } = 33;
        //public static int VENTASPLANTA { get; set; } = 38;

        //PERFILES REALES
        public static int DIRCOMER { get; set; } = 24;
        public static int GERENTESUCU { get; set; } = 8;
        public static int VENTASESPECIALES { get; set; } = 12;
        public static int CALL_CENTER { get; set; } = 33;
        public static int VENTASPLANTA { get; set; } = 38;


        double DescuentoGlobal = 0;
        public static string ListaPrecio { get; set; }

        public static int CLIENTECONTADO { get; set; } = 111;

        int Validado = 0;


        double CPRECIO_Pro = 0;
        double CNETO_Pro = 0;
        double cDescuentoGeneral_Pro = 0;
        double cImpuesto1_Pro = 0;
        double Importe_Pro = 0;
        double ImporteSI_Pro = 0;

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
            ListaPrecio = "Efectivo";
            //TipoText.Text = "Venta";
            cargaAgentes();

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


        private void cargaAgentes()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select cidagente, cnombreagente from admagentes order by cidagente", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("cidagente", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "cidagente";
            metroComboBox1.DisplayMember = "cnombreagente";
            metroComboBox1.DataSource = dt;

            sqlConnection2.Close();
        }

        private void desactiva()
        {
            if (TipoText.Text == "Venta")
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
            }
            else if (TipoText.Text == "Cotizacion")
            {
                button1.Enabled = true;
                button3.Enabled = true;
                button6.Enabled = true;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (Principal.Variablescompartidas.Perfil_id == VENTASESPECIALES.ToString())
            {
                panel2.Visible = true;
                button7.Visible = true;
                //Correguimos el tamaño y la posicion de la ventana
                dataGridView1.Size = new Size(1269, 278);
                dataGridView1.Location = new Point(6, 469);
            }
            else
            {
                panel2.Visible = true;
                button7.Visible = true;

                //dataGridView1.Size = new Size(1262, 314);
                //dataGridView1.Location = new Point(6, 291);
                //Coreguimos el tamaño y la posicion de la ventana
                dataGridView1.Size = new Size(1269, 278);
                dataGridView1.Location = new Point(6, 469);
            }

            //            Efectivo
            //Descuento 1.5
            //Descuento 3
            //Cheque
            //Cheque Plus
            //Tarjeta
            //Empleados
            //panel1.BackColor = ColorTranslator.FromHtml("#DDE4EE");
            DescuentoText.Text = "0";
            //llenarComboColoniasSINFILTRO();
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#CBD0D3");
            button1.BackColor = ColorTranslator.FromHtml("#2196F3");
            button2.BackColor = ColorTranslator.FromHtml("#3498DB");
            button3.BackColor = ColorTranslator.FromHtml("#F39C12");
            button4.BackColor = ColorTranslator.FromHtml("#E74C3C");
            button5.BackColor = ColorTranslator.FromHtml("#2ECC71");
            button6.BackColor = ColorTranslator.FromHtml("#9B59B6");
            button7.BackColor = ColorTranslator.FromHtml("#F1C40F");

            Colonia.SelectedIndex = -1;
            ComboEnvio.Text = "Sin Envio";

            CargaCombo();

            if (Principal.Variablescompartidas.Perfil_id == CALL_CENTER.ToString())
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

            //Calculos(615,3, 1);


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
                //--------¿ 

                //cmd.CommandText = @"select top 1 CCODIGOCLIENTE,  CRAZONSOCIAL, CRFC, 
                //CEMAIL1, CNOMBRECALLE, CNUMEROEXTERIOR, CTELEFONO1, CCOLONIA, 
                //CCODIGOPOSTAL, CPAIS, CCIUDAD, CESTADO from admClientes 
                //left join admDomicilios on admClientes.CIDCLIENTEPROVEEDOR = admDomicilios.CIDCATALOGO 
                //where CCODIGOCLIENTE='" + codigo + "' and ctipocatalogo = '1' and ctipodireccion = '1'";

                cmd.CommandText = @"with cliente as (select  CIDCLIENTEPROVEEDOR, CCODIGOCLIENTE,  CRAZONSOCIAL, CRFC,
                CEMAIL1
                FROM ADMCLIENTES 
                ),
                direccion as 
                (select CIDCATALOGO, CNOMBRECALLE, CNUMEROEXTERIOR, CTELEFONO1, CCOLONIA, 
                CCODIGOPOSTAL, CPAIS, CCIUDAD, CESTADO, ctipocatalogo, ctipodireccion from admdomicilios
                where ctipocatalogo = '1' and ctipodireccion = '1')
                
                select * from cliente 
                left join direccion on cliente.CIDCLIENTEPROVEEDOR = direccion.CIDCATALOGO
                where CCODIGOCLIENTE='" + codigo + "'";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    CodigoCli.Text = reader["CCODIGOCLIENTE"].ToString();
                    NombreCli.Text = reader["CRAZONSOCIAL"].ToString();
                    RecibeText.Text = reader["CRAZONSOCIAL"].ToString();
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
            cmd.CommandText = "select CBANVENTACREDITO, CDIASCREDITOCLIENTE from admClientes where CCODIGOCLIENTE = '" + codigo + "'";
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
                    comboBoxTipoDePago.Text = "Efectivo";
                    comboBoxTipoDePago.Enabled = false;
                }
                else if (reader["CBANVENTACREDITO"].ToString() == "0")
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = true;
                    comboBoxTipoDePago.Enabled = true;
                }

                DiasCredito.Text = reader["CDIASCREDITOCLIENTE"].ToString();


            }
            sqlConnection2.Close();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            //this.Hide();

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
                idCliente.Text = PantallaClientes.Id_Cliente;

                if (idCliente.Text == CLIENTECONTADO.ToString())
                {
                    NombreCli.Enabled = true;
                }
                else
                {
                    NombreCli.Enabled = false;
                }


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
                this.Show();


            }
        }

        public double existenciaLote(string codigo, string NumLote)
        {
            Double exis = 0;
            string sql = @"SELECT TOP (1000)
                    h.[CCODIGOPRODUCTO],
                    d.[CNUMEROLOTE],
                    h.[CNOMBREPRODUCTO],
                    a.[CNOMBREALMACEN],
                    h.CPRECIO10 as peso,
                    (h.CPRECIO1 / 1.16) as Precio,
                    d.[CEXISTENCIA]
                FROM[adAmsa_Pruebas no usar].[dbo].[admProductos] h
               LEFT JOIN admCapasProducto d ON h.CIDPRODUCTO = d.CIDPRODUCTO
                LEFT JOIN admExistenciaCosto e ON h.CIDPRODUCTO = e.CIDPRODUCTO AND d.CIDALMACEN = e.CIDALMACEN
                LEFT JOIN admAlmacenes a ON a.CIDALMACEN = d.CIDALMACEN AND a.CIDALMACEN = e.CIDALMACEN
                WHERE e.CIDALMACEN IS NOT NULL
                    AND e.CIDEJERCICIO = @ejercicio
                    AND h.CTIPOPRODUCTO = 1
                    AND h.CCONTROLEXISTENCIA = 17
                    AND d.CEXISTENCIA != 0
                    AND d.CIDPRODUCTO = @CodProducto
                    AND d.CNUMEROLOTE = @NumLote-- Filtro por código de producto
                ORDER BY h.CIDPRODUCTO; ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@CodProducto", codigo);
            cmd.Parameters.AddWithValue("@NumLote", NumLote);
            cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);

            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                exis = Double.Parse(reader["existencia"].ToString());

            }
            sqlConnection2.Close();

            return exis;
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
            inner join movs on periAnt.relaciona = movs.relaciona";
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


        public void Calculos(double PrecioNormal, double Descuento, double Cantidad)
        {

            double precioNormal = PrecioNormal;
            double Cunidades = Cantidad;
            double descuento = Math.Round(Descuento / 100, 4);

            double CPRECIO = 0;
            double CNETO = 0;
            double CNETO2 = 0;
            double cImpuesto1 = 0;
            double cDescuentoGeneral = 0;

            double Importe = 0;

            if (descuento > 0)
            {
                CPRECIO = Math.Round(PrecioNormal, 6);
                Importe = Math.Round(CPRECIO * Cunidades, 2);
                CNETO = Math.Round(Importe / 1.16, 2);
                cDescuentoGeneral = Math.Round(CNETO * descuento, 2);
                CNETO2 = Math.Round(CNETO / Cunidades, 2);
                cImpuesto1 = Math.Round((CNETO - cDescuentoGeneral) * 0.16, 2);

                double ImporteDescuento = Math.Round((CNETO - cDescuentoGeneral) + cImpuesto1, 2);

                CPRECIO_Pro = CPRECIO;
                CNETO_Pro = CNETO2;
                ImporteSI_Pro = CNETO;
                cDescuentoGeneral_Pro = cDescuentoGeneral;
                cImpuesto1_Pro = cImpuesto1;
                Importe_Pro = ImporteDescuento;

            } else
            {
                CPRECIO = Math.Round(PrecioNormal, 6);
                Importe = Math.Round(CPRECIO * Cunidades, 2);
                CNETO = Math.Round(Importe / 1.16, 2);
                cImpuesto1 = Math.Round(Importe - CNETO, 2);
                CNETO2 = Math.Round(CNETO / Cunidades, 2);


                CPRECIO_Pro = CPRECIO;
                CNETO_Pro = CNETO2;
                ImporteSI_Pro = CNETO;
                cDescuentoGeneral_Pro = cDescuentoGeneral;
                cImpuesto1_Pro = cImpuesto1;
                Importe_Pro = Importe;

            }


            //CPRECIO = Math.Round(PrecioNormal / 1.16, 6);
            //CNETO = Math.Round(CPRECIO * Cunidades, 2);
            //CNETO2 = Math.Round(CPRECIO,2);
            //cDescuentoGeneral = Math.Round(CNETO * descuento, 2);
            //cImpuesto1 = Math.Round((CNETO - cDescuentoGeneral) * 0.16f, 2);
            //Importe = (CNETO - cDescuentoGeneral) + cImpuesto1;



            //CPRECIO_Pro = CPRECIO;
            //CNETO_Pro = CNETO2;
            //ImporteSI_Pro = CNETO;
            //cDescuentoGeneral_Pro = cDescuentoGeneral;
            //cImpuesto1_Pro = cImpuesto1;
            //Importe_Pro = Importe;


        }

        public void insertgrid(string numeroLote, string codigoProducto, int cantidad, int existen,string nombre, double cuento, double precio )
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdPro"].Value = PantallaProductos.IdProPasa;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = codigoProducto;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = nombre;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existen;

            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = cantidad;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value = cuento;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value = PantallaProductos.peso;

            Calculos(precio, cuento, cantidad);

            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(precio), 4);
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Importe_Pro;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";
            calculaTotales();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
            {
                //this.Hide();


                CodigoPasa = "Nada";
                PantallaProductos.Ocultamos = "0";
                PantallaProductos.Cancelaste = "0";
                PantallaProductos.sucursalViene = comboBox1.Text;

                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value != null)
                {
                    CodigoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                    ObservaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value.ToString();
                    CantidadPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString();
                    DescripcionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    DescuentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value.ToString();

                    using (PantallaProductos pp = new PantallaProductos())
                    {
                        pp.ShowDialog();
                    }
                    if (!string.IsNullOrEmpty(PantallaProductos.codigo))
                    {
                        /*if (dataGridView1.CurrentRow != null &&
    dataGridView1.CurrentRow.Cells["NumLote"].Value != null &&
    !string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells["NumLote"].Value.ToString()))
                        {*/
                        //dataGridView1.Rows.Add();

                        //AGREGAR VARIABLES
                       

                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdPro"].Value = PantallaProductos.IdProPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = PantallaProductos.codigo;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = PantallaProductos.nombre;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa;


                                 dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                             
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value = PantallaProductos.DescuentoPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value = PantallaProductos.peso;

                                Calculos(double.Parse(PantallaProductos.precio), PantallaProductos.DescuentoPasa, double.Parse(PantallaProductos.CantidadEnvio));

                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Importe_Pro;
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";


                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precioOri"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["descuento"].Value = "0";
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value = PantallaProductos.peso;
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";

                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells["PrecioCapturado"].Value.ToString()), 4);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value.ToString()), 4);


                                //if (PantallaProductos.DescuentoPasa > 0)
                                //{
                                //    double precio = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precioOri"].Value.ToString()), 4);
                                //    double descuento1 = Math.Round(precio * (PantallaProductos.DescuentoPasa / 100), 4);
                                //    double precioReal = precio - descuento1;

                                //    double precio2 = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString()), 4);
                                //    double descuento2 = Math.Round(precio2 * (PantallaProductos.DescuentoPasa / 100), 4);
                                //    double precioReal2 = precio2 - descuento2;

                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value = PantallaProductos.DescuentoPasa.ToString();

                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value = precioReal.ToString();
                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value = precioReal2.ToString();

                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()), 2);

                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value.ToString()), 4);

                                //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value.ToString()), 4); 
                                //}

                                calculaTotales();

                           //de mi if }
                            /*else //if(!string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["NumLote"].Value.ToString()))
                            {

                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdPro"].Value = PantallaProductos.IdProPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value = PantallaProductos.codigo;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value = PantallaProductos.nombre;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["NumLote"].Value = null;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia(PantallaProductos.codigo);

                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value = PantallaProductos.DescuentoPasa;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value = PantallaProductos.peso;

                                Calculos(double.Parse(PantallaProductos.precio), PantallaProductos.DescuentoPasa, double.Parse(PantallaProductos.CantidadEnvio));

                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Importe_Pro;
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X";
                                calculaTotales();
                            }*/
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

                        

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["IdPro"].Value = PantallaProductos.IdProPasa;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value = PantallaProductos.codigo;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value = PantallaProductos.nombre;
                            
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Observa"].Value = PantallaProductos.ObservaPasa;

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["exis"].Value = existencia(PantallaProductos.codigo);
                           
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Descuento"].Value = PantallaProductos.DescuentoPasa;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value = PantallaProductos.peso;

                            Calculos(double.Parse(PantallaProductos.precio), PantallaProductos.DescuentoPasa, double.Parse(PantallaProductos.CantidadEnvio));

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CNETO"].Value = CNETO_Pro.ToString();
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["ImporteSI"].Value = ImporteSI_Pro;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Importe_Pro;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";


                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value = PantallaProductos.peso;
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value.ToString()), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["importe"].Value.ToString()) * 0.16f, 2);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioOri"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value.ToString()), 4);


                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precioOri"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["descuento"].Value = "0";
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(PantallaProductos.Cprecio1val), 4);

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value = PantallaProductos.peso;
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Eliminar"].Value = "X";

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = PantallaProductos.CantidadEnvio;
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioDesc"].Value.ToString()), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value.ToString()), 4);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["importe"].Value.ToString()) * 0.16f, 2);
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioOri"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PrecioCapturado"].Value.ToString()), 4);

                            //Descuentos(DescuentoGlobal);

                            // if (PantallaProductos.DescuentoPasa > 0)
                            //{

                            //double precio = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["precioOri"].Value.ToString()), 4);
                            //double descuento1 = Math.Round(precio * (PantallaProductos.DescuentoPasa / 100), 4);
                            //double precioReal = precio - descuento1;

                            //double precio2 = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["precio"].Value.ToString()), 4);
                            //double descuento2 = Math.Round(precio2 * (PantallaProductos.DescuentoPasa / 100), 4);
                            //double precioReal2 = precio2 - descuento2;

                            //double IvaReal = Math.Round(precio - precio2, 4);

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["Descuento"].Value = PantallaProductos.DescuentoPasa.ToString();

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["PrecioCapturado"].Value = precioReal.ToString();
                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["PrecioDesc"].Value = precioReal2.ToString();

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["PrecioDesc"].Value.ToString()), 2);

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["importe"].Value.ToString()) * 0.16f, 2);

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["PrecioCapturado"].Value.ToString()), 4);

                            ////dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["ImporteSI"].Value = ((precio2 - descuento1) + IvaReal) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value.ToString());

                            //dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index -1].Cells["PrecioCapturado"].Value.ToString()), 4); 

                            //}


                            calculaTotales();
                        }
                    }
                }
                this.Show();

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

        private void Descuentos(double descuento) {


            //try
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {


            //        if (dataGridView1.Rows[row.Index].Cells["Clasificacion"].Value.ToString() == LINEA.ToString())
            //        {
            //            //Sobre pedido
            //            double precio = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioOri"].Value.ToString()), 4);
            //            double descuento1 = Math.Round(precio * (descuento / 100), 4);
            //            double precioReal = precio - descuento1;

            //            double precio2 = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()), 4);
            //            double descuento2 = Math.Round(precio2 * (descuento / 100), 4);
            //            double precioReal2 = precio2 - descuento2;

            //            dataGridView1.Rows[row.Index].Cells["Descuento"].Value = descuento.ToString();

            //            dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = precioReal.ToString();
            //            dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = precioReal2.ToString();

            //            dataGridView1.Rows[row.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 2);

            //            dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
            //            dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);

            //            dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);

            //            //dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //        }
            //        else
            //        {
            //            dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
            //        }

            //        calculaTotales();

            //    }

            //    //calculaTotales();
            //}
            //catch (Exception)
            //{


            //}


            //try
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {


            //        if (dataGridView1.Rows[row.Index].Cells["Clasificacion"].Value.ToString() == LINEA.ToString() || dataGridView1.Rows[row.Index].Cells["Clasificacion"].Value.ToString() == Form1.SOBREPEDIDO.ToString())
            //        {
            //            //Sobre pedido
            //            double precio = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioOri"].Value.ToString()), 4);
            //            double descuento1 = Math.Round(precio * (Double.Parse(dataGridView1.Rows[row.Index].Cells["Descuento"].Value.ToString()) / 100), 4);
            //            double precioReal = precio - descuento1;

            //            double precio2 = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()), 4);
            //            double descuento2 = Math.Round(precio2 * (Double.Parse(dataGridView1.Rows[row.Index].Cells["Descuento"].Value.ToString()) / 100), 4);
            //            double precioReal2 = precio2 - descuento2;

            //            //dataGridView1.Rows[row.Index].Cells["Descuento"].Value = descuento.ToString();

            //            dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = precioReal.ToString();
            //            dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = precioReal2.ToString();

            //            dataGridView1.Rows[row.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 2);

            //            dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
            //            dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);

            //            dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);

            //            //dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
            //        }
            //        else
            //        {
            //            dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "0";
            //        }

            //        calculaTotales();

            //    }

            //    //calculaTotales();
            //}
            //catch (Exception)
            //{


            //}



        }



        private void CambioLista(string Tipo)
        {


            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (dataGridView1.Rows[row.Index].Cells["Clasificacion"].Value.ToString() != ORDENESEXPRES.ToString())
                    {
                        if (Tipo == "Efectivo" || Tipo == "Transferencia")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO1 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                Calculos(double.Parse(reader["descu"].ToString()), double.Parse(row.Cells["descuento"].Value.ToString()), double.Parse(row.Cells["Cantidad"].Value.ToString()));

                                dataGridView1.Rows[row.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(reader["descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                                dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                                dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Importe_Pro;
                                // dataGridView1.Rows[row.Index].Cells["Eliminar"].Value = "X";

                                //dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                //dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["PrecioOri"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);


                                //dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                //dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) *  Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close();
                        }
                        else if (Tipo == "Tarjeta")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO2 as descu from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                Calculos(double.Parse(reader["descu"].ToString()), double.Parse(row.Cells["descuento"].Value.ToString()), double.Parse(row.Cells["Cantidad"].Value.ToString()));

                                dataGridView1.Rows[row.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(reader["descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                                dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                                dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Importe_Pro;

                                //dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "1.5";
                                //dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                //dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["PrecioOri"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                //dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close();
                        }
                        else if (Tipo == "CheckPlus")
                        {
                            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, (CPRECIO1/1.16) as CPRECIO1, (CPRECIO2/1.16) as CPRECIO2, (CPRECIO3/1.16) as CPRECIO3, CPRECIO2 as descu  from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Codigo"].Value.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                Calculos(double.Parse(reader["descu"].ToString()), double.Parse(row.Cells["descuento"].Value.ToString()), double.Parse(row.Cells["Cantidad"].Value.ToString()));

                                dataGridView1.Rows[row.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(reader["descu"].ToString()), 4);
                                dataGridView1.Rows[row.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                                dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                                dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                                dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                                dataGridView1.Rows[row.Index].Cells["Importe"].Value = Importe_Pro;

                                //dataGridView1.Rows[row.Index].Cells["Descuento"].Value = "1.5";
                                //dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO2"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Importe"].Value = Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString());
                                //dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["PrecioOri"].Value = Math.Round(Convert.ToDouble(reader["Descu"].ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                                //dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
                                //dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precio"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioDesc"].Value.ToString()), 4);
                            }
                            sqlConnection2.Close();
                        }




                    }

                    calculaTotales();

                }

            }
            catch (Exception)
            {


            }



        }


        private void comboBoxTipoDePago_SelectedIndexChanged(object sender, EventArgs e)
        {

            CambioLista(comboBoxTipoDePago.Text);
            ListaPrecio = comboBoxTipoDePago.Text;
            //if (DescuentoText.Text != "0")
            //{
            //    Descuentos(DescuentoGlobal);
            //}

            Descuentos(DescuentoGlobal);
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

        public void calculaTotales()
        {
            double subtotal = 0;
            double unidades = 0;
            double iva = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Importe"].Value != null)
                {
                    //subtotal += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Importe"].Value.ToString());
                    //unidades += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString());

                    //subtotal = subtotal;
                    //unidades = unidades;
                    subtotal += (Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["cDescuento1"].Value.ToString()));
                    unidades += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["Cantidad"].Value.ToString());
                    iva += Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value.ToString());

                    subtotal = subtotal;
                    unidades = unidades;

                }
            }

            Unidades.Text = unidades.ToString();
            Subtotal.Text = Math.Round(subtotal, 2).ToString();
            // textBoxTotal.Text = subtotal.ToString();

            //Double iva = Convert.ToDouble(Subtotal.Text.ToString()) * 0.16f;
            //textBoxSubTotal.Text = iva.ToString();
            ivaText.Text = Math.Round(iva, 2).ToString();
            totalText.Text = Math.Round(subtotal + iva, 2).ToString();
            //textBoxIVA.Text = (float.Parse(textBoxTotal.Text) - float.Parse(textBoxSubTotal.Text)).ToString();

        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                Calculos(double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value.ToString()), double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento"].Value.ToString()), double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()));

                // dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Importe_Pro;

                calculaTotales();
            }
            catch (Exception)
            {


            }

            //try
            //{
            //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioDesc"].Value.ToString()),2);


            //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
            //    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
            //    calculaTotales();
            //}
            //catch (Exception)
            //{


            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd.CommandText = "select idalmacen, PedidoContado, PedidoCredito, SeriePediContado, SeriePediCredito from folios where sucursal = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                textSerie.Text = reader["idalmacen"].ToString();
                PediContado.Text = reader["PedidoContado"].ToString();
                PediCredito.Text = reader["PedidoCredito"].ToString();

                SeriePediContado.Text = reader["SeriePediContado"].ToString();
                SeriePediCredito.Text = reader["SeriePediCredito"].ToString();
            }
            sqlConnection1.Close();
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            limpiarCliente();

            Catalogo_Clientes.PantallaCliente.idPasa = "0";
            using (Catalogo_Clientes.PantallaCliente pc = new Catalogo_Clientes.PantallaCliente())
            {
                pc.ShowDialog();
            }

            //using (AltaClienteLoc ac = new AltaClienteLoc())
            //{
            //    ac.ShowDialog();
            //}
            //if (AltaClienteLoc.codigocli != "-")
            //{
            //    eslocal = "1";
            //    cargarClienteLOC(AltaClienteLoc.codigocli);
            //}


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
            //this.Hide();

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
                TipoText.Text = CargaCotizacion.TipoPasa;
                if (CargaCotizacion.Local == "1")
                {
                    //eslocal = "1";
                    //textBox1.Text = CargaCotizacion.observaciones;
                    //cargarClienteLOC(CargaCotizacion.ClienteCodigo);
                    //cargaCotiza(CargaCotizacion.foliocot);
                    //cargarDatos();
                    //calculaTotales();
                    //button5.Enabled = true;
                    //esCredito(CargaCotizacion.ClienteCodigo);
                }
                else if (CargaCotizacion.Local == "0")
                {
                    eslocal = "0";
                    textBox1.Text = CargaCotizacion.observaciones;
                    //cargarCliente(CargaCotizacion.ClienteCodigo);
                    cargaCotiza(CargaCotizacion.foliocot);
                    cargaDatosCot(CargaCotizacion.foliocot);
                    cargarDatos();

                    CambioLista(comboBoxTipoDePago.Text);

                    if (idCliente.Text == CLIENTECONTADO.ToString())
                    {
                        NombreCli.Enabled = true;
                    }
                    else
                    {
                        NombreCli.Enabled = false;
                    }

                    //if (!string.IsNullOrEmpty(DescuentoText.Text))
                    //{
                    //    DescuentoGlobal = Double.Parse(DescuentoText.Text);
                    Descuentos(DescuentoGlobal);

                    //}
                    //else
                    //{
                    //    DescuentoGlobal = 0;
                    //    DescuentoText.Text = "0";
                    //    calculaTotales();
                    //}

                    calculaTotales();
                    button5.Enabled = true;
                    esCredito(CargaCotizacion.ClienteCodigo);


                    this.Show();
                }
            }
        }

        private void cargaCotiza(string folio)
        {
            cont = 0;
            cmd.CommandText = "select * from CotizacionesDetalles where folioCotizacion = '" + folio + "'";
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
                    dataGridView1.Rows[cont].Cells["idPro"].Value = reader["id_Producto"].ToString();
                    dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["CodigoProducto"].ToString();
                    dataGridView1.Rows[cont].Cells["Cantidad"].Value = reader["Cantidad"].ToString();

                    dataGridView1.Rows[cont].Cells["Nombre"].Value = reader["Nombre"].ToString();
                    //dataGridView1.Rows[cont].Cells["PrecioCapturado"].Value = reader["PrecioCapturado"].ToString();

                    dataGridView1.Rows[cont].Cells["CPrecio"].Value = reader["Precio"].ToString();
                    dataGridView1.Rows[cont].Cells["CNETO"].Value = reader["Precio"].ToString();

                    // dataGridView1.Rows[cont].Cells["Precio"].Value = reader["Precio"].ToString();
                    // dataGridView1.Rows[cont].Cells["PrecioOri"].Value = reader["PrecioCapturado"].ToString();
                    // dataGridView1.Rows[cont].Cells["PrecioDesc"].Value = reader["Precio"].ToString();

                    dataGridView1.Rows[cont].Cells["Peso"].Value = reader["Peso"].ToString();
                    dataGridView1.Rows[cont].Cells["Importe"].Value = reader["Importe"].ToString();
                    dataGridView1.Rows[cont].Cells["ImporteSI"].Value = reader["ImporteSI"].ToString();
                    dataGridView1.Rows[cont].Cells["Cimpuesto1"].Value = reader["Impuesto1"].ToString();
                    dataGridView1.Rows[cont].Cells["cDescuento1"].Value = reader["Descuento1"].ToString();
                    dataGridView1.Rows[cont].Cells["Descuento"].Value = reader["Descuento"].ToString();
                    dataGridView1.Rows[cont].Cells["Observa"].Value = reader["Observa"].ToString();
                    dataGridView1.Rows[cont].Cells["NumLote"].Value = reader["NumLote"].ToString();

                    dataGridView1.Rows[cont].Cells["Eliminar"].Value = "X";

                    cont++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }


        private void cargaDatosCot(string folio)
        {
            cont = 0;
            string colonia = "";
            cmd.CommandText = "select * from Cotizaciones where foliocot = '" + folio + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            string idAgente = "";

            try
            {

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    DescuentoText.Text = reader["Descuento"].ToString();
                    comboBoxTipoDePago.Text = reader["TipoPago"].ToString();
                    direccion.Text = reader["Direccion"].ToString();

                    colonia = reader["Colonia"].ToString();
                    Numero.Text = reader["Numero"].ToString();
                    Telefono.Text = reader["Telefono"].ToString();
                    Correo.Text = reader["Email"].ToString();
                    Cp.Text = reader["Cp"].ToString();
                    Ciudad.Text = reader["Ciudad"].ToString();
                    Estado.Text = reader["Estado"].ToString();
                    Pais.Text = reader["Pais"].ToString();
                    NombreCli.Text = reader["Nombre_Cliente"].ToString();
                    idCliente.Text = reader["id_Cliente"].ToString();
                    CodigoCli.Text = reader["codcliente"].ToString();
                    Rfc.Text = reader["rfc"].ToString();
                    Solicito.Text = reader["Solicito"].ToString();
                    idAgente = reader["idAgente"].ToString();
                    metroDateTime2.Value = DateTime.Parse(reader["FechaEntrega_Documento"].ToString());


                    RecibeText.Text = reader["NombreRecibe"].ToString();
                    RecibeTelText.Text = reader["TelefonoRecibe"].ToString();
                    metroDateTime1.Value = DateTime.Parse(reader["FechaEntrega"].ToString());
                    FolioFacText.Text = reader["FolioFac"].ToString();
                    ComboEnvio.Text = reader["Envio"].ToString();

                    if (reader["Montacarga"].ToString() == "Si")
                    {
                        metroRadioButton1.Checked = true;
                    }
                    else if (reader["Montacarga"].ToString() == "No")
                    {
                        metroRadioButton2.Checked = true;
                    }

                    ObsDireccion.Text = reader["ObsDireccion"].ToString();
                    RecibeText.Text = reader["NombreRecibe"].ToString();
                    //direccion.Text = reader["idAgente"].ToString();

                }




            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());

            }
            finally
            {
                metroComboBox1.SelectedValue = idAgente;
                sqlConnection1.Close();

                if (!string.IsNullOrEmpty(Cp.Text))
                {

                    llenarComboColonias(Cp.Text);

                }
                Colonia.Text = colonia;

            }

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

                    cmd.CommandText = "select CNOMBREPRODUCTO, CPRECIO1 / 1.16 as precio, CPRECIO10, CPRECIO1, cidvalorclasificacion3 as clasificacion  from admProductos where CIDPRODUCTO = '" + row.Cells["IdPro"].Value.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        row.Cells["clasificacion"].Value = Math.Round(Convert.ToDouble(reader["clasificacion"].ToString()), 4);
                        if (reader["clasificacion"].ToString() == LINEA.ToString() || reader["clasificacion"].ToString() == PRODUCTOAMSA.ToString())
                        {
                            row.Cells["Nombre"].Value = reader["CNOMBREPRODUCTO"].ToString();


                            Calculos(double.Parse(reader["CPRECIO1"].ToString()), double.Parse(row.Cells["descuento"].Value.ToString()), double.Parse(row.Cells["Cantidad"].Value.ToString()));

                            dataGridView1.Rows[row.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                            dataGridView1.Rows[row.Index].Cells["CNETO"].Value = CNETO_Pro.ToString();
                            dataGridView1.Rows[row.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                            dataGridView1.Rows[row.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                            dataGridView1.Rows[row.Index].Cells["ImporteSI"].Value = ImporteSI_Pro;
                            dataGridView1.Rows[row.Index].Cells["Importe"].Value = Importe_Pro;

                            //row.Cells["Exis"].Value = existencia(row.Cells["Codigo"].Value.ToString());
                            //row.Cells["Precio"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                            //row.Cells["Peso"].Value = reader["CPRECIO10"].ToString();
                            ////row.Cells["Descuento"].Value = "0";
                            //row.Cells["PrecioDesc"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4);
                            //row.Cells["Importe"].Value = Math.Round(Convert.ToDouble(reader["precio"].ToString()), 4) * Convert.ToDouble(row.Cells["Cantidad"].Value);
                            //row.Cells["PrecioCapturado"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);
                            //row.Cells["PrecioORi"].Value = Math.Round(Convert.ToDouble(reader["CPRECIO1"].ToString()), 4);

                            //row.Cells["ImporteSI"].Value = Math.Round(Convert.ToDouble(row.Cells["Cantidad"].Value.ToString()) * Convert.ToDouble(row.Cells["PrecioCapturado"].Value.ToString()), 4);
                        }


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
                    //row.Cells["CIMPUESTO1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["importe"].Value.ToString()) * 0.16f, 2);
                    //row.Cells["Cdescuento1"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["precioORI"].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[row.Index].Cells["PrecioCapturado"].Value.ToString()), 4);
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
                string sql = "delete from cotizacionesdetalles where folioCotizacion = @param1 ";
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
                MessageBox.Show(err.ToString(), "Error al eliminar Encabezado");
            }
        }

        private void actualiza()
        {
            continua = 0;
            try
            {
                string sql = @"update cotizaciones set codcliente = @param1, esLocal = @param2, 
                observaciones = @Obs, fecha= @Fecha, descuento = @descuento,
                TipoPago = @TipoPago, Direccion = @Direccion, Colonia = @Colonia, Numero = @Numero, Telefono = @Telefono, Email = @Email,
                Cp = @Cp, Ciudad = @Ciudad, Estado = @Estado, Pais = @Pais, idAgente = @idAgente, Nombre_Cliente = @Nombre_Cliente, id_Cliente = @Id_Cliente,
                rfc = @RFC, Solicito = @Solicito, NombreRecibe = @NombreRecibe,TelefonoRecibe = @TelefonoRecibe, FechaEntrega = @FechaEntrega, FolioFac = @FolioFac, Montacarga = @Montacarga, Envio = @Envio, ImporteTotal = @ImporteTotal,
                FechaEntrega_Documento = @FechaEntrega_Documento, ObsDireccion = @ObsDireccion 
                where folioCot = @param3"; //Agregamos las observaciones de direccion
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", CodigoCli.Text);
                cmd.Parameters.AddWithValue("@param2", eslocal);
                cmd.Parameters.AddWithValue("@param3", textFolio.Text);
                cmd.Parameters.AddWithValue("@Obs", textBox1.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@descuento", DescuentoText.Text);

                cmd.Parameters.AddWithValue("@TipoPago", comboBoxTipoDePago.Text);
                cmd.Parameters.AddWithValue("@Direccion", direccion.Text);
                cmd.Parameters.AddWithValue("@Colonia", Colonia.Text);

                cmd.Parameters.AddWithValue("@Numero", Numero.Text);
                cmd.Parameters.AddWithValue("@Telefono", Telefono.Text);
                cmd.Parameters.AddWithValue("@Email", Correo.Text);
                cmd.Parameters.AddWithValue("@Cp", Cp.Text);
                cmd.Parameters.AddWithValue("@Ciudad", Ciudad.Text);
                cmd.Parameters.AddWithValue("@Estado", Estado.Text);
                cmd.Parameters.AddWithValue("@Pais", Pais.Text);
                cmd.Parameters.AddWithValue("@IdAgente", metroComboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Nombre_Cliente", NombreCli.Text);
                cmd.Parameters.AddWithValue("@Id_Cliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@RFC", Rfc.Text);
                cmd.Parameters.AddWithValue("@Solicito", Solicito.Text);

                cmd.Parameters.AddWithValue("@NombreRecibe", RecibeText.Text);
                cmd.Parameters.AddWithValue("@TelefonoRecibe", RecibeTelText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega", metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@FolioFac", FolioFacText.Text);
                if (metroRadioButton1.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "Si");
                }
                else if (metroRadioButton2.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "No");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "");
                }

                cmd.Parameters.AddWithValue("@Envio", ComboEnvio.Text);
                cmd.Parameters.AddWithValue("@ImporteTotal", totalText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega_Documento", metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@ObsDireccion", ObsDireccion.Text); //Agregamos ObsDireccion

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
                sqlConnection1.Close();
            }
        }

        private void actualizaFolios(string Folio, string idDocumento, string FolioSuc)
        {
            try
            {
                string sql = @"update cotizaciones set FolioComercial = @FolioComercial, IdDocumento = @IdDocumento
                where folioCot = @param3 and FolioSuc = @FolioSuc";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@FolioComercial", Folio);
                cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);
                cmd.Parameters.AddWithValue("@param3", textFolio.Text);
                cmd.Parameters.AddWithValue("@FolioSuc", FolioSuc);


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
                MessageBox.Show(err.ToString(), "Error al Actualizar Folios");
                sqlConnection1.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void obtenultimofolio()
        {
            //cmd.CommandText = "select ultimoFolio+1 as folio from folios where sucursal = '" + comboBox1.Text + "'";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //sqlConnection1.Close();
            //sqlConnection1.Open();
            //reader = cmd.ExecuteReader();

            //// Data is accessible through the DataReader object here.
            //if (reader.Read())
            //{
            //    ultFol = reader["folio"].ToString();

            //}
            //sqlConnection1.Close();

            cmd.CommandText = @"if not exists (
            select top 1 foliocotizacion + 1 as siguiente, foliocot, tipo
             from Cotizaciones
             where tipo = 'Cotizacion' and folioSuc = '" + textSerie.Text + @"'
            order by FolioCotizacion desc)
            begin
            select 1 as siguiente
            end
            else
            begin
            select top 1 foliocotizacion + 1 as siguiente, foliocot, tipo
             from Cotizaciones
             where tipo = 'Cotizacion' and folioSuc = '" + textSerie.Text + @"'
            order by FolioCotizacion desc
            end";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                ultFol = reader["siguiente"].ToString();

            }
            sqlConnection1.Close();
        }


        private void obtenultimofolioVenta()
        {
            cmd.CommandText = @"if not exists (
            select top 1 foliocotizacion + 1 as siguiente, foliocot, tipo
             from Cotizaciones
             where tipo = 'Venta' and folioSuc = '" + textSerie.Text + @"'
            order by FolioCotizacion desc)
            begin
            select 1 as siguiente
            end
            else
            begin
            select top 1 foliocotizacion + 1 as siguiente, foliocot, tipo
             from Cotizaciones
             where tipo = 'Venta' and folioSuc = '" + textSerie.Text + @"'
            order by FolioCotizacion desc
            end";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                ultFol = reader["siguiente"].ToString();

            }
            sqlConnection1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {



            if (TipoText.Text != "Venta")
            {
                if (string.IsNullOrEmpty(textFolio.Text))
                {
                    if (contadorRegistros() > 0)
                    {
                        if (TipoText.Text != "Venta")
                        {
                            if (!string.IsNullOrEmpty(CodigoCli.Text))
                            {
                                if (!string.IsNullOrEmpty(textFolio.Text))
                                {
                                    elimina();
                                    GuardaDetalles();
                                    if (continua == 1)
                                    {
                                        actualiza();
                                        if (continua == 1)
                                        {
                                            updateCliente();
                                            MessageBox.Show("Actualizado Correctamente!", "AVISO");
                                            TipoText.Text = "Cotizacion";
                                        }
                                    }
                                }
                                else
                                {
                                    obtenultimofolio();
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
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
                                            updateCliente();
                                            MessageBox.Show("Guardado Correctamente!", "AVISO");
                                        }
                                    }
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
                                    FolioSolo.Text = ultFol;
                                    TipoText.Text = "Cotizacion";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selecciona un Cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            MessageBox.Show("No puedes convertir una venta a cotizacion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se han agregado productos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (!string.IsNullOrEmpty(textFolio.Text))
                {

                    if (Autoriza())
                    {
                        //cmd.CommandText = "select sucnom, direccion, colonia, lugar, telefono, celular, nombre, email from datger where sucursal = '" + comboBox1.Text + "'";
                        //cmd.CommandType = CommandType.Text;
                        //cmd.Connection = sqlConnection1;
                        //sqlConnection1.Open();
                        //reader = cmd.ExecuteReader();

                        //// Data is accessible through the DataReader object here.
                        //if (reader.Read())
                        //{
                        //    infogerente.sucursal = reader["sucnom"].ToString();
                        //    infogerente.direccion = reader["direccion"].ToString();
                        //    infogerente.colonia = reader["colonia"].ToString();
                        //    infogerente.lugar = reader["lugar"].ToString();
                        //    infogerente.telefono = reader["telefono"].ToString();
                        //    infogerente.celular = reader["celular"].ToString();
                        //    //infogerente.nombre = reader["nombre"].ToString();
                        //    infogerente.email = reader["email"].ToString();


                        //}
                        //sqlConnection1.Close();

                        //infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();

                        //infogerente.subtotal = Subtotal.Text;
                        //infogerente.iva = ivaText.Text;
                        //infogerente.total = totalText.Text;

                        //VariablesCompartidas.Folio = textFolio.Text;
                        //VariablesCompartidas.Fecha = textFecha.Text;
                        //VariablesCompartidas.Cliente = NombreCli.Text;
                        //VariablesCompartidas.Telefono = Telefono.Text;
                        //VariablesCompartidas.Direccion = direccion.Text;
                        //VariablesCompartidas.Email = Correo.Text;
                        //VariablesCompartidas.Atencion = Atencion.Text;
                        //VariablesCompartidas.Solicito = Solicito.Text;
                        //VariablesCompartidas.Textos = textBox1.Text;
                        //VariablesCompartidas.SerieImp = textSerie.Text;
                        //using (ImprimeCot RC = new ImprimeCot(dataGridView1))
                        //{
                        //    RC.ShowDialog();
                        //}


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
                            //infogerente.colonia = reader["colonia"].ToString();
                            // infogerente.lugar = reader["lugar"].ToString();
                            infogerente.telefono = reader["telefono"].ToString();
                            infogerente.celular = reader["celular"].ToString();
                            //infogerente.nombre = reader["nombre"].ToString();
                            infogerente.email = reader["email"].ToString();


                        }
                        sqlConnection1.Close();

                        infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();
                        infogerente.colonia = Colonia.Text;
                        infogerente.lugar = RecibeText.Text;

                        infogerente.subtotal = Subtotal.Text;
                        infogerente.iva = ivaText.Text;
                        infogerente.total = totalText.Text;

                        VariablesCompartidas.Folio = textFolio.Text;
                        VariablesCompartidas.Fecha = textFecha.Text;
                        VariablesCompartidas.Cliente = NombreCli.Text;
                        VariablesCompartidas.Telefono = Telefono.Text;

                        //Agregamos la direccion completa
                        VariablesCompartidas.Direccion = direccion.Text + " " + Numero.Text + " " + " Col." + Colonia.Text + "\n" + Cp.Text + ", " + Ciudad.Text + ", " + Estado.Text + ", " + Pais.Text;

                        //Agregamos las Observaciones de direccion y el tipo de envio
                        VariablesCompartidas.ObsDireccion = ObsDireccion.Text;
                        VariablesCompartidas.Envio = ComboEnvio.Text;
                        VariablesCompartidas.Email = Correo.Text;
                        VariablesCompartidas.Atencion = Atencion.Text;
                        VariablesCompartidas.Solicito = Solicito.Text;
                        VariablesCompartidas.Textos = textBox1.Text;
                        VariablesCompartidas.SerieImp = textSerie.Text;
                        VariablesCompartidas.CorreoAgente = CorreoAgente.Text;
                        VariablesCompartidas.TelefonoAgente = TelefonoAgente.Text;
                        VariablesCompartidas.NombreAgente = metroComboBox1.Text;
                        //agregamos nuestro codigo
                        using (Imprime_Cotiza RC = new Imprime_Cotiza(dataGridView1))
                        {
                            RC.ShowDialog();
                        }

                        Autorizacion.Validado = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se autorizo esta acción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Las ventas no pueden ser impresas", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                metroDateTime1.Value = DateTime.Now;
                metroDateTime2.Value = DateTime.Now;

                metroRadioButton1.Checked = false;
                metroRadioButton2.Checked = false;

                cont = 0;
                continua = 0;
                cteloc = "";
                idloc = "";
                eslocal = "";
                ultFol = "";
                Subtotal.Clear();
                ivaText.Clear();
                totalText.Clear();
                button5.Enabled = true;
                metroComboBox1.SelectedValue = "0";
                comboBoxTipoDePago.Text = "Efectivo";
                llenarComboColoniasSINFILTRO();
                DescuentoText.Text = "0";
                DescuentoGlobal = 0;
                Colonia.SelectedIndex = -1;
                CorreoAgente.Text = "";
                TelefonoAgente.Text = "";
                ComboEnvio.Text = "Sin Envio";
                //TipoText.Text = "Venta";

                cmd.CommandText = "select idalmacen, PedidoContado, PedidoCredito, SeriePediContado, SeriePediCredito from folios where sucursal = '" + comboBox1.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Close();
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textSerie.Text = reader["idalmacen"].ToString();
                    PediContado.Text = reader["PedidoContado"].ToString();
                    PediCredito.Text = reader["PedidoCredito"].ToString();

                    SeriePediContado.Text = reader["SeriePediContado"].ToString();
                    SeriePediCredito.Text = reader["SeriePediCredito"].ToString();
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

        private int RevisaClas()
        {
            int hay = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    if (row.Cells["Clasificacion"].Value.ToString() == SOBREPEDIDO.ToString())
                    {
                        hay += 1;
                    }
                    if (row.Cells["Clasificacion"].Value.ToString() == ORDENESEXPRES.ToString())
                    {
                        hay += 1;
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            return hay;
        }


        private bool revisaDescuento()
        {
            int Total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    //if (row.Cells["Descuento"].Value.ToString() != "0")
                    //{
                    //    Total += 1;
                    //}
                    if (float.Parse(row.Cells["Descuento"].Value.ToString()) > 5)
                    {
                        Total += 1;
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            if (Total > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Autoriza()
        {
            if (revisaDescuento())
            {
                if (Principal.Variablescompartidas.Perfil_id == VENTASESPECIALES.ToString() || Principal.Variablescompartidas.Perfil_id == DIRCOMER.ToString())
                {
                    Autorizacion.Tipo = "Ventas Especiales";
                    Autorizacion.Tipo2 = "";
                    using (Autorizacion at = new Autorizacion())
                    {
                        at.ShowDialog();
                    }

                    if (Autorizacion.Validado == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Autorizacion.Tipo = "Sucursales";
                    return true;
                }

            }
            else if (RevisaClas() > 0)
            {
                if (Principal.Variablescompartidas.Perfil_id == VENTASESPECIALES.ToString() || Principal.Variablescompartidas.Perfil_id == DIRCOMER.ToString())
                {
                    Autorizacion.Tipo2 = "Agente";
                    Autorizacion.Tipo = "Ventas Especiales";
                }
                else
                {
                    Autorizacion.Tipo2 = "";
                    Autorizacion.Tipo = "Sucursales";

                }
                using (Autorizacion at = new Autorizacion())
                {
                    at.ShowDialog();
                }

                if (Autorizacion.Validado == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        private bool validaEnvio()
        {
            int errores = 0;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(Cp.Text))
            {
                errorProvider1.SetError(Cp, "Ingresa Codigo Postal");
                errores += 1;
            }
            if (string.IsNullOrEmpty(direccion.Text))
            {
                errorProvider1.SetError(direccion, "Ingresa una direccion");
                errores += 1;
            }
            if (string.IsNullOrEmpty(Telefono.Text))
            {
                errorProvider1.SetError(Telefono, "Ingresa un Telefono");
                errores += 1;
            }
            if (string.IsNullOrEmpty(Colonia.Text))
            {
                errorProvider1.SetError(Colonia, "Ingresa una Colonia");
                errores += 1;
            }
            if (errores == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(ComboEnvio.Text))
            {
                MessageBox.Show("Selecciona el tipo de envio", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (ComboEnvio.Text == "COD" || ComboEnvio.Text == "EAD")
                {
                    if (validaEnvio())
                    {
                        if (RevisarExistencia() > 0)
                        {

                            DialogResult result = MessageBox.Show("Hay productos con existencia insuficiente para surtir el pedido\n¿Deseas Continuar?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                GuardaPedido();
                                updateCliente();
                            }
                            else if (result == DialogResult.No)
                            {


                            }


                        }
                        else
                        {
                            GuardaPedido();
                            updateCliente();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Asegurate de capturar los campos marcados en rojo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (RevisarExistencia() > 0)
                    {

                        DialogResult result = MessageBox.Show("Hay productos con existencia insuficiente para surtir el pedido\n¿Deseas Continuar?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            GuardaPedido();
                            updateCliente();
                        }
                        else if (result == DialogResult.No)
                        {


                        }


                    }
                    else
                    {
                        GuardaPedido();
                        updateCliente();
                    }
                }
            }






        }

        private void GuardaPedido()
        {
            if (PediContado.Text != "0" || PediCredito.Text != "0")
            {
                if (!string.IsNullOrEmpty(idCliente.Text))
                {
                    if (contadorRegistros() > 0)
                    {
                        if (metroComboBox1.SelectedValue.ToString() != "0")
                        {
                            if (string.IsNullOrEmpty(TipoText.Text))
                            {
                                if (Autoriza())
                                {
                                    if (comboBoxTipoDePago.Text == "Tarjeta")
                                    {
                                        PagoTarjeta.ImporteTotal = totalText.Text;
                                        using (PagoTarjeta pt = new PagoTarjeta())
                                        {
                                            pt.ShowDialog();
                                        }

                                        if (PagoTarjeta.Cancelado == "No")
                                        {
                                            guardarVenta();
                                            cotizaComercial(PagoTarjeta.CreditoPasa, PagoTarjeta.DebitoPasa, "0", "", "");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }

                                    }
                                    else if (comboBoxTipoDePago.Text == "CheckPlus" || comboBoxTipoDePago.Text == "Transferencia")
                                    {
                                        PagoCheque.ImporteTotal = totalText.Text;
                                        using (PagoCheque pt = new PagoCheque())
                                        {
                                            pt.ShowDialog();
                                        }


                                        if (PagoCheque.Cancelado == "No")
                                        {
                                            guardarVenta();
                                            cotizaComercial("0", "0", PagoCheque.ImporteCheque, PagoCheque.FechaCheque, PagoCheque.NumeroCheque);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }



                                    }

                                    else
                                    {
                                        guardarVenta();
                                        cotizaComercial("0", "0", "0", "", "");
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("No se autorizo esta acción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                if (TipoText.Text == "Cotizacion")
                                {
                                    if (!string.IsNullOrEmpty(textFolio.Text))
                                    {
                                        if (Autoriza())
                                        {


                                            if (comboBoxTipoDePago.Text == "Tarjeta")
                                            {
                                                PagoTarjeta.ImporteTotal = totalText.Text;
                                                using (PagoTarjeta pt = new PagoTarjeta())
                                                {
                                                    pt.ShowDialog();
                                                }

                                                if (PagoTarjeta.Cancelado == "No")
                                                {

                                                    cotizaComercial(PagoTarjeta.CreditoPasa, PagoTarjeta.DebitoPasa, "0", "", "");
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }

                                            }
                                            else if (comboBoxTipoDePago.Text == "CheckPlus" || comboBoxTipoDePago.Text == "Transferencia")
                                            {
                                                PagoCheque.ImporteTotal = totalText.Text;
                                                using (PagoCheque pt = new PagoCheque())
                                                {
                                                    pt.ShowDialog();
                                                }


                                                if (PagoCheque.Cancelado == "No")
                                                {

                                                    cotizaComercial("0", "0", PagoCheque.ImporteCheque, PagoCheque.FechaCheque, PagoCheque.NumeroCheque);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }



                                            }
                                            else
                                            {

                                                cotizaComercial("0", "0", "0", "", "");
                                            }




                                        }
                                        else
                                        {
                                            MessageBox.Show("No se autorizo esta acción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Asegurate de guardar la cotizacion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }


                                else if (TipoText.Text == "Venta")
                                {
                                    if (string.IsNullOrEmpty(textFolio.Text))
                                    {
                                        if (Autoriza())
                                        {

                                            if (comboBoxTipoDePago.Text == "Tarjeta")
                                            {
                                                PagoTarjeta.ImporteTotal = totalText.Text;
                                                using (PagoTarjeta pt = new PagoTarjeta())
                                                {
                                                    pt.ShowDialog();
                                                }

                                                if (PagoTarjeta.Cancelado == "No")
                                                {
                                                    guardarVenta();
                                                    cotizaComercial(PagoTarjeta.CreditoPasa, PagoTarjeta.DebitoPasa, "0", "", "");
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }

                                            }
                                            else if (comboBoxTipoDePago.Text == "CheckPlus" || comboBoxTipoDePago.Text == "Transferencia")
                                            {
                                                PagoCheque.ImporteTotal = totalText.Text;
                                                using (PagoCheque pt = new PagoCheque())
                                                {
                                                    pt.ShowDialog();
                                                }


                                                if (PagoCheque.Cancelado == "No")
                                                {
                                                    guardarVenta();
                                                    cotizaComercial("0", "0", PagoCheque.ImporteCheque, PagoCheque.FechaCheque, PagoCheque.NumeroCheque);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }



                                            }
                                            else
                                            {
                                                guardarVenta();
                                                cotizaComercial("0", "0", "0", "", "");
                                            }



                                        }
                                    }
                                    else
                                    {
                                        if (Autoriza())
                                        {
                                            elimina();
                                            GuardaDetalles();
                                            if (continua == 1)
                                            {
                                                actualiza();
                                                if (continua == 1)
                                                {
                                                    MessageBox.Show("Venta Actualizada Correctamente!", "AVISO");
                                                    TipoText.Text = "Venta";
                                                    //cotizaComercial("0", "0");


                                                    if (comboBoxTipoDePago.Text == "Tarjeta")
                                                    {
                                                        PagoTarjeta.ImporteTotal = totalText.Text;
                                                        using (PagoTarjeta pt = new PagoTarjeta())
                                                        {
                                                            pt.ShowDialog();
                                                        }

                                                        if (PagoTarjeta.Cancelado == "No")
                                                        {

                                                            cotizaComercial(PagoTarjeta.CreditoPasa, PagoTarjeta.DebitoPasa, "0", "", "");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                        }

                                                    }
                                                    else if (comboBoxTipoDePago.Text == "CheckPlus" || comboBoxTipoDePago.Text == "Transferencia")
                                                    {
                                                        PagoCheque.ImporteTotal = totalText.Text;
                                                        using (PagoCheque pt = new PagoCheque())
                                                        {
                                                            pt.ShowDialog();
                                                        }


                                                        if (PagoCheque.Cancelado == "No")
                                                        {

                                                            cotizaComercial("0", "0", PagoCheque.ImporteCheque, PagoCheque.FechaCheque, PagoCheque.NumeroCheque);
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Accion Cancelada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                        }



                                                    }
                                                    else
                                                    {

                                                        cotizaComercial("0", "0", "0", "", "");
                                                    }




                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Asegurate de seleccionar un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se han agregado productos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay un concepto de pedido definido para este almacen", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void cotizaComercial(string Credito, string Debito, string Cheque, string FechaCheque, string NoCheque)
        {
            obtenInfo();
            double SumaDescuento = 0;

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    try {
            //        string Neto = "";
            //        string TotalDescuento = "";


            //        if (row.Cells["Descuento"].Value.ToString() != "0")
            //        {
            //            double neto = Math.Round((Double.Parse(row.Cells["Precio"].Value.ToString()) * Double.Parse(row.Cells["Cantidad"].Value.ToString())), 4);
            //            Neto = Math.Round(neto, 2).ToString();

            //            double Descuento = Math.Round(((Double.Parse(row.Cells["Precio"].Value.ToString()) - Double.Parse(row.Cells["PrecioDesc"].Value.ToString())) * Double.Parse(row.Cells["Cantidad"].Value.ToString())), 4);


            //            TotalDescuento = Math.Round(Descuento, 2).ToString();
            //            SumaDescuento = SumaDescuento + Descuento;
            //        }
            //        else if (row.Cells["Descuento"].Value.ToString() == "0")
            //        {
            //            double neto = double.Parse(row.Cells["Importe"].Value.ToString());
            //            Neto = Math.Round(neto, 2).ToString();
            //            TotalDescuento = "0";
            //        }
            //    }
            //    catch (NullReferenceException)
            //    {

            //    }
            //}

            double SubTReal = double.Parse(Subtotal.Text) + SumaDescuento;

            string referencia = "";
            string concepto = "";
            string FolioComercial = "";
            if (ComboEnvio.Text == "Sin Envio") {
                referencia = "";

            } else
            {
                referencia = ComboEnvio.Text;
            }

            if (radioButton1.Checked)
            {
                concepto = PediContado.Text;
            }
            else if (radioButton2.Checked)
            {
                concepto = PediCredito.Text;
            }


            FolioComercial = ObtenFolio(concepto).ToString();

            //inserta(FolioSolo.Text, referencia, Unidades.Text, Math.Round(SumaDescuento, 2).ToString(), Math.Round(SubTReal, 2).ToString(), Credito, Debito, Cheque, FechaCheque, NoCheque);


            //DOCUMENTO
            inserta(FolioComercial, referencia, Unidades.Text, Math.Round(SumaDescuento, 2).ToString(), Math.Round(SubTReal, 2).ToString(), Credito, Debito, Cheque, FechaCheque, NoCheque);

            actualizaFolios(FolioComercial, idDocumento, textSerie.Text);

            //direcciones
            insertaDirecciones(idDocumento, 3, 1, direccion.Text, Numero.Text, "", Colonia.Text, Cp.Text, Telefono.Text,
            Correo.Text, Pais.Text, Estado.Text, Ciudad.Text, Ciudad.Text, comboBox1.Text);



            int contadormov = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {

                    //string Neto = "";
                    //string TotalDescuento = "";

                    //if (row.Cells["Descuento"].Value.ToString() != "0")
                    //{
                    //    double neto = Math.Round((Double.Parse(row.Cells["Precio"].Value.ToString()) * Double.Parse(row.Cells["Cantidad"].Value.ToString())), 4);
                    //    Neto = Math.Round(neto, 2).ToString();

                    //    double Descuento = Math.Round(((Double.Parse(row.Cells["Precio"].Value.ToString()) - Double.Parse(row.Cells["PrecioDesc"].Value.ToString())) * Double.Parse(row.Cells["Cantidad"].Value.ToString())), 4);


                    //    TotalDescuento = Math.Round(Descuento, 2).ToString();
                    //}
                    //else if (row.Cells["Descuento"].Value.ToString() == "0")
                    //{
                    //    double neto = double.Parse(row.Cells["Importe"].Value.ToString());
                    //    Neto = Math.Round(neto, 2).ToString();
                    //    TotalDescuento = "0";
                    //}

                    //insertaMovimiento(int mov, string iddocumento1, String idproducto, String idalmacen, String unidades, int afecta, int afectaSaldo, int movowner, int tipoTraspaso, int Oculto,
                    //string precio, string preciocapturado, string neto, string impuesto1, string descuento1, string procentajedescuento, string total)
                    insertaMovimiento(contadormov, idDocumento, row.Cells["codigo"].Value.ToString(), textSerie.Text, row.Cells["cantidad"].Value.ToString(), 3, 1, 0, 1, 0, row.Cells["CNETO"].Value.ToString(),
                    row.Cells["CPRECIO"].Value.ToString(), row.Cells["ImporteSI"].Value.ToString(), Math.Round(Double.Parse(row.Cells["cimpuesto1"].Value.ToString()), 2).ToString(),
                    row.Cells["CDescuento1"].Value.ToString(), Math.Round(Double.Parse(row.Cells["descuento"].Value.ToString()), 2).ToString(), Math.Round(Convert.ToDouble(row.Cells["importe"].Value.ToString()) + Convert.ToDouble(row.Cells["Cimpuesto1"].Value.ToString()), 2).ToString(),
                    row.Cells["Observa"].Value.ToString(), comboBox1.Text);


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
            MessageBox.Show("Guardado en Comercial con folio: " + FolioComercial, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Autorizacion.Validado = 0;
        }



        private void guardarVenta()
        {

            obtenultimofolioVenta();
            textFolio.Text = "VEN-" + comboBox1.Text.Trim() + "-" + ultFol;
            GuardaCotizacionVenta();
            if (continua == 1)
            {
                GuardaDetalles();
            }
            if (continua == 1)
            {
                GuardaOriginal();
                if (continua == 1)
                {
                    MessageBox.Show("Guardado Correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TipoText.Text = "Venta";
                }
            }
            //textFolio.Text = "COT-" + comboBox1.Text + "-" + ultFol;
            FolioSolo.Text = ultFol;

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
                //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error", ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (TipoText.Text != "Venta")
            {
                if (string.IsNullOrEmpty(textFolio.Text))
                {
                    if (contadorRegistros() > 0)
                    {
                        if (TipoText.Text != "Venta")
                        {
                            if (!string.IsNullOrEmpty(CodigoCli.Text))
                            {
                                if (!string.IsNullOrEmpty(textFolio.Text))
                                {
                                    elimina();
                                    GuardaDetalles();
                                    if (continua == 1)
                                    {
                                        actualiza();
                                        if (continua == 1)
                                        {
                                            updateCliente();
                                            MessageBox.Show("Actualizado Correctamente!", "AVISO");
                                            TipoText.Text = "Cotizacion";
                                        }
                                    }
                                }
                                else
                                {
                                    obtenultimofolio();
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
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
                                            updateCliente();
                                            MessageBox.Show("Guardado Correctamente!", "AVISO");
                                        }
                                    }
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
                                    FolioSolo.Text = ultFol;
                                    TipoText.Text = "Cotizacion";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selecciona un Cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            MessageBox.Show("No puedes convertir una venta a cotizacion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se han agregado productos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (!string.IsNullOrEmpty(textFolio.Text))
                {
                    if (!string.IsNullOrEmpty(Correo.Text))
                    {
                        if (email_bien_escrito(Correo.Text))
                        {
                            if (string.IsNullOrEmpty(EmailAdicional.Text))
                            {
                                if (Autoriza())
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
                                        //infogerente.colonia = reader["colonia"].ToString();
                                        // infogerente.lugar = reader["lugar"].ToString();
                                        infogerente.telefono = reader["telefono"].ToString();
                                        infogerente.celular = reader["celular"].ToString();
                                        //infogerente.nombre = reader["nombre"].ToString();
                                        infogerente.email = reader["email"].ToString();


                                    }
                                    sqlConnection1.Close();

                                    infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();
                                    infogerente.colonia = Colonia.Text;
                                    infogerente.lugar = RecibeText.Text;

                                    infogerente.subtotal = Subtotal.Text;
                                    infogerente.iva = ivaText.Text;
                                    infogerente.total = totalText.Text;

                                    VariablesCompartidas.CorreoPasa = Correo.Text;
                                    VariablesCompartidas.CorreoPasa2 = EmailAdicional.Text;
                                    CorreoPasa = Correo.Text;
                                    CorreoPasa2 = EmailAdicional.Text;
                                    VariablesCompartidas.Folio = textFolio.Text;
                                    VariablesCompartidas.Fecha = textFecha.Text;
                                    VariablesCompartidas.Cliente = NombreCli.Text;
                                    VariablesCompartidas.Telefono = Telefono.Text;

                                    //Agregamos la direccion completa
                                    VariablesCompartidas.Direccion = direccion.Text + " " + Numero.Text + " " + " Col." + Colonia.Text + "\n" + Cp.Text + ", " + Ciudad.Text + ", " + Estado.Text + ", " + Pais.Text;

                                    //Agregamos las observaciones de direccion y el tipo de envio
                                    VariablesCompartidas.ObsDireccion = ObsDireccion.Text;
                                    VariablesCompartidas.Envio = ComboEnvio.Text;
                                    VariablesCompartidas.Email = Correo.Text;
                                    VariablesCompartidas.Atencion = Atencion.Text;
                                    VariablesCompartidas.Solicito = Solicito.Text;
                                    VariablesCompartidas.Textos = textBox1.Text;
                                    VariablesCompartidas.SerieImp = textSerie.Text;

                                    VariablesCompartidas.CorreoAgente = CorreoAgente.Text;
                                    VariablesCompartidas.TelefonoAgente = TelefonoAgente.Text;
                                    VariablesCompartidas.NombreAgente = metroComboBox1.Text;
                                    using (Imprime_Cotiza RC = new Imprime_Cotiza(dataGridView1))
                                    {
                                        RC.ShowDialog();
                                    }
                                    Autorizacion.Validado = 0;
                                    CorreoPasa = "";
                                    CorreoPasa2 = "";
                                }
                                else
                                {
                                    MessageBox.Show("No se autorizo esta acción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            else
                            {
                                if (email_bien_escrito(EmailAdicional.Text))
                                {

                                    if (Autoriza())
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
                                            //infogerente.nombre = reader["nombre"].ToString();
                                            infogerente.email = reader["email"].ToString();


                                        }
                                        sqlConnection1.Close();

                                        infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();

                                        infogerente.subtotal = Subtotal.Text;
                                        infogerente.iva = ivaText.Text;
                                        infogerente.total = totalText.Text;

                                        CorreoPasa = Correo.Text;
                                        CorreoPasa2 = EmailAdicional.Text;

                                        VariablesCompartidas.Folio = textFolio.Text;
                                        VariablesCompartidas.Fecha = textFecha.Text;
                                        VariablesCompartidas.Cliente = NombreCli.Text;
                                        VariablesCompartidas.Telefono = Telefono.Text;

                                        //Agregamos la direccion Completa
                                        VariablesCompartidas.Direccion = direccion.Text + " " + Numero.Text + " " + " Col. " + Colonia.Text + "\n" + Cp.Text + ", " + Ciudad.Text + ", " + Estado.Text + ", " + Pais.Text;

                                        // Agregamos las Observaciones de la direccion y el tipo de envio
                                        VariablesCompartidas.ObsDireccion = ObsDireccion.Text;
                                        VariablesCompartidas.Envio = ComboEnvio.Text;
                                        VariablesCompartidas.Email = Correo.Text;
                                        VariablesCompartidas.Atencion = Atencion.Text;
                                        VariablesCompartidas.Solicito = Solicito.Text;
                                        VariablesCompartidas.Textos = textBox1.Text;
                                        VariablesCompartidas.SerieImp = textSerie.Text;
                                        VariablesCompartidas.Envio = ComboEnvio.Text;
                                        VariablesCompartidas.CorreoAgente = CorreoAgente.Text;
                                        VariablesCompartidas.TelefonoAgente = TelefonoAgente.Text;
                                        VariablesCompartidas.NombreAgente = metroComboBox1.Text;
                                        using (Imprime_Cotiza RC = new Imprime_Cotiza(dataGridView1))
                                        {
                                            RC.ShowDialog();
                                        }
                                        Autorizacion.Validado = 0;
                                        CorreoPasa = "";
                                        CorreoPasa2 = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se autorizo esta acción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("El correo adicional no es valido", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }



                        }
                        else
                        {
                            MessageBox.Show("El correo electronico ingresado no es valido", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }
                    else
                    {
                        MessageBox.Show("Asegurate de ingresar un correo electronico", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                   
                }
            }
            else
            {
                MessageBox.Show("Las ventas no pueden enviarse por correo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void exporta(DataGridView datagridview1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            //Agregams codigo donde se maneja la columna lote.
            dt.Columns.Add("NumLote", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(string));
            dt.Columns.Add("cantidad", typeof(string));
            dt.Columns.Add("Dscto", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("importe", typeof(string));

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells["Codigo"].Value.ToString()))
                    {
                        dt.Rows.Add(item.Cells["Codigo"].Value
                        , item.Cells["Nombre"].Value
                        ,item.Cells["NumLote"].Value
                        , item.Cells["PrecioDesc"].Value
                        , item.Cells["Cantidad"].Value
                        , item.Cells["Descuento"].Value + "%"
                        , "$ " + item.Cells["PrecioOri"].Value
                        , item.Cells["Peso"].Value
                        , "$ " + item.Cells["ImporteSI"].Value

                        );
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
            ds.Tables.Add(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizacion2022\imprep.rpt");

            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);

            //Agregamos los parametros utilizarlos en el reporte
            rd.SetParameterValue("ObsDireccionPA", VariablesCompartidas.ObsDireccion);
            rd.SetParameterValue("EnvioPa", VariablesCompartidas.Envio);

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



        private void exporta2(DataGridView datagridview1)
        {
            DataSet1 Data_Set = new DataSet1();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("NumLote", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(string));
            dt.Columns.Add("cantidad", typeof(string));
            dt.Columns.Add("Descuento", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("importe", typeof(string));


            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells["Codigo"].Value.ToString()))
                    {
                        dt.Rows.Add(item.Cells["Codigo"].Value
                        , item.Cells["Nombre"].Value
                        , item.Cells["NumLote"].Value
                        , item.Cells["PrecioDesc"].Value
                        , item.Cells["Cantidad"].Value
                        , item.Cells["Descuento"].Value + "%"
                        , "$ " + item.Cells["PrecioOri"].Value
                        , item.Cells["Peso"].Value
                        , "$ " + item.Cells["ImporteSI"].Value

                        );
                    }
                }
                catch (NullReferenceException)
                {

                }

            }
            Data_Set.Tables.Add(dt);

            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            reportViewer1.RefreshReport();

            ReportParameter[] parameters = new ReportParameter[21];

            //rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            //rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            //rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            //rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            //rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            //rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            //rd.SetParameterValue("AtencionPa", VariablesCompartidas.Atencion);
            //rd.SetParameterValue("SolicitoPa", VariablesCompartidas.Solicito);
            //rd.SetParameterValue("textos", VariablesCompartidas.Textos);

            //rd.SetParameterValue("sucursal", infogerente.sucursal);
            //rd.SetParameterValue("direccion", infogerente.direccion);
            //rd.SetParameterValue("colonia", infogerente.colonia);
            //rd.SetParameterValue("lugar", infogerente.lugar);
            //rd.SetParameterValue("telefono", infogerente.telefono.Trim());
            //rd.SetParameterValue("celular", infogerente.celular.Trim());
            //rd.SetParameterValue("nombre", infogerente.nombre.Trim());
            //rd.SetParameterValue("email", infogerente.email.Trim());

            //rd.SetParameterValue("subtotal", infogerente.subtotal);
            //rd.SetParameterValue("iva", infogerente.iva);
            //rd.SetParameterValue("total", infogerente.total);

            parameters[0] = new ReportParameter("subtotal", infogerente.subtotal);
            parameters[1] = new ReportParameter("iva", infogerente.iva);
            parameters[2] = new ReportParameter("total", infogerente.total);

            parameters[3] = new ReportParameter("FolioPa", VariablesCompartidas.Folio);
            parameters[4] = new ReportParameter("FechaPa", VariablesCompartidas.Fecha);
            parameters[5] = new ReportParameter("NombrePa", VariablesCompartidas.Cliente);
            parameters[6] = new ReportParameter("TelefonoPa", VariablesCompartidas.Telefono);
            parameters[7] = new ReportParameter("DireccionPa", VariablesCompartidas.Direccion);

            //Enviamos los parametros al reporte
            parameters[8] = new ReportParameter("ObsDireccionPA", VariablesCompartidas.ObsDireccion);

            parameters[9] = new ReportParameter("EnvioPa", VariablesCompartidas.Envio);

            parameters[10] = new ReportParameter("EmailPa", VariablesCompartidas.Email);
            parameters[11] = new ReportParameter("AtencionPa", VariablesCompartidas.Solicito);
            parameters[12] = new ReportParameter("SolicitoPa", VariablesCompartidas.Solicito);
            parameters[13] = new ReportParameter("textos", VariablesCompartidas.Textos);

            parameters[14] = new ReportParameter("Sucursal", infogerente.sucursal);
            parameters[15] = new ReportParameter("direccion", infogerente.direccion);
            parameters[16] = new ReportParameter("colonia", infogerente.colonia);
            parameters[17] = new ReportParameter("lugar", infogerente.lugar);
            parameters[18] = new ReportParameter("telefono", infogerente.telefono.Trim());
            parameters[19] = new ReportParameter("celular", infogerente.celular.Trim());
            parameters[20] = new ReportParameter("nombre", infogerente.nombre.Trim());
            parameters[21] = new ReportParameter("email", infogerente.email.Trim());

            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(parameters);



            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();

            string deviceInfo = "";
            string[] streamIds;
            Warning[] warnings;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            var bytes = reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension,
                out streamIds, out warnings);

            string filename = @"\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf";
            File.WriteAllBytes(filename, bytes);
            System.Diagnostics.Process.Start(filename);

            // rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf");




        }


        private void correo()
        {
            //string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            //@profile_name = 'Notifications',
            //@recipients =@Recipiente,
            //@copy_recipients = @Copia,
            //@subject = @Folio2, 
            //@body = @Folio,
            //@file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf';";
            //SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.Parameters.AddWithValue("@Recipiente", "yisusedward@gmail.com"); 
            //cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx"); 
            //cmd.Parameters.AddWithValue("@Folio", "Se envia la cotizacion: " +textFolio.Text); 
            //cmd.Parameters.AddWithValue("@Folio2",  textFolio.Text); 


            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Close();

            //MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            @profile_name = 'Notificaciones',
            @recipients =@Recipiente,
            @subject = @Folio2, 
            @body_format = 'html',
            @body = @Folio,
            @file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            cmd.Parameters.AddWithValue("@Recipiente", Correo.Text);
            //cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx");
            cmd.Parameters.AddWithValue("@Folio", "<h1>Se envia la cotización: </h1>" + "<h2>" + textFolio.Text + "</h2>");
            cmd.Parameters.AddWithValue("@Folio2", textFolio.Text);


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
                else
                {
                    llenarComboColoniasSINFILTRO();
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
            try
            {
                if (!string.IsNullOrEmpty(Colonia.SelectedValue.ToString()))
                {
                    obtenDatosColonia();
                }
            }
            catch (NullReferenceException)
            {

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
                    if (Principal.Variablescompartidas.Perfil_id == "33")
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox2.Text.Equals("Descuento 1.5"))
            //{
            //    DescuentoGlobal = 1.5;
            //    Descuentos(DescuentoGlobal);
            //}
            //else if (comboBox2.Text.Equals("Descuento 3"))
            //{
            //    DescuentoGlobal = 3;
            //    Descuentos(DescuentoGlobal);
            //}
            //else if (string.IsNullOrEmpty(comboBox2.Text))
            //{
            //    DescuentoGlobal = 0;
            //    Descuentos(DescuentoGlobal);
            //}

        }

        private void DescuentoText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (string.IsNullOrEmpty(DescuentoText.Text))
                {
                    DescuentoText.Text = "0";
                    DescuentoGlobal = 0;
                    Descuentos(DescuentoGlobal);
                }
                else
                {

                    if (Principal.Variablescompartidas.Perfil_id == "12" || Principal.Variablescompartidas.Perfil_id == "24")
                    {
                        if (double.Parse(DescuentoText.Text) > 10)
                        {
                            MessageBox.Show("No puedes ingresar un descuento mayor al 10%", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DescuentoText.Text = "10";
                            DescuentoGlobal = Double.Parse(DescuentoText.Text);
                            Descuentos(DescuentoGlobal);
                        }
                        else
                        {
                            DescuentoGlobal = Double.Parse(DescuentoText.Text);
                            Descuentos(DescuentoGlobal);
                        }

                    }
                    else
                    {

                        if (double.Parse(DescuentoText.Text) > 3)
                        {
                            MessageBox.Show("No puedes ingresar un descuento mayor al 3%", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DescuentoText.Text = "3";
                            DescuentoGlobal = Double.Parse(DescuentoText.Text);
                            Descuentos(DescuentoGlobal);
                        }
                        else
                        {
                            DescuentoGlobal = Double.Parse(DescuentoText.Text);
                            Descuentos(DescuentoGlobal);
                        }
                    }


                    //DescuentoGlobal = Double.Parse(DescuentoText.Text);
                    //Descuentos(DescuentoGlobal);
                }

            }
        }



        private void DescuentoText_Leave(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(DescuentoText.Text))
            {
                DescuentoText.Text = "0";
                DescuentoGlobal = 0;
                Descuentos(DescuentoGlobal);
            }
            else
            {

                if (Principal.Variablescompartidas.Perfil_id == "12" || Principal.Variablescompartidas.Perfil_id == "24")
                {
                    if (double.Parse(DescuentoText.Text) > 10)
                    {
                        MessageBox.Show("No puedes ingresar un descuento mayor al 10%", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DescuentoText.Text = "10";
                        DescuentoGlobal = Double.Parse(DescuentoText.Text);
                        Descuentos(DescuentoGlobal);
                    }
                    else
                    {
                        DescuentoGlobal = Double.Parse(DescuentoText.Text);
                        Descuentos(DescuentoGlobal);
                    }

                }
                else
                {

                    if (double.Parse(DescuentoText.Text) > 3)
                    {
                        MessageBox.Show("No puedes ingresar un descuento mayor al 3%", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DescuentoText.Text = "3";
                        DescuentoGlobal = Double.Parse(DescuentoText.Text);
                        Descuentos(DescuentoGlobal);
                    }
                    else
                    {
                        DescuentoGlobal = Double.Parse(DescuentoText.Text);
                        Descuentos(DescuentoGlobal);
                    }
                }


                //DescuentoGlobal = Double.Parse(DescuentoText.Text);
                //Descuentos(DescuentoGlobal);
            }
        }

        private void Cp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Cp.Text))
            {
                llenarComboColonias(Cp.Text);
            }
            else
            {
                llenarComboColoniasSINFILTRO();
            }

        }

        private int contadorRegistros()
        {
            int regi = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells["IdPro"].Value.ToString()))
                    {
                        regi += 1;
                    }

                }
            }
            catch (NullReferenceException)
            {


            }

            return regi;
        }

        private int RevisarExistencia()
        {
            int regi = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    double Existencia = Double.Parse(row.Cells["Exis"].Value.ToString());
                    double Cantidad = Double.Parse(row.Cells["Cantidad"].Value.ToString());

                    if (Cantidad > Existencia)
                    {
                        regi += 1;
                    }


                }
            }
            catch (NullReferenceException)
            {


            }

            return regi;
        }

        public bool updateCliente()
        {
            try
            {
                string sql = @"update admClientes set CEMAIL1 = @email
                where CCODIGOCLIENTE = @Codigo";

                SqlCommand cmd = new SqlCommand(sql, Principal.Variablescompartidas.AcerosConnection);
                cmd.Parameters.AddWithValue("@email", Correo.Text);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (contadorRegistros() > 0)
            {
                if (TipoText.Text != "Venta")
                {
                    if (!string.IsNullOrEmpty(CodigoCli.Text))
                    {
                        if (!string.IsNullOrEmpty(textFolio.Text))
                        {
                            elimina();
                            GuardaDetalles();
                            if (continua == 1)
                            {
                                actualiza();
                                if (continua == 1)
                                {
                                    updateCliente();
                                    MessageBox.Show("Actualizado Correctamente!", "AVISO");
                                    TipoText.Text = "Cotizacion";
                                }
                            }
                        }
                        else
                        {
                            obtenultimofolio();
                            textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
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
                                    updateCliente();
                                    MessageBox.Show("Guardado Correctamente!", "AVISO");
                                }
                            }
                            textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
                            FolioSolo.Text = ultFol;
                            TipoText.Text = "Cotizacion";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un Cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("No puedes convertir una venta a cotizacion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No se han agregado productos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        //private void inserta(string folio, string Referencia, string unidades)
        //{
        //    obtenidDocumento();
        //    error = 1;
        //    while (error == 1)
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("insDocumento", sqlConnection2);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@CidDocumento", idDocumento);
        //            if (radioButton1.Checked)
        //            {
        //                cmd.Parameters.AddWithValue("@cidconcepto", cotizaContado);
        //                cmd.Parameters.AddWithValue("@serie", letra);
        //            }
        //            else if (radioButton2.Checked)
        //            {
        //                cmd.Parameters.AddWithValue("@cidconcepto", CotizaCredito);
        //                cmd.Parameters.AddWithValue("@serie", SerieCredito);
        //            }

        //            cmd.Parameters.AddWithValue("@Folio", folio);
        //            cmd.Parameters.AddWithValue("@Referencia", Referencia);
        //            cmd.Parameters.AddWithValue("@TotalUnidades", unidades);
        //            cmd.Parameters.AddWithValue("@Usuario", Principal.Variablescompartidas.usuario);
        //            cmd.Parameters.AddWithValue("@tipo", "1");
        //            cmd.Parameters.AddWithValue("@idcliente", CodigoCli.Text);
        //            cmd.Parameters.AddWithValue("@nombrecliente", NombreCli.Text);
        //            cmd.Parameters.AddWithValue("@rfccliente", Rfc.Text);
        //            cmd.Parameters.AddWithValue("@usacliente", "1");
        //            cmd.Parameters.AddWithValue("@cneto", Subtotal.Text);
        //            cmd.Parameters.AddWithValue("@cimpuesto", ivaText.Text);
        //            cmd.Parameters.AddWithValue("@total", totalText.Text);
        //            cmd.Parameters.AddWithValue("@observaciones", textBox1.Text);
        //            cmd.Parameters.AddWithValue("@IdAgente", metroComboBox1.SelectedValue.ToString());



        //            sqlConnection2.Open();
        //            cmd.ExecuteNonQuery();
        //            sqlConnection2.Close();
        //            error = 0;
        //            //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
        //    }
        //        catch (SqlException ex)
        //    {
        //        error = 1;
        //        obtenidDocumento();
        //    }
        //}
        //}

        private void inserta(string folio, string Referencia, string unidades, string TotalDescuento, string Subtotal, string credito, string debito, string Cheque, string FechaCheque, string noCheque)
        {
            obtenidDocumento();
            error = 1;
            while (error == 1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insDocumentoPedidoCoti2022", sqlConnection2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CidDocumento", idDocumento);

                    if (radioButton1.Checked)
                    {
                        cmd.Parameters.AddWithValue("@cidconcepto", PediContado.Text);
                        cmd.Parameters.AddWithValue("@serie", SeriePediContado.Text);
                    }
                    else if (radioButton2.Checked)
                    {
                        cmd.Parameters.AddWithValue("@cidconcepto", PediCredito.Text);
                        cmd.Parameters.AddWithValue("@serie", SeriePediCredito.Text);
                    }

                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@Referencia", Referencia);
                    cmd.Parameters.AddWithValue("@TotalUnidades", unidades);
                    cmd.Parameters.AddWithValue("@Usuario", Principal.Variablescompartidas.usuario);
                    cmd.Parameters.AddWithValue("@tipo", "2");
                    cmd.Parameters.AddWithValue("@idcliente", idCliente.Text);
                    cmd.Parameters.AddWithValue("@nombrecliente", NombreCli.Text);
                    cmd.Parameters.AddWithValue("@rfccliente", Rfc.Text);
                    cmd.Parameters.AddWithValue("@usacliente", "1");
                    //cmd.Parameters.AddWithValue("@cneto", Subtotal);
                    //cmd.Parameters.AddWithValue("@cimpuesto", ivaText.Text);
                    //cmd.Parameters.AddWithValue("@total", totalText.Text);

                    cmd.Parameters.AddWithValue("@cneto", "0");
                    cmd.Parameters.AddWithValue("@cimpuesto", "0");
                    cmd.Parameters.AddWithValue("@total", "0");
                    cmd.Parameters.AddWithValue("@observaciones", textBox1.Text);

                    cmd.Parameters.AddWithValue("@Destinatario", RecibeText.Text);
                    cmd.Parameters.AddWithValue("@Guia", "");
                    cmd.Parameters.AddWithValue("@Mensajeria", "");
                    cmd.Parameters.AddWithValue("@CuentaMensa", "");
                    cmd.Parameters.AddWithValue("@Cajas", "");
                    cmd.Parameters.AddWithValue("@Peso", "");
                    cmd.Parameters.AddWithValue("@IdAgente", metroComboBox1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DescuentoMov", "0");
                    cmd.Parameters.AddWithValue("@Impreso", "1");
                    cmd.Parameters.AddWithValue("@IdMoneda", "1");

                    cmd.Parameters.AddWithValue("@Credito  ", credito);
                    cmd.Parameters.AddWithValue("@Debito ", debito);
                    cmd.Parameters.AddWithValue("@Chequete ", Cheque);
                    cmd.Parameters.AddWithValue("@Fecha_Cheque ", FechaCheque);
                    cmd.Parameters.AddWithValue("@Numero_Cheque ", noCheque);
                    cmd.Parameters.AddWithValue("@Fecha_Entrega ", metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha));




                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    error = 0;
                    //MessageBox.Show("Se guardo con folio: " + idDocumento.ToString());
                }
                catch (SqlException ex)
                {
                    error = 1;
                    obtenidDocumento();
                }
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "select ctextoextra1, ctextoextra2, ccodigoagente from admagentes where cidagente = '" + metroComboBox1.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    CorreoAgente.Text = reader["ctextoextra1"].ToString();
                    TelefonoAgente.Text = reader["ctextoextra2"].ToString();
                    CodAgente.Text = reader["ccodigoagente"].ToString();

                }

            }
            catch (Exception)
            {
                CodAgente.Text = "";
                CorreoAgente.Text = "";
                TelefonoAgente.Text = "";
                sqlConnection2.Close();
            }
            finally
            {
                sqlConnection2.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
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
                    //infogerente.nombre = reader["nombre"].ToString();
                    infogerente.email = reader["email"].ToString();


                }
                sqlConnection1.Close();

                infogerente.nombre = Principal.Variablescompartidas.nombre.ToUpper();

                infogerente.subtotal = Subtotal.Text;
                infogerente.iva = ivaText.Text;
                infogerente.total = totalText.Text;

                VariablesCompartidas.Folio = textFolio.Text;
                VariablesCompartidas.Fecha = textFecha.Text;
                VariablesCompartidas.Cliente = NombreCli.Text;
                VariablesCompartidas.Telefono = Telefono.Text;

                //Agregamos la direccion completa
                VariablesCompartidas.Direccion = direccion.Text + " " + Numero.Text + " " + " Col." + Colonia.Text + "\n" + Cp.Text + ", " + Ciudad.Text + ", " + Estado.Text + ", " + Pais.Text;

                //Damos valor a las variables compartidas
                VariablesCompartidas.ObsDireccion = ObsDireccion.Text;
                VariablesCompartidas.Envio = ComboEnvio.Text;
                VariablesCompartidas.Email = Correo.Text;
                VariablesCompartidas.Atencion = Atencion.Text;
                VariablesCompartidas.Solicito = Solicito.Text;
                VariablesCompartidas.Textos = textBox1.Text;
                VariablesCompartidas.SerieImp = textSerie.Text;
                VariablesCompartidas.CorreoAgente = CorreoAgente.Text;
                VariablesCompartidas.TelefonoAgente = TelefonoAgente.Text;

                VariablesCompartidas.NombreRecibe = RecibeText.Text;
                VariablesCompartidas.TelefonoRecibe = RecibeText.Text;
                VariablesCompartidas.FechaEntrega = metroDateTime1.Text;
                VariablesCompartidas.FolioFac = FolioFacText.Text;
                VariablesCompartidas.TipoPago = comboBoxTipoDePago.Text;

                if (metroRadioButton1.Checked)
                {
                    VariablesCompartidas.Montacarga = "Si";
                }
                else if (metroRadioButton2.Checked)
                {
                    VariablesCompartidas.Montacarga = "No";
                }
                else
                {
                    VariablesCompartidas.Montacarga = "";
                }

                if (validacionEntrega())
                {
                //Agregamos nuestra berificacion para ver si esta guardado en Cotizaciones
                //Si no esta guardado guardamos 
                if (string.IsNullOrEmpty(textFolio.Text)){
                    if (contadorRegistros() > 0)
                    {
                        if (TipoText.Text != "Venta")
                        {
                            if (!string.IsNullOrEmpty(CodigoCli.Text))
                            {
                                if (!string.IsNullOrEmpty(textFolio.Text))
                                {
                                    elimina();
                                    GuardaDetalles();
                                    if (continua == 1)
                                    {
                                        actualiza();
                                        if (continua == 1)
                                        {
                                            updateCliente();
                                            MessageBox.Show("Actualizado Correctamente!", "AVISO");
                                            TipoText.Text = "Cotizacion";
                                        }
                                    }
                                }
                                else
                                {
                                    obtenultimofolio();
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
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
                                            updateCliente();
                                            MessageBox.Show("Guardado Correctamente!", "AVISO");
                                        }
                                    }
                                    textFolio.Text = "COT-" + comboBox1.Text.Trim() + "-" + ultFol;
                                    FolioSolo.Text = ultFol;
                                    TipoText.Text = "Cotizacion";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selecciona un Cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            MessageBox.Show("No puedes convertir una venta a cotizacion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se han agregado productos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (!string.IsNullOrEmpty(textFolio.Text))
                {
                    using (Imprime_Entrega RC = new Imprime_Entrega(dataGridView1))
                    {
                        RC.ShowDialog();
                        actualiza();
                    }
                    //Realicar un aupdate aqui

            }
            else
            {
               
            }

        }
                else
                {
                MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //METODO DE VALIDACIÓN DEL BOTÓN DE ENTREGA
        public bool validacionEntrega()
        {
            bool valido = false;
            int errores = 0;
            errorProvider1.Clear();

            // Condicion Para el Tipo De envio

            if (ComboEnvio.Text == "Sin Envio")
            {
                errorProvider1.SetError(ComboEnvio, "EL CAMPO TIPO DE ENVIO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Cp.Text.Length == 0)
            {
                errorProvider1.SetError(Cp, "EL CAMPO CODIGO POSTAL NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Colonia.SelectedIndex.Equals(-1))
            {
                errorProvider1.SetError(Colonia, "EL CAMPO COLONIA NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (direccion.Text.Length == 0)
            {
                errorProvider1.SetError(direccion, "EL CAMPO DIRECCIÓN NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Telefono.Text.Length == 0)
            {
                errorProvider1.SetError(Telefono, "EL CAMPO TELEFONO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Ciudad.Text.Length == 0)
            {
                errorProvider1.SetError(Ciudad, "EL CAMPO CIUDAD NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Estado.Text.Length == 0)
            {
                errorProvider1.SetError(Estado, "EL CAMPO ESTADO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (Pais.Text.Length == 0)
            {
                errorProvider1.SetError(Pais, "EL CAMPO PAIS NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (RecibeText.Text.Length == 0)
            {
                errorProvider1.SetError(RecibeText, "EL CAMPO NOMBRE DE QUIEN RECIBE NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (FolioFacText.Text.Length == 0)
            {
                errorProvider1.SetError(FolioFacText, "EL CAMPO FOLIO FACTURA O NOTA NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (metroRadioButton1.Checked || metroRadioButton2.Checked)
            {
            }else
            {
                errorProvider1.SetError(metroRadioButton1, "EL CAMPO CLIENTE CUENTA CON MONTACARGA NO PUEDE ESTAR VACIO");
                errores += 1;

            }

            if (errores >= 1)
            {
                valido = false;
                return valido;
            }
            else
            {
                valido = true;
                return valido;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CodAgente_Leave(object sender, EventArgs e)
        {
            string Agente = "";
            try
            {
                cmd.CommandText = "select Cnombreagente from admagentes where ccodigoagente = '" + CodAgente.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    Agente = reader["Cnombreagente"].ToString();
                }
                else
                {
                    MessageBox.Show("No se encontro un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodAgente.Clear();
                    metroComboBox1.Text = "(Ninguno)";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error al tratar de buscar un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CodAgente.Clear();
                Agente = "(Ninguno)";
            }
            finally
            {
                sqlConnection2.Close();
                metroComboBox1.Text = Agente;
            }
        }

        private void CodAgente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string Agente = "";
                try
                {
                    cmd.CommandText = "select Cnombreagente from admagentes where ccodigoagente = '" + CodAgente.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        Agente = reader["Cnombreagente"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontro un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CodAgente.Clear();
                        metroComboBox1.Text = "(Ninguno)";
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Ocurrio un error al tratar de buscar un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodAgente.Clear();
                    Agente = "(Ninguno)";
                }
                finally
                {
                    sqlConnection2.Close();
                    metroComboBox1.Text = Agente;
                }


            }
            else if ((int)e.KeyChar == (int)Keys.Tab)
            {
                string Agente = "";
                try
                {
                    cmd.CommandText = "select Cnombreagente from admagentes where ccodigoagente = '" + CodAgente.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        Agente = reader["Cnombreagente"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontro un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CodAgente.Clear();
                        metroComboBox1.Text = "(Ninguno)";
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Ocurrio un error al tratar de buscar un agente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodAgente.Clear();
                    Agente = "(Ninguno)";
                }
                finally
                {
                    sqlConnection2.Close();
                    metroComboBox1.Text = Agente;
                }
            }

        }
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Correo_Leave(object sender, EventArgs e)
        {
           
        }

        private void comboBoxTipoDePago_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CambioLista(comboBoxTipoDePago.Text);
            ListaPrecio = comboBoxTipoDePago.Text;
            //if (DescuentoText.Text != "0")
            //{
            //    Descuentos(DescuentoGlobal);
            //}

            Descuentos(DescuentoGlobal);
        }

        private void Rfc_Click(object sender, EventArgs e)
        {

        }

        private void insertaMovimiento(int mov, string iddocumento1, String idproducto, String idalmacen, String unidades, int afecta, 
            int afectaSaldo, int movowner, int tipoTraspaso, int Oculto,
            string precio, string preciocapturado, string neto, string impuesto1, string descuento1, string procentajedescuento, string total, string obsMov, string Ref)
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
                    // cmd.Parameters.AddWithValue("@tipo", "1");
                    cmd.Parameters.AddWithValue("@tipo", "2");

                    cmd.Parameters.AddWithValue("@cprecio", precio);
                    cmd.Parameters.AddWithValue("@cpreciocapturado", preciocapturado);
                    cmd.Parameters.AddWithValue("@cneto", neto);
                    cmd.Parameters.AddWithValue("@cimpuesto1", impuesto1);
                    cmd.Parameters.AddWithValue("@cdescuento", descuento1);
                    cmd.Parameters.AddWithValue("@porcentajedescuento", procentajedescuento);
                    cmd.Parameters.AddWithValue("@ctotal", total);
                    cmd.Parameters.AddWithValue("@Observa", obsMov);
                    cmd.Parameters.AddWithValue("@Ref", Ref);



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

        private void label10_Click(object sender, EventArgs e)
        {

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
       
        //Verificaciones para No Aceptar texto en el telefono
        private void Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Comprueba si el carácter presionado no es un dígito y no es un carácter de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            // Comprueba si el carácter presionado es un dígito y la longitud del texto es mayor o igual a 10
            else if (char.IsDigit(e.KeyChar))
            {
                if (textBox != null && textBox.Text.Length >= 10)
                {
                    e.Handled = true;
                }

            }
        }
        //Verificaciones para No Aceptar texto en el campo de numero
        private void Numero_KeyPress_1(object sender, KeyPressEventArgs e)
        {
           
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void metroDateTime2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void EmailAdicional_Click(object sender, EventArgs e)
        {

        }

        //Verificacion para no aceptar texto en el telefono
        private void RecibeTelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Comprueba si el carácter presionado no es un dígito y no es un carácter de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            // Comprueba si el carácter presionado es un dígito y la longitud del texto es mayor o igual a 10
            else if (char.IsDigit(e.KeyChar))
            {
                if (textBox != null && textBox.Text.Length >= 10)
                {
                    e.Handled = true;
                }

            }
        }

        
       
       

        private void GuardaCotizacion()
        {
            try
            {
                //string sql = @"insert into Cotizaciones values (@folioSuc, @Sucursal, @FolioCotizacion, @FolioCot, @codCliente, @Local, @Atencion, @Solicito, @observa, @fecha, @Descuento)
                //update folios set ultimofolio = @FolioCotizacion where sucursal = @Sucursal";
                //Agregamos las observaciones de direccion
                string sql = @"INSERT INTO [dbo].[Cotizaciones]
           ([FolioSuc]
           ,[Sucursal]
           ,[FolioCotizacion]
           ,[FolioCot]
           ,[CodCliente]
           ,[esLocal]
           ,[Atencion]
           ,[Solicito]
           ,[Observaciones]
           ,[Fecha]
           ,[Descuento]
           ,[TipoPago]
           ,[Direccion]
           ,[Colonia]
           ,[Numero]
           ,[Telefono]
           ,[Email]
           ,[Cp]
           ,[Ciudad]
           ,[Estado]
           ,[Pais]
           ,[idAgente]
           ,[Nombre_Cliente]
           ,[Id_Cliente]
           ,[RFC]
           ,[Tipo]
           ,[status]
           ,[NombreRecibe]
           ,[TelefonoRecibe]
           ,[FechaEntrega]
           ,[FolioFac]
           ,[Montacarga]
           ,[Envio]
           ,[ImporteTotal]
           ,[FechaEntrega_Documento]
           ,[ObsDireccion])
            VALUES
           (@folioSuc, @Sucursal, @FolioCotizacion, @FolioCot, @codCliente, @Local, @Atencion, @Solicito, @observa, @fecha, @Descuento,
            @TipoPago, @Direccion, @Colonia, @Numero, @Telefono, @Email, @Cp, @Ciudad, @Estado, @Pais, @IdAgente, @Nombre_Cliente, @Id_Cliente, @RFC,
            @Tipo, @Estatus, @NombreRecibe, @TelefonoRecibe, @FechaEntrega, @FolioFac, @Montacarga, @Envio, @ImporteTotal, @FechaEntrega_Documento, @ObsDireccion)

           update folios set ultimofolio = @FolioCotizacion where sucursal = @Sucursal";

                //Se pasan los datos a la base de datos

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
                cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(textFecha.Text).ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@Descuento", DescuentoText.Text);

                cmd.Parameters.AddWithValue("@TipoPago", comboBoxTipoDePago.Text);
                cmd.Parameters.AddWithValue("@Direccion", direccion.Text);
                cmd.Parameters.AddWithValue("@Colonia", Colonia.Text);
                cmd.Parameters.AddWithValue("@Numero", Numero.Text);

                

                cmd.Parameters.AddWithValue("@Telefono", Telefono.Text);
                cmd.Parameters.AddWithValue("@Email", Correo.Text);
                cmd.Parameters.AddWithValue("@Cp", Cp.Text);
                cmd.Parameters.AddWithValue("@Ciudad", Ciudad.Text);
                cmd.Parameters.AddWithValue("@Estado", Estado.Text);
                cmd.Parameters.AddWithValue("@Pais", Pais.Text);
                cmd.Parameters.AddWithValue("@IdAgente", metroComboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Nombre_Cliente", NombreCli.Text);
                cmd.Parameters.AddWithValue("@Id_Cliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@RFC", Rfc.Text);
                cmd.Parameters.AddWithValue("@Tipo", "Cotizacion");
                cmd.Parameters.AddWithValue("@Estatus", "A");

                cmd.Parameters.AddWithValue("@NombreRecibe", RecibeTelText.Text);
                cmd.Parameters.AddWithValue("@TelefonoRecibe", RecibeTelText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega", metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@FolioFac", FolioFacText.Text);
                if (metroRadioButton1.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "Si");
                }
                else if (metroRadioButton2.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "No");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "");
                }

                cmd.Parameters.AddWithValue("@Envio", ComboEnvio.Text);
                cmd.Parameters.AddWithValue("@ImporteTotal", totalText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega_Documento", metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@ObsDireccion", ObsDireccion.Text); //Enviamos el valor de Observaciones de direccion al servidor

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


        private void GuardaCotizacionVenta()
        {
            try
            {
                //string sql = @"insert into Cotizaciones values (@folioSuc, @Sucursal, @FolioCotizacion, @FolioCot, @codCliente, @Local, @Atencion, @Solicito, @observa, @fecha, @Descuento)
                //update folios set ultimofolio = @FolioCotizacion where sucursal = @Sucursal";
                //Agregamos las observaciones de la direccion
                string sql = @"INSERT INTO [dbo].[Cotizaciones]
                ([FolioSuc]
                ,[Sucursal]
                ,[FolioCotizacion]
                ,[FolioCot]
                ,[CodCliente]
                ,[esLocal]
                ,[Atencion]
                ,[Solicito]
                ,[Observaciones]
                ,[Fecha]
                ,[Descuento]
                ,[TipoPago]
                ,[Direccion]
                ,[Colonia]
                ,[Numero]
                ,[Telefono]
                ,[Email]
                ,[Cp]
                ,[Ciudad]
                ,[Estado]
                ,[Pais]
                ,[idAgente]
                ,[Nombre_Cliente]
                ,[Id_Cliente]
                ,[RFC]
                ,[Tipo]
                ,[status]
                ,[NombreRecibe]
                ,[TelefonoRecibe]
                ,[FechaEntrega]
                ,[FolioFac]
                ,[Montacarga]
                ,[Envio]
                ,[ImporteTotal]
                ,[FechaEntrega_Documento]
                ,[ObsDireccion])
                 VALUES
                (@folioSuc, @Sucursal, @FolioCotizacion, @FolioCot, @codCliente, @Local, @Atencion, @Solicito, @observa, @fecha, @Descuento,
                 @TipoPago, @Direccion, @Colonia, @Numero, @Telefono, @Email, @Cp, @Ciudad, @Estado, @Pais, @IdAgente, @Nombre_Cliente, @Id_Cliente, @RFC,
                 @Tipo, @Estatus, @NombreRecibe, @TelefonoRecibe, @FechaEntrega, @FolioFac, @Montacarga, @Envio, @ImporteTotal,  @FechaEntrega_Documento, @ObsDireccion)";



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
                cmd.Parameters.AddWithValue("@Descuento", DescuentoText.Text);

                cmd.Parameters.AddWithValue("@TipoPago", comboBoxTipoDePago.Text);
                cmd.Parameters.AddWithValue("@Direccion", direccion.Text);
                cmd.Parameters.AddWithValue("@Colonia", Colonia.Text);
                cmd.Parameters.AddWithValue("@Numero", Numero.Text);

                

                cmd.Parameters.AddWithValue("@Telefono", Telefono.Text);
                cmd.Parameters.AddWithValue("@Email", Correo.Text);
                cmd.Parameters.AddWithValue("@Cp", Cp.Text);
                cmd.Parameters.AddWithValue("@Ciudad", Ciudad.Text);
                cmd.Parameters.AddWithValue("@Estado", Estado.Text);
                cmd.Parameters.AddWithValue("@Pais", Pais.Text);
                cmd.Parameters.AddWithValue("@IdAgente", metroComboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Nombre_Cliente", NombreCli.Text);
                cmd.Parameters.AddWithValue("@Id_Cliente", idCliente.Text);
                cmd.Parameters.AddWithValue("@RFC", Rfc.Text);
                cmd.Parameters.AddWithValue("@Tipo", "Venta");
                cmd.Parameters.AddWithValue("@Estatus", "A");

                cmd.Parameters.AddWithValue("@NombreRecibe", RecibeTelText.Text);
                cmd.Parameters.AddWithValue("@TelefonoRecibe", RecibeTelText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega", metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@FolioFac", FolioFacText.Text);
                if (metroRadioButton1.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "Si");
                }
                else if (metroRadioButton2.Checked)
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "No");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Montacarga", "");
                }

                cmd.Parameters.AddWithValue("@Envio", ComboEnvio.Text);
                cmd.Parameters.AddWithValue("@ImporteTotal", totalText.Text);
                cmd.Parameters.AddWithValue("@FechaEntrega_Documento", metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@ObsDireccion", ObsDireccion.Text); //Enviamos las observaciones de direccion al servidor


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
                        //string sql = @"insert into cotizacionesdetalles values (@FolioCotizacion, @CodigoProducto, @Cantidad)";
                        string sql = @"INSERT INTO [dbo].[CotizacionesDetalles]
                                       ([FolioCotizacion]
                                       ,[CodigoProducto]
                                       ,[Cantidad]
                                       ,[Id_Producto]
                                       ,[Nombre]
                                       ,[PrecioCapturado]
                                       ,[Precio]
                                       ,[Descuento]
                                       ,[Peso]
                                       ,[Importe]
                                       ,[ImporteSI]
                                       ,[Impuesto1]
                                       ,[Descuento1]
                                       ,[Observa]
                                       ,[NumLote]
                                        )
                                        VALUES
                                       (@FolioCotizacion
                                       ,@CodigoProducto
                                       ,@Cantidad
                                       ,@Id_Producto
                                       ,@Nombre
                                       ,@PrecioCapturado
                                       ,@Precio
                                       ,@Descuento
                                       ,@Peso
                                       ,@Importe
                                       ,@ImporteSI
                                       ,@Impuesto1
                                       ,@Descuento1
                                       ,@Observa
                                       ,@NumLote
                                       )";


                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@FolioCotizacion", textFolio.Text);
                        cmd.Parameters.AddWithValue("@CodigoProducto", row.Cells["Codigo"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cantidad"].Value.ToString());

                        cmd.Parameters.AddWithValue("@Id_Producto", row.Cells["IdPro"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Nombre", row.Cells["Nombre"].Value.ToString());
                        cmd.Parameters.AddWithValue("@PrecioCapturado", row.Cells["CPRECIO"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Precio", row.Cells["CNETO"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Descuento", row.Cells["Descuento"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Peso", row.Cells["Peso"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Importe", row.Cells["Importe"].Value.ToString());
                        cmd.Parameters.AddWithValue("@ImporteSI", row.Cells["ImporteSI"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Impuesto1", row.Cells["CIMPUESTO1"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Descuento1", row.Cells["CDescuento1"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Observa", row.Cells["Observa"].Value.ToString());
                        cmd.Parameters.AddWithValue("@NumLote", row.Cells["NumLote"].Value.ToString());

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


        private int ObtenFolio(string idConcepto)
        {
            int cuenta = 0;
            cmd.CommandText = @"select cnoFolio+1 as siguiente from admconceptos where cidconceptodocumento = '" + idConcepto + "' " +
                "update admconceptos set cNoFolio = cnoFolio+1 where cidconceptodocumento = '" + idConcepto + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Principal.Variablescompartidas.AcerosConnection;
            Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                cuenta = int.Parse(reader["siguiente"].ToString());
            }
            Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            return cuenta;
        }


        private void GuardaOriginal()
        {
            //continua = 0;
            //try
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        try
            //        {
            //            string sql = @"insert into cotizacionesoriginal values (@FolioCotizacion, @codigoProducto, @NombreProducto, @Precio, @Cantidad, @Descuento, @PrecioDescuento, @Peso, @Importe, @existencia)";
            //            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //            cmd.Parameters.AddWithValue("@FolioCotizacion", textFolio.Text);
            //            cmd.Parameters.AddWithValue("@CodigoProducto", row.Cells["Codigo"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@NombreProducto", row.Cells["Nombre"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Precio", row.Cells["CPrecio"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cantidad"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Descuento", row.Cells["Descuento"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@PrecioDescuento", row.Cells["PrecioDesc"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Peso", row.Cells["peso"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Importe", row.Cells["importe"].Value.ToString());
            //            cmd.Parameters.AddWithValue("@Existencia", row.Cells["exis"].Value.ToString());

            //            sqlConnection1.Open();
            //            cmd.ExecuteNonQuery();
            //            sqlConnection1.Close();
                       continua = 1;
            //        }
            //        catch (NullReferenceException)
            //        {


            //        }
            //    }
            //}
            //catch (SqlException ex)
            //{

            //    SqlError err = ex.Errors[0];
            //    string mensaje = string.Empty;
            //    continua = 0;
            //    MessageBox.Show(err.ToString(), "Error al Guardar Detalles Cotizacion");
            //}

        }

    }
}