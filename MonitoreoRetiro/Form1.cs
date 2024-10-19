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
using Microsoft.Reporting.WinForms;

namespace MonitoreoRetiro
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            materialRadioButton1.Checked = true;
            btnEliminar.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void cargaCombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select sucursal, sucnom from folios where (idalmacen <= 6 and idalmacen !=0)", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "sucnom";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            sqlConnection1.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            cargarReporte();
        }

        private void cargarReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            S_Conn.Open();
            string query_1 = "";
            if (materialRadioButton1.Checked)
            {
                query_1 = @"select fecha, hora, folios.sucnom as sucursal, folio, monto, observa from retiros inner join folios on folios.sucursal = retiros.sucursal where fecha between '" + metroDateTime1.Value.ToString("MM/dd/yyyy") + "' and '" + metroDateTime2.Value.ToString("MM/dd/yyyy") + "' order by fecha, retiros.sucursal, hora";
            }
            else if (materialRadioButton2.Checked)
            {
                query_1 = @"select fecha, hora, folios.sucnom as sucursal, folio, monto, observa from retiros inner join folios on folios.sucursal = retiros.sucursal where fecha between '" + metroDateTime1.Value.ToString("MM/dd/yyyy") + "' and '" + metroDateTime2.Value.ToString("MM/dd/yyyy") + "' and folios.sucnom = '"+metroComboBox1.Text+"' order by fecha, retiros.sucursal, hora";
            }
            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            //ReportParameter[] parameters = new ReportParameter[1];

            //parameters[0] = new ReportParameter("Param1", "HOLA");
            //parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            //reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            metroComboBox1.Enabled = true;
            cargaCombo();
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            metroComboBox1.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
