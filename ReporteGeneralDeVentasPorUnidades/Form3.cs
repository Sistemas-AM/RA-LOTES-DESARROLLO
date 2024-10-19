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

namespace ReporteGeneralDeVentasPorUnidades
{
    public partial class Form3 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public Form3()
        {
            InitializeComponent();
            //try
            //{

                if (VariablesCompartidas.accesorios == "1")
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand(VariablesCompartidas.sp, sqlConnection1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechini", Convert.ToString(VariablesCompartidas.fecini));
                    cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                    cmd.Parameters.AddWithValue("@familia", Convert.ToString(VariablesCompartidas.familia));
                    //SqlDataReader dr = cmd.ExecuteReader();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralDeVentasPorUnidades\repFamilia.rpt");
                    rd.SetDataSource(ds.Tables[0]);

                    rd.SetParameterValue("FInicial", VariablesCompartidas.fecini);
                    rd.SetParameterValue("FFinal", VariablesCompartidas.fecfin);
                    rd.SetParameterValue("Cambio", "Kilos");
                    //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
                    //DateTime.Now.ToString("HH:mm:ss")

                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.Show();
                    sqlConnection1.Close();
                }
                else
                {
                    sqlConnection1.Open();
                    SqlCommand cmd = new SqlCommand("spVentaUnidadFamilia", sqlConnection1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechini", Convert.ToString(VariablesCompartidas.fecini));
                    cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                    cmd.Parameters.AddWithValue("@familia", Convert.ToString(VariablesCompartidas.familia));
                    //SqlDataReader dr = cmd.ExecuteReader();

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralDeVentasPorUnidades\repFamilia.rpt");
                    rd.SetDataSource(ds.Tables[0]);

                    rd.SetParameterValue("FInicial", VariablesCompartidas.fecini);
                    rd.SetParameterValue("FFinal", VariablesCompartidas.fecfin);
                    rd.SetParameterValue("Cambio", "Kilos");

                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.Show();
                    sqlConnection1.Close();
                }




                
            //}
            //catch (Exception)
            //{


            //}
        }
    }
}
