using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteVentasSucursal
{
    
    public partial class Reporte : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        public Reporte()
        {
            InitializeComponent();
            try
            {
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("select idCobranza, sucursal,  cfecha , cseriedo01, cfolio, crazonso01, ctotal, case when original = 1 then 'S' else 'N' end as original, case when cr = 1 then 'S' else 'N' end as cr, ccodigoc01, orden, fecord, case when firmada = 1 then 'S' else 'N' end as firmada from cobranza where sucursal = '" + Variablescompartidas.sucursal + "' and cfecha = '" + Variablescompartidas.fecha + "'", sqlConnection1);
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DataSet ds2 = new DataSet();
                SqlCommand cmd2 = new SqlCommand("select factura, proveedor, importe from cobrafac where sucursal = '" + Variablescompartidas.sucursal + "' and fecha = '" + Variablescompartidas.fecha + "'",sqlConnection1);
                cmd2.CommandType = CommandType.Text;


                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(ds2);


                DataSet ds3 = new DataSet();
                SqlCommand cmd3 = new SqlCommand("select sucursal, fecha, factura, proveedor, cheque, efectivo, tc, td, transfer from faccob where sucursal = '" + Variablescompartidas.sucursal + "' and fecha = '" + Variablescompartidas.fecha + "'", sqlConnection1);
                cmd3.CommandType = CommandType.Text;


                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(ds3);



                DataSet ds4 = new DataSet();
                SqlCommand cmd4 = new SqlCommand("select factura, proveedor, importe from cobraant where sucursal = '" + Variablescompartidas.sucursal + "' and fecha = '" + Variablescompartidas.fecha + "'", sqlConnection1);
                cmd4.CommandType = CommandType.Text;


                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(ds4);

                //select factura, proveedor, importe from cobrafac where sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'"
                ReportDocument rd = new ReportDocument();
                rd.Load(System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteVentasSucursal\CrystalReport1.rpt");
                rd.Subreports["cobranza"].Database.Tables[0].SetDataSource(ds.Tables[0]);
                rd.Subreports["cobrafac"].Database.Tables[0].SetDataSource(ds2.Tables[0]);
                rd.Subreports["faccob"].Database.Tables[0].SetDataSource(ds3.Tables[0]);
                rd.Subreports["cobraant"].Database.Tables[0].SetDataSource(ds4.Tables[0]);
                //rd.SetDataSource(ds.Tables[1]);

                rd.SetParameterValue("fecha", Variablescompartidas.fecha);
                rd.SetParameterValue("sucursal", Variablescompartidas.sucursal);

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
