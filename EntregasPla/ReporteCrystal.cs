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

namespace EntregasPla
{
    public partial class ReporteCrystal : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();

        public ReporteCrystal()
        {
            InitializeComponent();

            try
            {

                sqlConnection2.Open();
                string sql = @"select *, case when ccancelado = 0 then '' WHEN CCANCELADO = 1 THEN 'CANCELADO' ELSE '' end as CCANCELADO2  from entregas_planta where idDocumento = @IdDocu";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                cmd.Parameters.AddWithValue("@IdDocu", Form1.ciddocu);


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\EntregasPla\EntregaPDF.rpt");
                rd.SetDataSource(ds.Tables[0]);


                rd.SetParameterValue("ImagenCodigo", Form1.BarrasPasa);
                //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
                //DateTime.Now.ToString("HH:mm:ss")

                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Show();
                sqlConnection2.Close();
            }
            catch (Exception)
            {


            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
