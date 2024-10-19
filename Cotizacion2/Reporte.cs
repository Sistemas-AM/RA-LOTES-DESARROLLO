using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Cotizacion2
{
    public partial class Reporte : Form
    {
        public Reporte(DataGridView datagridview1)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Unidad", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Importe", typeof(string));
          
            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                dt.Rows.Add(item.Cells[0].Value
                    , item.Cells[1].Value
                    , item.Cells[2].Value
                    , item.Cells[3].Value
                    , item.Cells[4].Value
                    
                    );
            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Cotizacion2\CotizaPDF.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("Folio", Variablescompartidas.Folio);


            DateTime hoy = DateTime.Parse(Variablescompartidas.Fecha);

            string mees = "";
            if (hoy.Month == 1)
            {
                mees = "Enero";
            }

            if (hoy.Month == 2)
            {
                mees = "Febrero";
            }
            if (hoy.Month == 3)
            {
                mees = "Marzo";
            }
            if (hoy.Month == 4)
            {
                mees = "Abril";
            }
            if (hoy.Month == 5)
            {
                mees = "Mayo";
            }
            if (hoy.Month == 6)
            {
                mees = "Junio";
            }
            if (hoy.Month == 7)
            {
                mees = "Julio";
            }
            if (hoy.Month == 8)
            {
                mees = "Agosto";
            }
            if (hoy.Month == 9)
            {
                mees = "Septiembre";
            }
            if (hoy.Month == 10)
            {
                mees = "Octubre";
            }
            if (hoy.Month == 11)
            {
                mees = "Noviembre";
            }
            if (hoy.Month == 12)
            {
                mees = "Diciembre";
            }
            string fechatexto = "A "+hoy.Day.ToString() + " de " + mees + " del " + hoy.Year.ToString() +"; Hermosillo, Sonora";
            rd.SetParameterValue("FechaPa", fechatexto);

            rd.SetParameterValue("NombrePa", Variablescompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", Variablescompartidas.Telefono);
            //rd.SetParameterValue("DireccionPa", Variablescompartidas.Direccion);
            //rd.SetParameterValue("EmailPa", Variablescompartidas.Email);
            //rd.SetParameterValue("AtencionPa", Variablescompartidas.Atencion);
            rd.SetParameterValue("SolicitoPa", Variablescompartidas.Atencion);
            rd.SetParameterValue("textos", Variablescompartidas.Textos);

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
