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

namespace ReporteMRPendientes
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

                string sql = @"with pendiente as (  
                select serie, factura, codigo, facturado, sum(entregado) as entregados,
                facturado - sum(entregado) as PendientesEntrega, tipo, cliente, neto
                from matres
                where sucursal = @Sucursal
                group by serie, facturado, codigo, factura, tipo, cliente, neto)
                
                select pendiente.serie, pendiente.factura, pendiente.tipo, pendiente.codigo, pendiente.facturado, 
                pendiente.entregados, pendiente.pendientesEntrega, pendiente.cliente,
                (select top 1 fecent from matres where serie = pendiente.serie and factura = pendiente.factura order by fecent desc ) as fecent, pendiente.neto 
                from pendiente
                where pendientesEntrega > 0 order by factura
                ";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@sucursal", Form1.sucursal);
                
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteMRPendientes\ReportePDF.rpt");
                rd.SetDataSource(ds.Tables[0]);


                rd.SetParameterValue("FeHOY", DateTime.Now.ToString("dd/MM/yyyy"));
                rd.SetParameterValue("Sucursal", Form1.sucursal);
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