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

namespace ReporteDeVentasDiariasSucursales
{
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public Form2()
        {
            InitializeComponent();

            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spRepVentasSucursales", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechini", Convert.ToString(Form1.fecini));
                cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(Form1.fecfin));
                cmd.Parameters.AddWithValue("@notas", Convert.ToString(Form1.notas));
                cmd.Parameters.AddWithValue("@credito", Convert.ToString(Form1.credito));
                cmd.Parameters.AddWithValue("@contado", Convert.ToString(Form1.contado));

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteDeVentasDiariasSucursales\RepoPDF.rpt");
                rd.SetDataSource(ds.Tables[0]);


                rd.SetParameterValue("Sucursal", Form1.Sucursal);
                rd.SetParameterValue("FInicial", Form1.fecini);
                rd.SetParameterValue("FFinal", Form1.fecfin);

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
