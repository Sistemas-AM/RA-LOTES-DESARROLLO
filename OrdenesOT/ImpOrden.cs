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

namespace OrdenesOT
{
    public partial class ImpOrden : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        public ImpOrden()
        {
            InitializeComponent();
            generar();
           
        }

        private void generar()
        {
            try
            {
                //Carga las ordenes
                sqlConnection1.Open();
                string sql2 = @"select DISTINCT CO.id_Operacion, CO.Descripcion as DescOpera, 
                Co.Codigo, CO.Supervisor, CO.id_LB, LB.Nombre, LB.Descripcion
                from cotizacionesOT as Doc
                left join cotizacionesDetallesOT as Mov on Doc.id_Documento = Mov.id_Documento
                left join ComponentesOT as compo on compo.id_mov = mov.id_mov
                left join Operaciones_Componentes as opera
                on opera.id_Componente = compo.id_Componente
                inner join Catalogo_Operaciones as CO
                inner join LaborTeams as LB on CO.id_Lb = LB.id_LB
                on opera.id_Operacion = CO.Id_Operacion
                where doc.id_documento =@Id";

                SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                cmd2.Parameters.AddWithValue("@Id", Form1.idImpresion);

                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(ds2);
                sqlConnection1.Close();

                //Carga toda la info
                sqlConnection1.Open();
                string sql = "select * from impresiones where id_documento = @Id";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Id", Form1.idImpresion);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                sqlConnection1.Close();



                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\OrdenesOT\ImprimeOrdenPDF.rpt");
                rd.SetDataSource(ds.Tables[0]);
                //rd.Subreports["OperacionesSub"].Database.Tables[0].SetDataSource(ds2.Tables[0]);
                ////rd.SetDataSource(ds.Tables[1]);
                //rd.SetDatabaseLogon("sa", "AdminSql7639!", "OrdenesTrabajo", "CotizacionesOT");



                rd.Database.Tables["DataTable1"].SetDataSource(ds.Tables[0]);
                rd.Database.Tables["DataTable2"].SetDataSource(ds2.Tables[0]); // Don't forget this line like I did!!

                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Show();
               

            }
            catch (Exception)
            {


            }
        }

        private void ImpOrden_Load(object sender, EventArgs e)
        {
           
        }
    }
}
