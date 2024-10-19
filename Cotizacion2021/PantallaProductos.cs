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

namespace Cotizacion2021
{
    public partial class PantallaProductos : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        public static string idPro { get; set; }
        public static string codigo { get; set; }
        public static string Cancelaste { get; set; }
        public static string nombre { get; set; }
        public static string precio { get; set; }
        public static string peso { get; set; }

        public static string DescripcionPasa { get; set; }

        public static string CantidadEnvio { get; set; }
        public static string ObservaPasa { get; set; }

        public static string descuento1Val { get; set; }
        public static string descuento2Val { get; set; }
        public static string DescuentoCliente { get; set; }
        public static string Cprecio1val { get; set; }
        public static string Cprecio2val { get; set; }
        public static string Cprecio3val { get; set; }
        public static string Cprecio6val { get; set; }

        public static string sucursalViene { get; set; }

        public static int IdCompo { get; set; }

        public static List<Operacion> Operaciones = new List<Operacion> { };

        public static List<componentesList> Componentes = new List<componentesList> { };

        public static int id_Mov { get; set; } = 0;
        public static int idPasa { get; set; } = 0;

        public static string idDocumentoPasa { get; set; } = "";

        public static string id_MovimientoPasaCompo { get; set; }

        public static int Descarto { get; set; }

        public PantallaProductos()
        {
            InitializeComponent();
            Busqueda.Select();
            Busqueda.Focus();
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

            button1.BackColor = ColorTranslator.FromHtml("#76CA62");

            button2.BackColor = ColorTranslator.FromHtml("#d93a2c");
            button3.BackColor = ColorTranslator.FromHtml("#06a038");

            idDocumentoText.Text = idDocumentoPasa;
        }

        private void PantallaProductos_Load(object sender, EventArgs e)
        {
            Descarto = 0;
            SqlCommand cmd = new SqlCommand(@"select admproductos.cidproducto, CCODIGOPRODUCTO codigo, 
            CNOMBREPRODUCTO nombre, (CPRECIO1 / 1.16) as Precio, CPRECIO10 as peso, (CPRECIO2 / 1.16) AS Descuento1, 
            (CPRECIO3 / 1.16) as Descuento2, (CPRECIO6/1.16) as CPRECIO6, CPRECIO1, CPRECIO2, CPRECIO3, CPRECIO6 as Empleado
            from admProductos where (CIDPRODUCTO != 0 and CIDPRODUCTO != 1)", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();

            Busqueda.Select();
            Busqueda.Focus();

            if (Form1.CodigoPasa !="Nada")
            {
                
                Busqueda.Text = Form1.CodigoPasa;
                textBox1.Text = Form1.ObservaPasa;
                cantidad.Text = Form1.CantidadPasa;
                IdMovText.Text = Form1.MovPasa;
                
                id_MovimientoPasaCompo = IdMovText.Text;

                dataGridView1_CellClick(this.dataGridView1, new DataGridViewCellEventArgs(0, 0));
                cantidad.Focus();
                cargarComponentes();

                descripcionText.Text = Form1.DescripcionPasa;
                if (idProText.Text == "3939")
                {
                    calculaTotales();
                }
                //calculaTotales();
                FormaCadena();

            }
            else
            {
                IdMovText.Text = ObtenIDMov();
                id_MovimientoPasaCompo = IdMovText.Text;

                if (guardarMov(idDocumentoText.Text, IdMovText.Text, "0", "Na", "0", "0", "0"))
                {
                   // MessageBox.Show("Sobres");
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void cargarComponentes()
        {
            SqlCommand cmd = new SqlCommand(@"select *, cantidad * precio as total from componentesOT where id_mov = '"+IdMovText.Text+"'", sqlConnection3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            sqlConnection3.Close();
        }

        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"codigo LIKE '%{Busqueda.Text}%' or nombre LIKE '%{Busqueda.Text}%'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
            nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
            precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
            peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
            descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
            descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();


            Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
            Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
            Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
            Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(cantidad.Text))
            {
                Cancelaste = "0";
                Descarto = 0;
                idPro = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
                DescripcionPasa = descripcionText.Text;

                if (CodigoPro.Text == "MANU01")
                {
                    precio = PrecioPro.Text;
                }
                else
                {
                    precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
                }
                
                peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
                descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
                descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();
                DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cliente"].Value.ToString();

                Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
                Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
                Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
                Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();
                CantidadEnvio = cantidad.Text;
                ObservaPasa = textBox1.Text;

                this.Close();
            }
            else
            {
                MessageBox.Show("Agrega una cantidad");
                cantidad.Focus();
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {

                    cantidad.Focus();
                    //codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value.ToString();
                    //nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["nombre"].Value.ToString();
                    //precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["precio"].Value.ToString();
                    //peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["peso"].Value.ToString();
                    //descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Descuento1"].Value.ToString();
                    //descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Descuento2"].Value.ToString();
                    //DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Cliente"].Value.ToString();

                    //Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["CPRECIO1"].Value.ToString();
                    //Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["CPRECIO2"].Value.ToString();
                    //Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["CPRECIO3"].Value.ToString();
                    //Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["CPRECIO6"].Value.ToString();
                    //this.Close();
                }
                catch (ArgumentOutOfRangeException)
                {

                    
                }
            }
        }

        private void Busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Select();
            }
        }

        private void Busqueda_TextChanged_1(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"codigo LIKE '%{Busqueda.Text}%' or nombre LIKE '%{Busqueda.Text}%'";
        }

        private void Busqueda_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Deseas Descartar este Producto ?\nTen encuenta que se eliminara todo lo capturado en este Producto", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (DeleteProducto(IdMovText.Text))
                {
                    Cancelaste = "1";
                    Descarto = 1;
                    codigo = "";
                    this.Close();
                }
                
            }
            else if (result == DialogResult.No)
            {

            }

            
        }

        public double existencia(string codigo, string sucursal)
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
		    when DATEPART(m, getdate()) = 11 then isnull(sum(cEntradasPeriodo10 - cSalidasPeriodo10), 0) 
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
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            cmd.Parameters.AddWithValue("@codigo", codigo);
            cmd.Parameters.AddWithValue("@sucursal", sucursal);
            cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);

            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                exis = Double.Parse(reader["existencia"].ToString());

            }
            sqlConnection1.Close();

            return exis;

        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
            CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
            NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();

            if (CodigoPro.Text == "MANU01")
            {
                PrecioPro.Enabled = true;
                descripcionText.Enabled = true;
            }
            else
            {
                PrecioPro.Enabled = false;
                descripcionText.Enabled = false;
            }

            ExisMI.Text = "0";
            ExiMAT.Text = "0";
            exiLP.Text = "0";
            exiIGS.Text = "0";
            exiSP.Text = "0";
            exiPLA.Text = "0";
            exiCD.Text = "0";


            ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), sucursalViene).ToString();
            ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
            exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
            exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
            exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
            exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
            exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
            exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();

            if (idProText.Text == "3939")
            {
                calculaTotales();
            }


        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                try
                {
                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Nombre"].Value.ToString();

                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }

                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }
                }
                catch (NullReferenceException)
                {
                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }


                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();

                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }

                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                try
                {
                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Nombre"].Value.ToString();

                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }

                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }

                }
                catch (ArgumentOutOfRangeException)
                {
                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();

                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }

                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }

                }
                catch (NullReferenceException)
                {
                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";

                    idProText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();
                    descripcionText.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();

                    if (CodigoPro.Text == "MANU01")
                    {
                        PrecioPro.Enabled = true;
                        descripcionText.Enabled = true;
                    }
                    else
                    {
                        PrecioPro.Enabled = false;
                        descripcionText.Enabled = false;
                    }

                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    if (idProText.Text == "3939")
                    {
                        calculaTotales();
                    }
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cantidad.Focus();
            }
            }

        private void cantidad_KeyPress(object sender, KeyPressEventArgs e)
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
                try
                {

                    if (!string.IsNullOrEmpty(cantidad.Text))
                    {
                        //cantidad.Focus();
                        Cancelaste = "0";
                        idPro = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                        codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                        nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
                        DescripcionPasa = descripcionText.Text;

                        if (CodigoPro.Text == "MANU01")
                        {
                            double preciocalculo = double.Parse(PrecioPro.Text) / 1.16;

                            Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                            precio = Math.Round(preciocalculo, 4).ToString();
                            Cprecio1val = Math.Round(preciocalculo, 4).ToString();
                            Cprecio2val = Math.Round(preciocalculo, 4).ToString();
                            Cprecio3val = Math.Round(preciocalculo, 4).ToString();
                            Cprecio6val = Math.Round(preciocalculo, 4).ToString();

                            descuento1Val = Math.Round(preciocalculo, 4).ToString();
                            descuento2Val = Math.Round(preciocalculo, 4).ToString();
                            DescuentoCliente = Math.Round(preciocalculo, 4).ToString();
                        }
                        else
                        {
                            precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
                            Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
                            Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
                            Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
                            Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();

                            descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
                            descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();
                            DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cliente"].Value.ToString();


                        }

                        peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
                        CantidadEnvio = cantidad.Text;
                        ObservaPasa = textBox1.Text;

                        //foreach (DataGridViewRow row in dataGridView2.Rows)
                        //{
                        //    string idComponente = row.Cells["Id"].Value.ToString();
                        //    string descripcion = row.Cells["Descripcion"].Value.ToString();
                        //    string Tolerancia = row.Cells["Tolerancia"].Value.ToString();
                        //    string Material = row.Cells["Material"].Value.ToString();
                        //    string Cantidad = row.Cells["Canti"].Value.ToString();
                        //    string total = row.Cells["Total"].Value.ToString();
                        //    string precio = row.Cells["Prec"].Value.ToString();

                        //    //PantallaProductos.Componentes.Add(new componentesList { id_Componente= idComponente, Descripcion = descripcion,  Tolerancia =Tolerancia, Material=Material, Cantidad =float.Parse(Cantidad), Precio= float.Parse(precio), Total = float.Parse(total) });
                        //}
                        this.Close();

                    }else
                    {
                        MessageBox.Show("Agrega una cantidad", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cantidad.Focus();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {


                }
            }
        }

        

        private void PrecioPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            id_Mov = Int32.Parse(ObtenIDCompo());

            id_MovimientoPasaCompo = IdMovText.Text;

            idPasa = 0;
            using (Componentes cp = new Componentes())
            {
                cp.ShowDialog();
            }

            if (Componente.Descripcion != "NO")
            {
                if (GuardaComponente(id_Mov.ToString(), Componente.Descripcion, Componente.Tolerancia, Componente.Material, Componente.Cantidad.ToString(), Componente.Precio.ToString()))
                {
                    //MessageBox.Show("si se hizo");
                }

                dt2.Rows.Clear();
                cargarComponentes();

                if (idProText.Text == "3939")
                {
                    calculaTotales();
                }
               
                FormaCadena();

                if (float.Parse(PrecioPro.Text) == 0)
                {
                    PrecioPro.Enabled = true;
                }
                else
                {
                    PrecioPro.Enabled = false;
                }

            }
           

        }


        private void calculaTotales()
        {
            double unidades = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                    unidades += Convert.ToDouble(dataGridView2.Rows[row.Index].Cells["Total"].Value.ToString());
                    unidades = unidades;
            }

            PrecioPro.Text = unidades.ToString();

            if (float.Parse(PrecioPro.Text) == 0)
            {
                PrecioPro.Enabled = true;
            }
            else
            {
                PrecioPro.Enabled = false;
            }
        }


        private void FormaCadena()
        {
            textBox1.Clear();
            string descripcion, cantidad, precio, total, Cadena = "";

            Cadena = descripcionText.Text + "\r\n";


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                descripcion = dataGridView2.Rows[row.Index].Cells["Descripcion"].Value.ToString();
                cantidad = dataGridView2.Rows[row.Index].Cells["Canti"].Value.ToString();
                Cadena = Cadena + "-" +  cantidad + " " + descripcion  + "\r\n";
            }
            textBox1.Text = Cadena;


            //textBox1.Clear();
            //int id, id_madre = 0;

            //string  Cadena, descripcion, codigo, supervisor = "";
            //Cadena ="Esta es la lista" + "\r\n";
            //foreach (var Valor in Operaciones)
            //{
            //    descripcion = Valor.Descripcion;
            //    id = Valor.id_Compo;
            //    id_madre = Valor.id_Operacion;
            //    codigo =Valor.Codigo;
            //    supervisor = Valor.Supervisor;
            //    Cadena = Cadena + "Id: "+id.ToString() +" Id_Pro:"+ id_madre + " Descrip: " +descripcion  + "\r\n";
            //}

            //textBox1.Text = Cadena;
        }


        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Id"].Value.ToString());
                Operaciones.RemoveAll(x => x.id_Compo == id);

                dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
                calculaTotales();
                FormaCadena();
            }
            catch (Exception)
            {
                
            }
        }

        private void descripcionText_TextChanged(object sender, EventArgs e)
        {
            FormaCadena();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Descripcion")
            {
                //MessageBox.Show(dataGridView2.CurrentRow.Index.ToString());
                //int id = Int32.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Id"].Value.ToString());
                //var Filtrados = PantallaProductos.Operaciones.Where(x => x.id_Compo == id).ToList();
                //foreach (var valor in Filtrados)
                //{
                //    MessageBox.Show(valor.id_Compo + " " + valor.id_Operacion);
                //}

                Componente.Descripcion = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Descripcion"].Value.ToString(); 
                Componente.Material = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Material"].Value.ToString();
                Componente.Tolerancia = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Tolerancia"].Value.ToString();
                Componente.Precio = float.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Prec"].Value.ToString());
                Componente.Cantidad = float.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Canti"].Value.ToString()); 
                idPasa = Int32.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["id"].Value.ToString());

                using (Componentes cp = new Componentes())
                {
                    cp.ShowDialog();
                }
                dt2.Rows.Clear();
                cargarComponentes();
                if (Componente.Descripcion!="NO")
                {
                    if (GuardaComponente(id_Mov.ToString(), Componente.Descripcion, Componente.Tolerancia, Componente.Material, Componente.Cantidad.ToString(), Componente.Precio.ToString()))
                    {
                        dt2.Rows.Clear();
                        cargarComponentes();
                        // MessageBox.Show("si se hizo");
                    }
                }
                

            }
        }


        private bool guardarMov(string idDocumento, string id_Mov, string idPro, string CodigoPro, string Precio, string cantidad, string observa)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("InsertaMovtosOT", sqlConnection3);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Documento", idDocumento);
                cmd.Parameters.AddWithValue("@id_Mov", id_Mov);
                cmd.Parameters.AddWithValue("@FolioCotizacion", "");
                cmd.Parameters.AddWithValue("@idProducto", idPro);
                cmd.Parameters.AddWithValue("@CodigoProducto", CodigoPro);
                cmd.Parameters.AddWithValue("@Precio", Precio);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Observaciones", observa);
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

        private string ObtenIDMov()
        {
            cmd.CommandText = @"declare @id_doc int

	if((select count(*) from cotizacionesDetallesOT) =0)
	begin
	set @id_doc = (select 1 as siguiente);
	end 
	else
	begin
	set @id_doc = (select top 1 (id_Mov + 1) as siguiente from cotizacionesDetallesOT order by id_Mov desc);
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



        private bool GuardaComponente(string id_Compi, string Descripcion, string Tolerancia, string Material, string Cantidad, string Precio)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[ComponentesOT]
                  SET
                    [Descripcion] = @Descripcion
                     ,[Tolerancia] = @Tolerancia
                     ,[Material] = @Material
                     ,[Cantidad] = @Cantidad
                     ,[Precio] = @Precio
                WHERE id_Componente = @id_Componente", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id_Componente", id_Compi);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Tolerancia", Tolerancia);
                cmd.Parameters.AddWithValue("@Material", Material);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@Precio", Precio);

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



        private string ObtenIDCompo()
        {
            cmd.CommandText = @"declare @id_doc int

	        if((select count(*) from componentesOT) =0)
	        begin
	        set @id_doc = (select 1 as siguiente);
	        end 
	        else
	        begin
	        set @id_doc = (select top 1 (id_componente + 1) as siguiente from componentesOT 
	        order by id_componente desc);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(cantidad.Text))
                {
                    //cantidad.Focus();
                    Cancelaste = "0";
                    Descarto = 0;
                    idPro = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cidproducto"].Value.ToString();
                    codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                    nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
                    DescripcionPasa = descripcionText.Text;

                    if (CodigoPro.Text == "MANU01")
                    {
                        double preciocalculo = double.Parse(PrecioPro.Text) / 1.16;

                        Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                        precio = Math.Round(preciocalculo, 4).ToString();
                        Cprecio1val = Math.Round(preciocalculo, 4).ToString();
                        Cprecio2val = Math.Round(preciocalculo, 4).ToString();
                        Cprecio3val = Math.Round(preciocalculo, 4).ToString();
                        Cprecio6val = Math.Round(preciocalculo, 4).ToString();

                        descuento1Val = Math.Round(preciocalculo, 4).ToString();
                        descuento2Val = Math.Round(preciocalculo, 4).ToString();
                        DescuentoCliente = Math.Round(preciocalculo, 4).ToString();
                    }
                    else
                    {
                        precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
                        Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
                        Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
                        Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
                        Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();

                        descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
                        descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();
                        DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cliente"].Value.ToString();


                    }


                    peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
                    CantidadEnvio = cantidad.Text;
                    ObservaPasa = textBox1.Text;

                    //foreach (DataGridViewRow row in dataGridView2.Rows)
                    //{
                    //    string idComponente = row.Cells["Id"].Value.ToString();
                    //    string descripcion = row.Cells["Descripcion"].Value.ToString();
                    //    string Tolerancia = row.Cells["Tolerancia"].Value.ToString();
                    //    string Material = row.Cells["Material"].Value.ToString();
                    //    string Cantidad = row.Cells["Canti"].Value.ToString();
                    //    string total = row.Cells["Total"].Value.ToString();
                    //    string precio = row.Cells["Prec"].Value.ToString();

                    //    //PantallaProductos.Componentes.Add(new componentesList { id_Componente= idComponente, Descripcion = descripcion,  Tolerancia =Tolerancia, Material=Material, Cantidad =float.Parse(Cantidad), Precio= float.Parse(precio), Total = float.Parse(total) });
                    //}
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Agrega una cantidad", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cantidad.Focus();
                }
            }
            catch (ArgumentOutOfRangeException)
            {


            }
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

        private void idProText_Click(object sender, EventArgs e)
        {

        }

        private void idProText_TextChanged(object sender, EventArgs e)
        {
            if (idProText.Text == "3939")
            {
                calculaTotales();
            }

        }
    }
}