using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ReporteVentasporSucursal
{
    public partial class Reporte : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public Reporte()
        {
            InitializeComponent();
            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spVentasSucursal", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(Variablescompartidas.fecha));
                cmd.Parameters.AddWithValue("@fecha2", Convert.ToString(Variablescompartidas.fecha2));
                cmd.Parameters.AddWithValue("@credito", Convert.ToString(Variablescompartidas.credito));
                cmd.Parameters.AddWithValue("@contado", Convert.ToString(Variablescompartidas.contado));
                cmd.Parameters.AddWithValue("@notas", Convert.ToString(Variablescompartidas.notas));
                
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);




                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteVentasporSucursal\repvensucu.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("nombresuc", Variablescompartidas.nombresuc);
                rd.SetParameterValue("fecha", Variablescompartidas.fecha);
                rd.SetParameterValue("Hora", DateTime.Now.ToShortTimeString());


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
