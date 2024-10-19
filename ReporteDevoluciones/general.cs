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

namespace ReporteDevoluciones
{
    public partial class general : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public general()
        {
            InitializeComponent();
            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spDevolucionesGeneral", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechini", Convert.ToString(VariablesCompartidas.fecini));
                cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);




                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteDevoluciones\DevolucionesGeneral.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("FInicial", VariablesCompartidas.fecini);
                rd.SetParameterValue("FFinal", VariablesCompartidas.fecfin);
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
