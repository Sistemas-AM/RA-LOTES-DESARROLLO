using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SurtidoAccesorios
{
    public partial class sinexis : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public sinexis()
        {
            InitializeComponent();

            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spSurtidoBIEN", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy")));
                cmd.Parameters.AddWithValue("@v1", Convert.ToString(0));
                cmd.Parameters.AddWithValue("@v2", Convert.ToString(0));
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(Variablescompartidas.sucursal));
                cmd.Parameters.AddWithValue("@tipo", Convert.ToString(4));
                cmd.Parameters.AddWithValue("@Ejercicio", Principal.Variablescompartidas.Ejercicio);
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);




                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\SurtidoAccesorios\reportesinexistencia.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("sucursal", Variablescompartidas.name);


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
