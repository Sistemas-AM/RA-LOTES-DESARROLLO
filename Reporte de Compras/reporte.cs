using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reporte_de_Compras
{
    public partial class reporte : Form
    {
        
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public reporte()
        {
            InitializeComponent();
            try
            {
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spCobranzas", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", Convert.ToString(VariablesCompartidas.codigo));
                cmd.Parameters.AddWithValue("@fecini", DateTime.Parse(VariablesCompartidas.fecini).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@fecfin", DateTime.Parse(VariablesCompartidas.fecfin).ToString("yyyy-MM-dd"));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);




                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+ @"\Reporte de Compras\ReporteCobranza.rpt");
                rd.SetDataSource(ds.Tables[0]);
                rd.SetParameterValue("FInicial", DateTime.Parse(VariablesCompartidas.fecini).ToString("yyyy-MM-dd"));
                rd.SetParameterValue("FFinal", DateTime.Parse(VariablesCompartidas.fecfin).ToString("yyyy-MM-dd"));
                rd.SetParameterValue("Existencia", VariablesCompartidas.existencia);
                //rd.SetParameterValue("FeHOY", DateTime.Now.ToString("dd/MM/yyyy"));
                //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
                //DateTime.Now.ToString("HH:mm:ss")

                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Show();
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
        }
    }
}
