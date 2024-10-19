using CrystalDecisions.CrystalReports.Engine;
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

namespace Reporte_de_Cobranza
{
    public partial class Form3 : Form
    {
        public DateTime fechainicial;
        public DateTime fechafinal;
        public Form3(DateTime fechainicial, DateTime fechafinal)
        {
            InitializeComponent();
            this.fechafinal = fechafinal;
            this.fechainicial = fechainicial;
            loadReport();
        }
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        private void loadReport()
        {
            //sqlConnection1.Open();
            //SqlCommand cmd = new SqlCommand("pagos", sqlConnection1);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@fecini", Convert.ToString(fechainicial));
            //cmd.Parameters.AddWithValue("@fecfin", Convert.ToString(fechafinal));
            //SqlDataReader dr = cmd.ExecuteReader();

            //DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(ds);



            //ReportDocument rd = new ReportDocument();
            //rd.Load(@"C:\Users\Sistemas Practicas 2\Documents\Visual Studio 2015\Projects\ReportesAMSA\Reporte de Cobranza\ReporteCobranza.rpt");
            //rd.SetDataSource(ds.Tables[0]);

            //crystalReportViewer1.ReportSource = rd;
            //crystalReportViewer1.Refresh();

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
