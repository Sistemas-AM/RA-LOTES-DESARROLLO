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

namespace repoInv
{
    public partial class reporte : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public reporte()
        {
            InitializeComponent();
            try
            {
                DateTime hoy = DateTime.Now;

                string fecha = hoy.ToString("MM/dd/yyyy");
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spInventario", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(fecha));
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(repoInv.Form1.Variablescompartidas.almacen));


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\repoInv\RepoInvPDF.rpt");
                rd.SetDataSource(ds.Tables[0]);


                rd.SetParameterValue("FeHOY", DateTime.Now.ToString("MM/dd/yyyy"));
                rd.SetParameterValue("Almacen", repoInv.Form1.Variablescompartidas.nombre);
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
