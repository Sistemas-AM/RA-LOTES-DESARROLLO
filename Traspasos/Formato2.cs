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

namespace Traspasos
{
    public partial class Formato2 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();

        public Formato2()
        {
            InitializeComponent();

            sqlConnection1.Open();
            string sql = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where Folio ='" + Form1.foliocom + "' and estatus = 'T'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PARAMETROSP", Convert.ToString("ALGO"));
            //cmd.Parameters.AddWithValue("@PARAMETROSP2", Convert.ToString("ALGO"));

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\Vacio.rpt");
            rd.SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("Serie", Form1.sucursalcom);
            rd.SetParameterValue("Sucursal", "Traspaso " + Form1.SucursalNomCom);
            rd.SetParameterValue("Fecha", DateTime.Now.ToShortDateString());
            rd.SetParameterValue("Folio", Form1.foliocom);

            rd.SetParameterValue("Chofer", Form1.Chofer2);
            rd.SetParameterValue("Carro",  Form1.Carro2);
            rd.SetParameterValue("Placas", Form1.Placas2);

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
        }
    }
}
