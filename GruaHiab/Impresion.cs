using Microsoft.Reporting.WinForms;
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

using MaterialSkin.Controls;
using MaterialSkin;

namespace GruaHiab
{
    public partial class Impresion : MaterialForm
    {
        public Impresion()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void Impresion_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
            cargarReporte();
            //this.reportViewer1.RefreshReport();
        }

        private void cargarReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            S_Conn.Open();
            string query_1 = "";
            query_1 = @"select * from dbSurtido where folio = '" + Form1.folipasa + "'";
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
            reportViewer1.Visible = true;

            reportViewer1.RefreshReport();
        }
    }
}
