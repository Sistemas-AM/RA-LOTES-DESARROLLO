using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cotizacion2022
{
    public partial class ImprimeCot : Form
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public ImprimeCot(DataGridView datagridview1)
        {
            InitializeComponent();
            
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("preciodescuento", typeof(string));
            dt.Columns.Add("cantidad", typeof(string));
            dt.Columns.Add("Dscto", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("importe", typeof(string));


            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells["Codigo"].Value.ToString()))
                    {
                        dt.Rows.Add(item.Cells["Codigo"].Value
                        , item.Cells["Nombre"].Value
                        , item.Cells["PrecioDesc"].Value
                        , item.Cells["Cantidad"].Value
                        , item.Cells["Descuento"].Value + "%"
                        , "$ " + item.Cells["PrecioOri"].Value
                        , item.Cells["Peso"].Value
                        , "$ " + item.Cells["ImporteSI"].Value

                        );
                    }
                }
                catch (NullReferenceException)
                {
                    
                }
                
            }
            ds.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizacion2022\imprep.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa",  VariablesCompartidas.Folio);
            rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            rd.SetParameterValue("AtencionPa", VariablesCompartidas.Atencion);
            rd.SetParameterValue("SolicitoPa", VariablesCompartidas.Solicito);
            rd.SetParameterValue("textos", VariablesCompartidas.Textos);

            rd.SetParameterValue("sucursal", infogerente.sucursal);
            rd.SetParameterValue("direccion", infogerente.direccion);
            rd.SetParameterValue("colonia", infogerente.colonia);
            rd.SetParameterValue("lugar", infogerente.lugar);
            rd.SetParameterValue("telefono", infogerente.telefono.Trim());
            rd.SetParameterValue("celular", infogerente.celular.Trim());
            rd.SetParameterValue("nombre", infogerente.nombre.Trim());
            rd.SetParameterValue("email", infogerente.email.Trim());

            rd.SetParameterValue("subtotal", infogerente.subtotal);
            rd.SetParameterValue("iva", infogerente.iva);
            rd.SetParameterValue("total", infogerente.total);

            //rd.SetParameterValue("Existencia", VariablesCompartidas.existencia);
            //rd.SetParameterValue("FeHOY", DateTime.Now.ToString("dd/MM/yyyy"));
            //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
            //DateTime.Now.ToString("HH:mm:ss")

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizacion2020\imprep.rpt");

            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", VariablesCompartidas.Folio);
            rd.SetParameterValue("FechaPa", VariablesCompartidas.Fecha);
            rd.SetParameterValue("NombrePa", VariablesCompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", VariablesCompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", VariablesCompartidas.Direccion);
            rd.SetParameterValue("EmailPa", VariablesCompartidas.Email);
            rd.SetParameterValue("AtencionPa", VariablesCompartidas.Atencion);
            rd.SetParameterValue("SolicitoPa", VariablesCompartidas.Solicito);
            rd.SetParameterValue("textos", VariablesCompartidas.Textos);

            rd.SetParameterValue("sucursal", infogerente.sucursal);
            rd.SetParameterValue("direccion", infogerente.direccion);
            rd.SetParameterValue("colonia", infogerente.colonia);
            rd.SetParameterValue("lugar", infogerente.lugar);
            rd.SetParameterValue("telefono", infogerente.telefono.Trim());
            rd.SetParameterValue("celular", infogerente.celular.Trim());
            rd.SetParameterValue("nombre", infogerente.nombre.Trim());
            rd.SetParameterValue("email", infogerente.email.Trim());

            rd.SetParameterValue("subtotal", infogerente.subtotal);
            rd.SetParameterValue("iva", infogerente.iva);
            rd.SetParameterValue("total", infogerente.total);

            //rd.SetParameterValue("Existencia", VariablesCompartidas.existencia);
            //rd.SetParameterValue("FeHOY", DateTime.Now.ToString("dd/MM/yyyy"));
            //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
            //DateTime.Now.ToString("HH:mm:ss")

            //crystalReportViewer1.ReportSource = rd;

            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"\\192.168.1.127\Fuentes Sistemas\PDFCOTIZA\COTIZACION.pdf");

            MessageBox.Show("Exported Successful");

           


        }

        private void ImprimeCot_Load(object sender, EventArgs e)
        {

        }
    }
}
