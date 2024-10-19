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

namespace ReportesTraspasos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
            string estado = "";
            string regreso = "";

            if (variablescompartidas.devolucion == "1")
            {
                estado = "D";
                regreso = variablescompartidas.foliodevol;
            }
            else
            {
                estado = "T";
                regreso = variablescompartidas.foliocom;
            }
            sqlConnection1.Open();
            string sql = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where Folio ='110-T' and estatus = 'T'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PARAMETROSP", Convert.ToString("ALGO"));
            //cmd.Parameters.AddWithValue("@PARAMETROSP2", Convert.ToString("ALGO"));

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReportesTraspasos\TrapasoPDF.rpt");
            rd.SetDataSource(ds.Tables[0]);


            rd.SetParameterValue("Serie", "Prueba"/*variablescompartidas.sucursalcom*/);
            rd.SetParameterValue("Sucursal", "Prueba"/*variablescompartidas.SucursalNomCom*/);
            rd.SetParameterValue("Fecha", "Prueba"/*DateTime.Now.ToShortDateString()*/);
            //DateTime.Now.ToString("HH:mm:ss")

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
            sqlConnection1.Close();
        }
    }
}
