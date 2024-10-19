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

namespace Cotizacion2020
{
    public partial class PantallaProductos : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public static string codigo { get; set; }
        public static string Cancelaste { get; set; }
        public static string nombre { get; set; }
        public static string precio { get; set; }
        public static string peso { get; set; }

        public static string CantidadEnvio { get; set; }

        public static string descuento1Val { get; set; }
        public static string descuento2Val { get; set; }
        public static string DescuentoCliente { get; set; }
        public static string Cprecio1val { get; set; }
        public static string Cprecio2val { get; set; }
        public static string Cprecio3val { get; set; }
        public static string Cprecio6val { get; set; }

        public static string sucursalViene { get; set; }

        public static string Ocultamos { get; set; }


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
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void PantallaProductos_Load(object sender, EventArgs e)
        {
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

            if (Ocultamos == "1")
            {
                materialLabel12.Visible = false;
                cantidad.Visible = false;
                materialLabel13.Visible = false;
                ExisMI.Visible = false;
            }
            else
            {
                materialLabel12.Visible = true;
                cantidad.Visible = true;
                materialLabel13.Visible = true;
                ExisMI.Visible = true;
            }
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
                codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
                precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
                peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
                descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
                descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();
                DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cliente"].Value.ToString();

                Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
                Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
                Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
                Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();
                this.Close();
            }else
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
            Cancelaste = "1";
            codigo = "";
            this.Close();
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
            CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
            NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();

            ExisMI.Text = "0";
            ExiMAT.Text = "0";
            exiLP.Text = "0";
            exiIGS.Text = "0";
            exiSP.Text = "0";
            exiPLA.Text = "0";
            exiCD.Text = "0";
            exiPPT.Text = "0";
            //Agregamos nuestros almacenes
            exiRF.Text = "0";
            exiAV.Text = "0";


            ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), sucursalViene).ToString();
            ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
            exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
            exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
            exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
            exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
            exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
            exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
            exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
            //Agregamos nuestros almacenes
            exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
            exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                try
                {
                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Cprecio1"].Value.ToString();

                    ExisMI.Text = "0";
                    ExiMAT.Text = "0";
                    exiLP.Text = "0";
                    exiIGS.Text = "0";
                    exiSP.Text = "0";
                    exiPLA.Text = "0";
                    exiCD.Text = "0";
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";
                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index+1].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();
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
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";

                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();

                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();

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
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";

                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();

                    ExisMI.Text =  existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();

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
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";

                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Cprecio1"].Value.ToString();

                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();

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
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";

                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();

                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();

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
                    exiPPT.Text = "0";
                    //Agregamos nuestros almacenes
                    exiRF.Text = "0";
                    exiAV.Text = "0";

                    CodigoPro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString();
                    NombrePro.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
                    PrecioPro.Text = "$ "+dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cprecio1"].Value.ToString();
                    ExisMI.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString(), sucursalViene).ToString();
                    ExiMAT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "MAT").ToString();
                    exiLP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "LP").ToString();
                    exiIGS.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "IGS").ToString();
                    exiSP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "SP").ToString();
                    exiPLA.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PLA").ToString();
                    exiCD.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "CD").ToString();
                    exiMP.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "TMP").ToString();
                    exiPPT.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "PPT").ToString();
                    //Agregamos nuestros almacenes
                    exiRF.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "RF").ToString();
                    exiAV.Text = existencia(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value.ToString(), "AG").ToString();
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
                        codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
                        nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nombre"].Value.ToString();
                        precio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["precio"].Value.ToString();
                        peso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
                        descuento1Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento1"].Value.ToString();
                        descuento2Val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descuento2"].Value.ToString();
                        DescuentoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cliente"].Value.ToString();

                        Cprecio1val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO1"].Value.ToString();
                        Cprecio2val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO2"].Value.ToString();
                        Cprecio3val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO3"].Value.ToString();
                        Cprecio6val = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CPRECIO6"].Value.ToString();
                        CantidadEnvio = cantidad.Text;
                        this.Close();
                    }else
                    {
                        MessageBox.Show("Agrega una cantidad");
                        cantidad.Focus();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {


                }
            }
        }
    }
}