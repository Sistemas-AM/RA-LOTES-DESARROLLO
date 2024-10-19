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

namespace Repo2
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
                SqlCommand cmd = new SqlCommand("spCortePlanta", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(Variablescompartidas.fecha));
                cmd.Parameters.AddWithValue("@fecha2", Convert.ToString(Variablescompartidas.fecha2));
                cmd.Parameters.AddWithValue("@credito", Convert.ToString(Variablescompartidas.credito));
                cmd.Parameters.AddWithValue("@contado", Convert.ToString(Variablescompartidas.contado));
                //cmd.Parameters.AddWithValue("@notas", Convert.ToString(Variablescompartidas.notas));

                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);




                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Repo2\PDFRepo.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("nombresuc", Variablescompartidas.nombresuc);
                rd.SetParameterValue("fecha", Variablescompartidas.fecha);
                rd.SetParameterValue("fecha2", Variablescompartidas.fecha2);


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
