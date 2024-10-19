using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace ImpresionMasiva
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void cargaReporte()
        {
            //SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.Aceros);
            //S_Conn.Open();
            //string query_1 = "";

            //query_1 = @"select 
            //    CSERIEDOCUMENTO as Serie,
            //    CFOLIO as Folio, 
            //    cli.crazonsocial as cliente, 
            //    cli.CRFC as RFC,
            //    doc.CFECHA as Fecha, 
            //    pro.CCODIGOPRODUCTO as Codigo,
            //    pro.CNOMBREPRODUCTO as Nombre, 
            //    mov.CUNIDADES,
            //    mov.CTOTAL as Total,
            //    pro.CPRECIO10 * mov.CUNIDADES as pesototal
            //    from admdocumentos as doc 
            //    inner join admmovimientos as mov on doc.CIDDOCUMENTO = mov.CIDDOCUMENTO
            //    inner join admproductos as pro on pro.cidproducto = mov.cidproducto
            //    inner join admClientes as cli on cli.CIDCLIENTEPROVEEDOR = doc.CIDCLIENTEPROVEEDOR
            //    where doc.CFECHA between '11/01/2020' and '11/30/2020'";



            //SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            //SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            //DataSet1 Data_Set = new DataSet1();
            //Data_Adapter.Fill(Data_Set);
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            //this.reportViewer1.RefreshReport();

            ////ReportParameter[] parameters = new ReportParameter[1];

            ////parameters[0] = new ReportParameter("letra", Form1.Letra);
            //////parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            //////reportViewer1.LocalReport.EnableExternalImages = true;
            ////reportViewer1.LocalReport.SetParameters(parameters);

            ////reportViewer1.Visible = true;
            //reportViewer1.RefreshReport();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

            //cargaReporte();
            try
            {

                sqlConnection1.Open();
                string sql = @"select 
CSERIEDOCUMENTO as Serie,
CAST(CFOLIO as nvarchar) as Folio, 
cli.crazonsocial as cliente, 
doc.CFECHA as Fecha, 
pro.CCODIGOPRODUCTO as Codigo,
pro.CNOMBREPRODUCTO as Nombre, 
mov.CUNIDADES,
mov.CTOTAL as Total,
pro.CPRECIO10* mov.CUNIDADES as pesototal,
CAST(CFOLIO as nvarchar) +'-'+Cli.CRazonSocial as resu
from admdocumentos as doc
inner join admmovimientos as mov on doc.CIDDOCUMENTO = mov.CIDDOCUMENTO
inner join admproductos as pro on pro.cidproducto = mov.cidproducto
inner join admClientes as cli on cli.CIDCLIENTEPROVEEDOR = doc.CIDCLIENTEPROVEEDOR
where doc.CFECHA between '11/01/2020' and '11/30/2020'
";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
                //cmd.Parameters.AddWithValue("@param2", row.Cells["Column1"].Value.ToString()); //Para grabar una columna

                //cmd.Parameters.AddWithValue("@fecini", DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy"));
                //cmd.Parameters.AddWithValue("@fecfin", DateTime.Parse(dateTimePicker2.Text).ToString("MM/dd/yyyy"));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ImpresionMasiva\Notas.rpt");
                rd.SetDataSource(ds.Tables[0]);
                //rd.SetParameterValue("FInicial", dateTimePicker1.Text);
                //rd.SetParameterValue("FFinal", dateTimePicker2.Text);
                //rd.SetParameterValue("FeHOY", DateTime.Now.ToString("MM/dd/yyyy"));
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
