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

namespace EntregasPla
{
    public partial class Reporte : MaterialForm
    {
        public Reporte()
        {
            InitializeComponent();
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

        private void Reporte_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();

            cargarReporte();
        }

        private void cargarReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.Aceros);
            S_Conn.Open();
            string query_1 = "";
            query_1 = @"declare @idDocumento int
            declare @CalleFiscal nvarchar(80)
            declare @ciudadFiscal nvarchar(80)
            declare @cpFiscal nvarchar(80)
            declare @ColoniaFiscal nvarchar(80)
            declare @CalleEnvio nvarchar(80)
            declare @ciudadEnvio nvarchar(80)
            declare @cpEnvio nvarchar(80)
            declare @ColoniaEnvio nvarchar(80)
			declare @TelefonoEnvio nvarchar(80)
            
            set @idDocumento = '"+Form1.ciddocu+@"'
            set @CalleFiscal = (select CONCAT(CNOMBRECALLE,' ', CNUMEROEXTERIOR) from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 0);
            set @ciudadFiscal = (select CCIUDAD from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 0);
            set @cpFiscal = (select CCODIGOPOSTAL from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 0);
            set @ColoniaFiscal = (select CCOLONIA from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 0);
            
            set @CalleEnvio = (select CONCAT(CNOMBRECALLE,' ', CNUMEROEXTERIOR) from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 1);
            set @ciudadEnvio = (select CCIUDAD from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 1);
            set @cpEnvio = (select CCODIGOPOSTAL from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 1);
            set @ColoniaEnvio = (select CCOLONIA from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 1);
			set @TelefonoEnvio = (select CTELEFONO1 from admdomicilios where cidcatalogo = @idDocumento and ctipocatalogo = 3 and ctipodireccion = 1);
            
            SELECT 
            DOCU.CFOLIO,
            FORMAT( DOCU.CFECHA, 'dd/MM/yy') as CFECHA,
            DOCU.CIDCLIENTEPROVEEDOR,
            CLI.CCODIGOCLIENTE,
            DOCU.CRAZONSOCIAL,
            DOCU.CRFC,
            
            DOCU.CDESTINATARIO,
            DOCU.CNUMEROCAJAS,
            DOCU.CNUMEROGUIA,
            DOCU.CMENSAJERIA,
            DOCU.CIDCONCEPTODOCUMENTO,
            CONCE.CNOMBRECONCEPTO,
            entregas.id,
            @CalleFiscal as calleFiscal,
            @CiudadFiscal as CiudadFiscal,
            @cpFiscal as cp_Fiscal,
            @ColoniaFiscal as ColoniaFiscal,
            
            
            @CalleEnvio as calleEnvio,
            @CiudadEnvio as CiudadEnvio,
            @cpEnvio as cp_Envio,
            @ColoniaEnvio as ColoniaEnvio,
			@TelefonoEnvio as TelefonoEnvio,
            
            MOV.CIDPRODUCTO,
            PRODU.CCODIGOPRODUCTO,
            PRODU.CNOMBREPRODUCTO,
            ALM.CCODIGOALMACEN,
            MOV.CIDALMACEN,
            ALM.CNOMBREALMACEN,
            MOV.CUNIDADES,
            PRODU.CPRECIO10 AS PESO,
            (MOV.CUNIDADES * PRODU.CPRECIO10) AS PESO_TOTAL,
            DOCU.COBSERVACIONES,
            FORMAT( GETDATE(), 'dd/MM/yy') as FECHAACTUAL
            FROM admDocumentos AS DOCU
            INNER JOIN ADMCLIENTES AS CLI ON DOCU.CIDCLIENTEPROVEEDOR 
            = CLI.CIDCLIENTEPROVEEDOR
            INNER JOIN admMovimientos AS MOV 
            ON DOCU.CIDDOCUMENTO = MOV.CIDDOCUMENTO
            INNER JOIN admProductos AS PRODU
            ON PRODU.CIDPRODUCTO = MOV.CIDPRODUCTO
            INNER JOIN admAlmacenes AS ALM
            ON MOV.CIDALMACEN = ALM.CIDALMACEN
            INNER JOIN admConceptos AS CONCE
            ON DOCU.CIDCONCEPTODOCUMENTO = CONCE.CIDCONCEPTODOCUMENTO
            INNER JOIN REPORTESAMSA.DBO.ENTREGAS_PLANTA AS Entregas
            on Entregas.iddocumento = Docu.CIDDOCUMENTO
            WHERE DOCU.CIDDOCUMENTO =@idDocumento";

            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            //ReportParameter[] parameters = new ReportParameter[1];

            ////parameters[0] = new ReportParameter("Param1", "HOLA");
            //parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            //reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();
        }
    }
}
