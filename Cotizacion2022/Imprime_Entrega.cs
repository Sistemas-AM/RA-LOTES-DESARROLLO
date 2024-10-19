using Microsoft.Reporting.WinForms;
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

namespace Cotizacion2022
{
    public partial class Imprime_Entrega : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataSet1 Data_Set = new DataSet1();
        DataTable dt = new DataTable();

        public Imprime_Entrega(DataGridView datagridview1)
        {
            InitializeComponent();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(string));
            dt.Columns.Add("cantidad", typeof(double));
            dt.Columns.Add("Descuento", typeof(string));
            dt.Columns.Add("Precio", typeof(double));
            dt.Columns.Add("Peso", typeof(double));
            dt.Columns.Add("importe", typeof(double));
            dt.Columns.Add("NumLote", typeof(string));

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells["Codigo"].Value.ToString()))
                    {
                        string nombre = item.Cells["Nombre"].Value?.ToString() ?? "Sin Nombre";
                        string numLote = item.Cells["NumLote"].Value?.ToString();

                        if (!string.IsNullOrEmpty(numLote))
                        {
                            nombre = "Numero lote: " + numLote + ",  " + nombre;
                        }

                        dt.Rows.Add(item.Cells["Codigo"].Value
                                                                 ,nombre//if (string.IsNullOrEmpty(item.Cells["NumLote"].Value.ToString())) { item.Cells["Nombre"].Value } else { "Numero lote: " + item.Cells["NumLote"].Value + ",  " + item.Cells["Nombre"].Value}
                                                                 , item.Cells["CPRECIO"].Value
                                                                 , item.Cells["Cantidad"].Value
                                                                 , item.Cells["Descuento"].Value + "%"
                                                                 , item.Cells["CPRECIO"].Value
                                                                 , item.Cells["Peso"].Value
                                                                 , item.Cells["ImporteSI"].Value

                                                                 );
                        
                        
                        

                           /* dt.Rows.Add(item.Cells["Codigo"].Value
                                      , nombre
                                      , item.Cells["CPRECIO"].Value
                                      , item.Cells["Cantidad"].Value
                                      , item.Cells["Descuento"].Value + "%"
                                      , item.Cells["CPRECIO"].Value
                                      , item.Cells["Peso"].Value
                                      , item.Cells["ImporteSI"].Value

                                      );
                        */
                        //No tocar este comentario

                        /*
                        dt.Rows.Add(item.Cells["Codigo"].Value
                                     , item.Cells["Nombre"].Value
                                     , item.Cells["CPRECIO"].Value
                                     , item.Cells["Cantidad"].Value
                                     , item.Cells["Descuento"].Value + "%"
                                     , item.Cells["CPRECIO"].Value
                                     , item.Cells["Peso"].Value
                                     , item.Cells["ImporteSI"].Value

                                     );
                        */

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

            ReportParameter[] parameters = new ReportParameter[30];

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

            parameters[3] = new ReportParameter("FolioPa", VariablesCompartidas.Folio);
            parameters[4] = new ReportParameter("FechaPa", VariablesCompartidas.Fecha);
            parameters[5] = new ReportParameter("NombrePa", VariablesCompartidas.Cliente);
            parameters[6] = new ReportParameter("TelefonoPa", VariablesCompartidas.Telefono);
            parameters[7] = new ReportParameter("DireccionPa", VariablesCompartidas.Direccion);
            //Agregamos Nuestros Parametos al reporte
            parameters[8] = new ReportParameter("ObsDireccionPa", VariablesCompartidas.ObsDireccion);
            parameters[9] = new ReportParameter("EnvioPA", VariablesCompartidas.Envio);

            parameters[10] = new ReportParameter("EmailPa", VariablesCompartidas.Email);
            parameters[11] = new ReportParameter("AtencionPa", VariablesCompartidas.Solicito);
            parameters[12] = new ReportParameter("SolicitoPa", VariablesCompartidas.Solicito);
            parameters[13] = new ReportParameter("textos", VariablesCompartidas.Textos);

            parameters[14] = new ReportParameter("Sucursal", infogerente.sucursal.TrimEnd());
            parameters[15] = new ReportParameter("direccion", infogerente.direccion);
            parameters[16] = new ReportParameter("colonia", infogerente.colonia);
            parameters[17] = new ReportParameter("lugar", infogerente.lugar);
            parameters[18] = new ReportParameter("telefono", infogerente.telefono.Trim());
            parameters[19] = new ReportParameter("celular", infogerente.celular.Trim());
            parameters[20] = new ReportParameter("nombre", infogerente.nombre.Trim());
            parameters[21] = new ReportParameter("email", infogerente.email.Trim());
            parameters[22] = new ReportParameter("CorreoAgente", VariablesCompartidas.CorreoAgente);
            parameters[23] = new ReportParameter("TelefonoAgente", VariablesCompartidas.TelefonoAgente);

            parameters[24] = new ReportParameter("RecibeNombre", VariablesCompartidas.NombreRecibe);
            parameters[25] = new ReportParameter("RecibeTelefono", VariablesCompartidas.TelefonoRecibe);
            parameters[26] = new ReportParameter("FechaEntrega", VariablesCompartidas.FechaEntrega);
            parameters[27] = new ReportParameter("Montacargas", VariablesCompartidas.Montacarga);
            parameters[28] = new ReportParameter("FolioFac", VariablesCompartidas.FolioFac);
            parameters[29] = new ReportParameter("TipoPago", VariablesCompartidas.TipoPago);


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

        

        private void Imprime_Entrega_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
