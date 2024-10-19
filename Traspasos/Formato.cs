using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class Formato : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();

        public Formato()
        {
            InitializeComponent();

            //try
            //{
            string estado = "";
            string regreso = "";

            if (Form1.Devolucion == "1")
            {
                estado = "D";
                regreso = Form1.foliodevol;
            }else
            {
                estado = "T";
                regreso = Form1.foliocom;
            }
                sqlConnection1.Open();
                string sql = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where Folio ='" + regreso+"' and estatus = '"+estado+"'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PARAMETROSP", Convert.ToString("ALGO"));
                //cmd.Parameters.AddWithValue("@PARAMETROSP2", Convert.ToString("ALGO"));
                
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\TrapasoPDF.rpt");
                rd.SetDataSource(ds.Tables[0]);


                rd.SetParameterValue("Serie", Form1.sucursalcom);
                rd.SetParameterValue("Sucursal", Form1.SucursalNomCom);
                rd.SetParameterValue("Fecha", DateTime.Now.ToShortDateString());
                //DateTime.Now.ToString("HH:mm:ss")

                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Show();
                sqlConnection1.Close();
            //}
            //catch (Exception)
            //{


            //}

        }
    }
}
