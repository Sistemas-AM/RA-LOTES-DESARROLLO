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

namespace ReporteGeneralVentasUnidadesSucursales
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
                SqlCommand cmd = new SqlCommand("spVentaUnidadGeneralSucursales", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(Variablescompartidas.sucursal2));
                cmd.Parameters.AddWithValue("@fechini", Convert.ToString(Variablescompartidas.fecini));
                cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(Variablescompartidas.fecfin));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\ReportePDF.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("FInicial", Variablescompartidas.fecini);
                rd.SetParameterValue("FFinal", Variablescompartidas.fecfin);
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
