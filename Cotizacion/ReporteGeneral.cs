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

namespace Cotizacion
{
    public partial class ReporteGeneral : Form
    {
        public ReporteGeneral(DataGridView datagridview1)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Existencia", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Importe", typeof(float));
            dt.Columns.Add("Peso", typeof(string));
            dt.Columns.Add("Tot Peso", typeof(float));
            dt.Columns.Add("$xKG1", typeof(string));
            dt.Columns.Add("KGLAB", typeof(string));
            dt.Columns.Add("Flete", typeof(string));
            dt.Columns.Add("Utilidad", typeof(string));
            dt.Columns.Add("$xKG2", typeof(string));
            dt.Columns.Add("Dscto", typeof(string));
            dt.Columns.Add("$xPza", typeof(string));
            dt.Columns.Add("$aCot", typeof(float));
            dt.Columns.Add("ExisCedis", typeof(float));

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
                    , item.Cells[8].Value
                    , item.Cells[9].Value
                    , item.Cells[10].Value
                    , item.Cells[11].Value
                    , item.Cells[12].Value
                    , item.Cells[13].Value
                    , item.Cells[14].Value
                    , item.Cells[15].Value
                    , item.Cells[19].Value
                    );
            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+ @"\Cotizacion\CrystalReport2.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", Variablescompartidas.Folio);
            rd.SetParameterValue("FechaPa", Variablescompartidas.Fecha);
            rd.SetParameterValue("NombrePa", Variablescompartidas.Cliente);
            rd.SetParameterValue("TelefonoPa", Variablescompartidas.Telefono);
            rd.SetParameterValue("DireccionPa", Variablescompartidas.Direccion);
            rd.SetParameterValue("EmailPa", Variablescompartidas.Email);
            rd.SetParameterValue("AtencionPa", Variablescompartidas.Atencion);
            rd.SetParameterValue("SolicitoPa", Variablescompartidas.Solicito);
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
