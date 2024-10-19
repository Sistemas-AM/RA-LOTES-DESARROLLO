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

namespace Traspasos
{
    public partial class Reporte1Pdf : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        public Reporte1Pdf()
        {
            InitializeComponent();
            sqlConnection1.Open();
            string sql = Variablescompartidas.GeneralReporte;
            //where Folio ='" + regreso + "' and estatus = '" + estado + "'
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PARAMETROSP", Convert.ToString("ALGO"));
            //cmd.Parameters.AddWithValue("@PARAMETROSP2", Convert.ToString("ALGO"));

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\Rep1Pdf.rpt");
            rd.SetDataSource(ds.Tables[0]);


            rd.SetParameterValue("Suc",    Variablescompartidas.SucursalReporte);
            rd.SetParameterValue("Fecha1", Variablescompartidas.Fecha1);
            rd.SetParameterValue("Fecha2", Variablescompartidas.Fecha2);
            //DateTime.Now.ToString("HH:mm:ss")

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
            sqlConnection1.Close();

        }
    }
}
