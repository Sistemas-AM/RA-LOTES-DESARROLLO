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

namespace Cotizacion
{
    public partial class ReporteCotizaciones : Form
    {
        public ReporteCotizaciones()
        {
            InitializeComponent();
        }

        private void ReporteCotizaciones_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
           // cargaReporte();
        }

        private void cargaReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            S_Conn.Open();
            string query_1 = "";

            query_1 = @"select folio, fecha, TRIM(cliente) as cliente, TRIM(nombre) as nombre, sum(importe) as total_Importe from bdcotizao
            where fecha between '"+dateTimePicker1.Value.ToString(Principal.Variablescompartidas.FormatoFecha)+"' and '"+dateTimePicker2.Value.ToString(Principal.Variablescompartidas.FormatoFecha)+"' group by folio, fecha, cliente, nombre";
               
            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            //ReportParameter[] parameters = new ReportParameter[1];

            //parameters[0] = new ReportParameter("letra", Form1.Letra);
            ////parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            ////reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            //reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargaReporte();
        }
    }
}