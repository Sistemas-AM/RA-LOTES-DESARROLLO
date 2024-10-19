using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntregasPla
{
    public partial class Entregas : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        public static string idPasa { get; set; }

        public Entregas()
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

            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void Entregas_Load(object sender, EventArgs e)
        {
            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");
            SqlCommand cmd = new SqlCommand(@"select idDocumento, CNOMBRECONCEPTO as Concepto,
EP.CFOLIO as folio, EP.CFECHA as Fecha, FECHAACTUAL as Fecha_Entrega,
EP.CRAZONSOCIAL, EP.CRFC,
CASE WHEN
Docu.CCANCELADO = 0 then '' 
when Docu.CCANCELADO = 1 then 'CANCELADO'
end as CCANCELADO
from Entregas_Planta as EP
INNER JOIN adAMSACONTPAQi.dbo.admDocumentos as Docu
on EP.idDocumento = Docu.CIDDOCUMENTO
group by idDocumento, CNOMBRECONCEPTO,
EP.CFOLIO, EP.CFECHA, FECHAACTUAL,
EP.CRAZONSOCIAL, EP.CRFC, Docu.CCANCELADO", sqlConnection2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection2.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Documento"].Value.ToString();
            Desgloce_Entregas.esEntrega = "1";
            using (Desgloce_Entregas DE = new Desgloce_Entregas())
            {
                DE.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Fechas Fe = new Fechas())
            {
                Fe.ShowDialog();
            }

            if (Fechas.Cancelado != "1")
            {
                sqlConnection2.Open();
                string sql = @"SELECT [Folio]
                      ,EP.[CFOLIO] as FolioDocumento
                      ,EP.[CFECHA] as Fecha
                      ,[FECHAACTUAL] as Fecha_Entrega
                      ,[HoraActual] as Hora_Entrega
                      ,[CCODIGOCLIENTE] as Codigo_Cliente
                      ,EP.[CRAZONSOCIAL] as Nombre_Cliente
                      ,EP.[CRFC] as RFC_Cliente
                      ,EP.[CDESTINATARIO] as Destinatario
                      ,EP.[CNUMEROCAJAS] as Numero_Cajas
                      ,EP.[CNUMEROGUIA] as Numero_Guia
                      ,EP.[CMENSAJERIA] as Mensajeria
                      ,[CNOMBRECONCEPTO] as Concepto
                      ,[calleFiscal] as Calle_Fiscal
                      ,[CiudadFiscal] as Ciudad_Fiscal
                      ,[cp_Fiscal] as CP_Fiscal
                      ,[ColoniaFiscal] as Colonia_Fiscal
                      ,[calleEnvio] as Calle_Envio
                      ,[CiudadEnvio] as Ciudad_Envio
                      ,[cp_Envio] as CP_Envio
                      ,[ColoniaEnvio] as Colonia_Envio
                      ,[TelefonoEnvio] as Telefono_Envio
                      ,[CCODIGOPRODUCTO] as Codigo_Producto
                      ,[CNOMBREPRODUCTO] as Nombre_Producto
                      ,[CCODIGOALMACEN] as Codigo_almacen
                      ,[CNOMBREALMACEN] as Nombre_Almacen
                      ,[CUNIDADES] as Unidades
                      ,[PESO] as Peso
                      ,[PESO_TOTAL] as Peso_Total
                      ,EP.[COBSERVACIONES] as Observaciones
                      ,CASE WHEN
                Docu.CCANCELADO = 0 then '' 
                when Docu.CCANCELADO = 1 then 'CANCELADO'
                end as CANCELADO
                FROM [dbo].[Entregas_Planta] as EP
                INNER JOIN adAMSACONTPAQi.dbo.admDocumentos as Docu
                on EP.idDocumento = Docu.CIDDOCUMENTO
                where ep.FECHAACTUAL between @Fecha1 and @Fecha2";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                cmd.Parameters.AddWithValue("@Fecha1", Fechas.Fecha1); 
                cmd.Parameters.AddWithValue("@Fecha2", Fechas.Fecha2); 
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                DataTable miTabla = ds.Tables[0];

                sqlConnection2.Close();

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                    Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    libros_trabajo = aplicacion.Workbooks.Add();
                    hoja_trabajo =
                        (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                    for (int i = 1; i < miTabla.Columns.Count + 1; i++)
                    {
                        hoja_trabajo.Cells[1, i] = miTabla.Columns[i - 1].ColumnName;
                    }

                    for (int j = 0; j < miTabla.Rows.Count; j++)
                    {
                        for (int k = 0; k < miTabla.Columns.Count; k++)
                        {
                            hoja_trabajo.Cells[j + 2, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                        }
                    }

                    libros_trabajo.SaveAs(fichero.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

                    libros_trabajo.Close(true);
                    MessageBox.Show("Archivo creado correctamente");
                    aplicacion.Quit();
                    DialogResult di = MessageBox.Show("¿Desea abrir el archivo?", "Abrir", MessageBoxButtons.YesNo);
                    if (di == DialogResult.Yes)
                    {
                        aplicacion.Visible = true;
                        aplicacion.Workbooks.Open(fichero.FileName.ToString());
                    }
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(aplicacion);
                }
            }
        }
    }
}
