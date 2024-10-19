using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteCotizaciones
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection RepoAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);

        public Form1()
        {
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            InitializeComponent();

            btn_generar.BackColor = ColorTranslator.FromHtml("#76CA62"); //VERDE
            metroRadioButton4.Checked = true;
            metroComboBox2.Text = "A";
            //btn_generar1.BackColor = ColorTranslator.FromHtml("#76CA62");
            //btn_buscar.BackColor = ColorTranslator.FromHtml("#95B0BD"); //AZUL CLARO
            button1.BackColor = ColorTranslator.FromHtml("#D66F6F"); //ROJO
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cargarCombo();

            //if (Principal.Variablescompartidas.sucural == "AUDITOR")
            //{
            //    metroComboBox1.Enabled = true;
            //}
            //else
            //{
            //    metroRadioButton4.Checked = false;
            //    metroRadioButton4.Visible = false;
            //    metroRadioButton3.Checked = true;
            //    metroComboBox1.Enabled = false;
            //    metroComboBox1.Text = Principal.Variablescompartidas.sucural;
            //}

            this.reportViewer1.RefreshReport();
        }

        private void btn_generar_Click(object sender, EventArgs e)
        {
            cargarReporte();
        }

        private void cargarCombo()
        {
            RepoAmsa.Open();
            SqlCommand sc = new SqlCommand("select sucnom from folios where idalmacen != 0", RepoAmsa);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "sucnom";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            RepoAmsa.Close();
        }
        private void cargarReporte()
        {


            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            S_Conn.Open();
            string query_1 = "";
            if (metroRadioButton3.Checked)
            {
                query_1 = @"select FolioCotizacion ,FolioCot,folios.sucnom as Sucursal,codcliente as CodigoCliente,admclientes.crazonsocial as nombreCliente,
                       Importe, Estatus,isnull(admclientes.ctextoextra1,'') as Segmento,Fecha from CotizacionesOT
                        left join adAMSACONTPAQi.dbo.admclientes on CotizacionesOT.idcliente = admclientes.cidclienteproveedor
                        inner join folios on CotizacionesOT.Sucursal = folios.sucursal
                        where fecha between '" + FechaInicio.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + @"'and'" + FechaFin.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + @"'
                        and folios.sucnom = '" + metroComboBox1.Text + "' and estatus = '"+metroComboBox2.Text+"'";
            }
            else if (metroRadioButton4.Checked)
            {
                query_1 = @"select FolioCotizacion ,FolioCot,folios.sucnom as Sucursal,codcliente as CodigoCliente,admclientes.crazonsocial as nombreCliente,
                        Importe, Estatus,isnull(admclientes.ctextoextra1,'') as Segmento,Fecha from CotizacionesOT
                        left join adAMSACONTPAQi.dbo.admclientes on CotizacionesOT.idcliente = admclientes.cidclienteproveedor
                        inner join folios on CotizacionesOT.Sucursal = folios.sucursal
                        where fecha between '" + FechaInicio.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + @"'and'" + FechaFin.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + @"' and estatus = '" + metroComboBox2.Text + "'";
            }
            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            reportViewer1.RefreshReport();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //if (metroRadioButton3.Checked)
            //{
            //    if (Principal.Variablescompartidas.sucural == "AUDITOR")
            //    {
            //        metroComboBox1.Enabled = true;
            //    }
            //    else
            //    {
            //        metroRadioButton4.Checked = false;
            //        metroComboBox1.Enabled = false;
            //        metroComboBox1.Text = Principal.Variablescompartidas.sucural;
            //    }
            //}
            //else
            //{
            //    metroComboBox1.Enabled = false;
            //}

            cargarCombo();

            metroComboBox1.Enabled = true;
        }

        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            metroComboBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
