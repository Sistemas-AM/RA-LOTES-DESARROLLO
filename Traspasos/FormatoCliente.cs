using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class FormatoCliente : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();

        public FormatoCliente()
        {
            InitializeComponent();
            string estado = "";
            string regreso = "";

            if (Form1.Devolucion == "1")
            {
                estado = "D";
                regreso = Form1.foliodevol;
            }
            else
            {
                estado = "T";
                regreso = Form1.foliocom;
            }
            sqlConnection1.Open();
            string sql = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where Folio ='" + regreso + "' and estatus = '" + estado + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\RepCliente.rpt");
            rd.SetDataSource(ds.Tables[0]);


            rd.SetParameterValue("Folio", regreso);
            rd.SetParameterValue("Placa", Form1.SucursalNomCom);
            rd.SetParameterValue("Fecha", DateTime.Now.ToShortDateString());
            rd.SetParameterValue("Cliente", Variablescompartidas.nombreCliente);
            rd.SetParameterValue("Direccion", Variablescompartidas.CalleCliente + " "+Variablescompartidas.NumeroCliente + " " + Variablescompartidas.Coloniacliente);
           

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
            sqlConnection1.Close();
        }
    }
}
