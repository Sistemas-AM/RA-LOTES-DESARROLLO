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

namespace ReporteRelaciones
{
    public partial class Form1 : MaterialForm
    {

        SqlConnection FlotillasConection = new SqlConnection(Principal.Variablescompartidas.Flotillas);
        SqlConnection Aceros = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection RepoAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
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
            metroRadioButton4.Checked = true;
            cargarCombo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void cargarCombo()
        {
            RepoAmsa.Open();
            SqlCommand sc = new SqlCommand("select idalmacen, sucnom from folios where idalmacen != 0", RepoAmsa);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "idalmacen";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            RepoAmsa.Close();
        }

        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (metroRadioButton4.Checked)
            {
                metroComboBox1.Enabled = false;
            }
        }

        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            metroComboBox1.Enabled = true;
        }

        private void cargaReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.Aceros);
            S_Conn.Open();
            string query_1 = "";
            if (metroRadioButton4.Checked)
            {
                query_1 = @"with Consulta1 as (
                 select 
                 doc.CIDDOCUMENTO,
                 doc.CIDCONCEPTODOCUMENTO,
                 doc.cfolio, 
                 doc.CFECHA, 
                 doc.CRAZONSOCIAL,
                 doc.CTOTAL,
                 FD.CIDDOCTO,
                 FD.CFOLIO as fol2, 
                 FD.CUSUBAN02,
                 CAST(FD.CCADPEDI as nvarchar(100)) as CCADPEDI,
                 doc.CIDDOCUMENTODE,
                 doc.cseriedocumento,
                 mov.CIDALMACEN
                 from admdocumentos as doc
                 inner join admFoliosDigitales as FD on doc.CIDDOCUMENTO = FD.CIDDOCTO
                 left join admmovimientos as mov on doc.CIDDOCUMENTO = mov.CIDDOCUMENTO
                 where (doc.ciddocumentode = 3 or doc.CIDDOCUMENTODE = 4 or doc.CIDDOCUMENTODE = 5 or doc.CIDDOCUMENTODE = 7) and FD.CUSUBAN02 != ''
                 group by 
                 doc.CIDDOCUMENTO,
                 doc.CFOLIO,
                 doc.CFECHA, 
                 doc.CRAZONSOCIAL,
                 doc.CIDCONCEPTODOCUMENTO,
                 doc.CTOTAL,
                 FD.CIDDOCTO,
                 FD.CFOLIO,
                 FD.CUSUBAN02,
                 CAST(FD.CCADPEDI as nvarchar(100)),
                 doc.CIDDOCUMENTODE,
                 doc.cseriedocumento,
                 mov.CIDALMACEN
                 ),
                 
                 consulta2 as (select CIDDOCTO ,CFOLIO, CFECHAEMI, CUUID, CUSUBAN02, CSERIE from admFoliosDigitales)
                 
                 select 
                 Consulta1.CFOLIO as Folio,
                 consulta1.cidalmacen,
                 admAlmacenes.CNOMBREALMACEN,
                 Consulta1.cseriedocumento,
                 admConceptos.CNOMBRECONCEPTO,
                 Consulta1.CFECHA as Fecha,
                 Consulta1.CIDDOCUMENTODE,
                 consulta1.crazonsocial as NombreCliente,
                 consulta1.ctotal as ImporteTotal,
                 consulta1.CUSUBAN02 as TipoRelacion,
                 consulta2.CFOLIO as FolioRelacion,
                 consulta2.CSERIE,
                 consulta2.CFECHAEMI as Fecha2,
                 admdocumentos.ctotal as Importe2
                 from Consulta1 inner join consulta2 on CAST(Consulta1.CCADPEDI as nvarchar(100)) = CAST(consulta2.CUUID as nvarchar(100))
                 inner join admDocumentos on consulta2.CIDDOCTO = admDocumentos.CIDDOCUMENTO
                 left join admAlmacenes on Consulta1.CIDALMACEN = admAlmacenes.CIDALMACEN
                 inner join admconceptos on consulta1.cidconceptodocumento = admConceptos.CIDCONCEPTODOCUMENTO
                 where consulta1.CFECHA between '"+metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha)+ "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' order by Consulta1.CFECHA";
            }
            else if (metroRadioButton3.Checked)
            {
                query_1 = @"with Consulta1 as (
                 select 
                 doc.CIDDOCUMENTO,
                 doc.CIDCONCEPTODOCUMENTO,
                 doc.cfolio, 
                 doc.CFECHA, 
                 doc.CRAZONSOCIAL,
                 doc.CTOTAL,
                 FD.CIDDOCTO,
                 FD.CFOLIO as fol2, 
                 FD.CUSUBAN02,
                 CAST(FD.CCADPEDI as nvarchar(100)) as CCADPEDI,
                 doc.CIDDOCUMENTODE,
                 doc.cseriedocumento,
                 mov.CIDALMACEN
                 from admdocumentos as doc
                 inner join admFoliosDigitales as FD on doc.CIDDOCUMENTO = FD.CIDDOCTO
                 left join admmovimientos as mov on doc.CIDDOCUMENTO = mov.CIDDOCUMENTO
                 where (doc.ciddocumentode = 3 or doc.CIDDOCUMENTODE = 4 or doc.CIDDOCUMENTODE = 5 or doc.CIDDOCUMENTODE = 7) and FD.CUSUBAN02 != ''
                 group by 
                 doc.CIDDOCUMENTO,
                 doc.CFOLIO,
                 doc.CFECHA, 
                 doc.CRAZONSOCIAL,
                 doc.CIDCONCEPTODOCUMENTO,
                 doc.CTOTAL,
                 FD.CIDDOCTO,
                 FD.CFOLIO,
                 FD.CUSUBAN02,
                 CAST(FD.CCADPEDI as nvarchar(100)),
                 doc.CIDDOCUMENTODE,
                 doc.cseriedocumento,
                 mov.CIDALMACEN
                 ),
                 
                 consulta2 as (select CIDDOCTO ,CFOLIO, CFECHAEMI, CUUID, CUSUBAN02, CSERIE from admFoliosDigitales)
                 
                 select 
                 Consulta1.CFOLIO as Folio,
                 consulta1.cidalmacen,
                 admAlmacenes.CNOMBREALMACEN,
                 Consulta1.cseriedocumento,
                 admConceptos.CNOMBRECONCEPTO,
                 Consulta1.CFECHA as Fecha,
                 Consulta1.CIDDOCUMENTODE,
                 consulta1.crazonsocial as NombreCliente,
                 consulta1.ctotal as ImporteTotal,
                 consulta1.CUSUBAN02 as TipoRelacion,
                 consulta2.CFOLIO as FolioRelacion,
                 consulta2.CSERIE,
                 consulta2.CFECHAEMI as Fecha2,
                 admdocumentos.ctotal as Importe2
                 from Consulta1 inner join consulta2 on CAST(Consulta1.CCADPEDI as nvarchar(100)) = CAST(consulta2.CUUID as nvarchar(100))
                 inner join admDocumentos on consulta2.CIDDOCTO = admDocumentos.CIDDOCUMENTO
                 left join admAlmacenes on Consulta1.CIDALMACEN = admAlmacenes.CIDALMACEN
                 inner join admconceptos on consulta1.cidconceptodocumento = admConceptos.CIDCONCEPTODOCUMENTO
                 where consulta1.CFECHA between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and admalmacenes.cidalmacen = '"+metroComboBox1.SelectedValue+ "' order by Consulta1.CFECHA";
            }


            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            //ReportParameter[] parameters = new ReportParameter[1];

            //parameters[0] = new ReportParameter("letra", Form1.Letra);
            ////parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            ////reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            cargaReporte();
        }
    }

}
