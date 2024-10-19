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

namespace ReporteDescuentos
{
    public partial class RepSucursal : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        public RepSucursal()
        {
            InitializeComponent();

            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spDescuentosGeneral", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechini", Convert.ToString(VariablesCompartidas.fechini));
                cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fechfin));
                cmd.Parameters.AddWithValue("@idcredito", Convert.ToString(VariablesCompartidas.idcredito));
                cmd.Parameters.AddWithValue("@idcontado", Convert.ToString(VariablesCompartidas.idcontado));
                cmd.Parameters.AddWithValue("@idnotas", Convert.ToString(VariablesCompartidas.inotas));
                cmd.Parameters.AddWithValue("@tipo", Convert.ToString(2));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteDescuentos\DescuentosSuc.rpt");
                rd.SetDataSource(ds.Tables[0]);

                rd.SetParameterValue("FInicial", VariablesCompartidas.fechini);
                rd.SetParameterValue("FFinal", VariablesCompartidas.fechfin);
                rd.SetParameterValue("sucursal", VariablesCompartidas.sucursal);

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
