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

namespace Cotizaciones
{
    public partial class RepCrisRep : Form
    {
        public RepCrisRep(DataGridView datagridview1)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Existencia", typeof(string));
            dt.Columns.Add("Precio", typeof(double));
            dt.Columns.Add("Importe", typeof(string));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("Tot Peso", typeof(string));
            dt.Columns.Add("$xKG1", typeof(string));
            dt.Columns.Add("KGLAB", typeof(string));
            dt.Columns.Add("Flete", typeof(string));
            dt.Columns.Add("Utilidad", typeof(string));
            dt.Columns.Add("$xKG2", typeof(string));
            dt.Columns.Add("Dscto", typeof(string));
            dt.Columns.Add("$xPza", typeof(string));
            dt.Columns.Add("$aCot", typeof(string));

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                dt.Rows.Add(item.Cells[0].Value
                    , item.Cells[1].Value
                    , item.Cells[2].Value
                    , item.Cells[3].Value
                    , item.Cells[4].Value
                    , item.Cells[5].Value
                    , item.Cells[6].Value
                    , item.Cells[7].Value
                    
                    );
            }
            ds.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizaciones\imprep.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", "COT-"+ VariablesCompartidas.SerieImp+"-"+ VariablesCompartidas.Folio);
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
    }
}