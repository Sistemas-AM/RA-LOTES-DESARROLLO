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

namespace DistribucionMaterialesSucursales
{
    public partial class sinexistencia : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public sinexistencia()
        {
            InitializeComponent();

            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spMatSucursales_copBIEN", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd")));
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
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\DistribucionMaterialesSucursales\reposinexistencia.rpt");
                rd.SetDataSource(ds.Tables[0]);
                DateTime hoy = DateTime.Now;
                rd.SetParameterValue("fecha", hoy.ToString("MM/dd/yyyy"));
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
