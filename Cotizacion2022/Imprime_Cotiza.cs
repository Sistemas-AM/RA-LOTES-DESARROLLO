using Microsoft.Reporting.WinForms;
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

namespace Cotizacion2022
{
    public partial class Imprime_Cotiza : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataSet1 Data_Set = new DataSet1();
        DataTable dt = new DataTable();


        public Imprime_Cotiza(DataGridView datagridview1)
        {
            InitializeComponent();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("NumLote", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(double));
            dt.Columns.Add("cantidad", typeof(double));
            dt.Columns.Add("Descuento", typeof(string));
            dt.Columns.Add("Precio", typeof(double));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("importe", typeof(double));
            dt.Columns.Add("NetoDescuento", typeof(double));
            dt.Columns.Add("CantidadDescuento", typeof(double));


            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells["Codigo"].Value.ToString()))
                    {
                        double cDescuentoGeneral = 0;
                        double Descuento_Parseado = Double.Parse(item.Cells["Descuento"].Value.ToString());
                        double Neto_Parseado = Double.Parse(item.Cells["Cneto"].Value.ToString());

                        double descuento = Math.Round(Descuento_Parseado / 100, 4);

                        double neto_Con_Descuento = 0;

                        cDescuentoGeneral = Math.Round(Neto_Parseado * descuento, 2);

                        neto_Con_Descuento = Neto_Parseado - cDescuentoGeneral;






                        dt.Rows.Add(item.Cells["Codigo"].Value
                        , item.Cells["Nombre"].Value
                        ,item.Cells["NumLote"].Value
                        , item.Cells["Cneto"].Value
                        , item.Cells["Cantidad"].Value
                        , item.Cells["Descuento"].Value + "%"
                        , item.Cells["Cprecio"].Value
                        , item.Cells["Peso"].Value
                        , item.Cells["ImporteSi"].Value
                        , neto_Con_Descuento.ToString()
                        , item.Cells["Descuento"].Value

                        );
                    }
                }
                catch (NullReferenceException)
                {

                }

            }
            Data_Set.Tables.Add(dt);

            cargarReporte();

        }


        private void cargarReporte()
        {
            //SqlConnection S_Conn = new SqlConnection(Flotillas.VariablesCompartidas.Flotillas);
            //S_Conn.Open();
            //SqlCommand cmd = new SqlCommand("reprdia", FlotillasConection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@tipo", tipo);
            //cmd.Parameters.AddWithValue("@fecini", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
            //cmd.Parameters.AddWithValue("@fecfin", dateTimePicker2.Value.ToString("MM/dd/yyyy"));
            //cmd.Parameters.AddWithValue("@suc", sucursal);
            //cmd.Parameters.AddWithValue("@emp", empresa);

            //SqlDataAdapter Data_Adapter = new SqlDataAdapter(cmd);
            //DataSet1 Data_Set = new DataSet1();
            //Data_Adapter.Fill(Data_Set);

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            ReportParameter[] parameters = new ReportParameter[23];

            //rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            //rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            //rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            //rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            //rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            //rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            //rd.SetParameterValue("AtencionPa", VariablesCompartidas.Atencion);
            //rd.SetParameterValue("SolicitoPa", VariablesCompartidas.Solicito);
            //rd.SetParameterValue("textos", VariablesCompartidas.Textos);

            //rd.SetParameterValue("sucursal", infogerente.sucursal);
            //rd.SetParameterValue("direccion", infogerente.direccion);
            //rd.SetParameterValue("colonia", infogerente.colonia);
            //rd.SetParameterValue("lugar", infogerente.lugar);
            //rd.SetParameterValue("telefono", infogerente.telefono.Trim());
            //rd.SetParameterValue("celular", infogerente.celular.Trim());
            //rd.SetParameterValue("nombre", infogerente.nombre.Trim());
            //rd.SetParameterValue("email", infogerente.email.Trim());

            //rd.SetParameterValue("subtotal", infogerente.subtotal);
            //rd.SetParameterValue("iva", infogerente.iva);
            //rd.SetParameterValue("total", infogerente.total);

            parameters[0] = new ReportParameter("subtotal", infogerente.subtotal);
            parameters[1] = new ReportParameter("iva", infogerente.iva);
            parameters[2] = new ReportParameter("total", infogerente.total);

            parameters[3] =  new ReportParameter("FolioPa", VariablesCompartidas.Folio);
            parameters[4] =  new ReportParameter("FechaPa", VariablesCompartidas.Fecha);
            parameters[5] =  new ReportParameter("NombrePa", VariablesCompartidas.Cliente);
            parameters[6] =  new ReportParameter("TelefonoPa", VariablesCompartidas.Telefono);
            parameters[7] =  new ReportParameter("DireccionPa", VariablesCompartidas.Direccion);
            parameters[8] =  new ReportParameter("EmailPa", VariablesCompartidas.Email);
            parameters[9] =  new ReportParameter("AtencionPa", VariablesCompartidas.Solicito);
            parameters[10] = new ReportParameter("SolicitoPa", VariablesCompartidas.Solicito);
            parameters[11] = new ReportParameter("textos", VariablesCompartidas.Textos);

            parameters[12] = new ReportParameter("Sucursal", infogerente.sucursal);
            parameters[13] = new ReportParameter("direccion", infogerente.direccion);
            parameters[14] = new ReportParameter("colonia", infogerente.colonia);
            parameters[15] = new ReportParameter("lugar", infogerente.lugar);
            parameters[16] = new ReportParameter("telefono", infogerente.telefono.Trim());
            parameters[17] = new ReportParameter("celular", infogerente.celular.Trim());
            parameters[18] = new ReportParameter("nombre", infogerente.nombre.Trim());
            parameters[19] = new ReportParameter("email", infogerente.email.Trim());
            parameters[20] = new ReportParameter("CorreoAgente", VariablesCompartidas.CorreoAgente);
            parameters[21] = new ReportParameter("TelefonoAgente", VariablesCompartidas.TelefonoAgente);
            parameters[22] = new ReportParameter("NombreAgente", VariablesCompartidas.NombreAgente);

            //6622 18 28 22

            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();

        //    string deviceInfo = "";
        //    string[] streamIds;
        //    Warning[] warnings;
        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;

        //    var bytes = reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension,
        //        out streamIds, out warnings);

        //    string filename = @"C:\Users\Programador\Desktop\Flotilla\Calesito.pdf";
        //    File.WriteAllBytes(filename, bytes);
        //    System.Diagnostics.Process.Start(filename);
        }

        private void Imprime_Cotiza_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
            if (!string.IsNullOrEmpty(Form1.CorreoPasa))
            {
                button6.Visible = true;
            }
            else
            {
                button6.Visible = false;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string deviceInfo = "";
            string[] streamIds;
            Warning[] warnings;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            var bytes = reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension,
                out streamIds, out warnings);

            string filename = @"\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf";
            File.WriteAllBytes(filename, bytes);
            correo();
            //System.Diagnostics.Process.Start(filename);

        }

        private void correo()
        {
            //string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            //@profile_name = 'Notifications',
            //@recipients =@Recipiente,
            //@copy_recipients = @Copia,
            //@subject = @Folio2, 
            //@body = @Folio,
            //@file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf';";
            //SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.Parameters.AddWithValue("@Recipiente", "yisusedward@gmail.com"); 
            //cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx"); 
            //cmd.Parameters.AddWithValue("@Folio", "Se envia la cotizacion: " +textFolio.Text); 
            //cmd.Parameters.AddWithValue("@Folio2",  textFolio.Text); 


            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Close();

            //MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

           
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spCorreoCotizacion", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Folio", "Tu Cotización ha llegado!");
                cmd.Parameters.AddWithValue("@Recipiente", Form1.CorreoPasa);
                cmd.Parameters.AddWithValue("@Recipiente2", Form1.CorreoPasa2);
            cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
           

            //string sql = @"EXEC msdb.dbo.sp_send_dbmail 
            //@profile_name = 'Notificaciones',
            //@recipients =@Recipiente,
            //@subject = @Folio2, 
            //@body_format = 'html',
            //@body = @Folio,
            //@file_attachments = '\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf'; ";
            //SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //cmd.Parameters.AddWithValue("@Recipiente", Form1.CorreoPasa);
            ////cmd.Parameters.AddWithValue("@Copia", "coordinador.ti@acerosmexico.com.mx");
            //cmd.Parameters.AddWithValue("@Folio", "<h1>Se envia la cotización: </h1>" + "<h2>" + VariablesCompartidas.Folio + "</h2> <img src=" + "https://i.ibb.co/cgm9m0W/img1.jpg"+" alt="+"img1" +"border="+"0"+"></a>");
            //cmd.Parameters.AddWithValue("@Folio2", VariablesCompartidas.Folio);
            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Close();
            //MessageBox.Show("CORREO ENVIADO EXITOSAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
    }
}
