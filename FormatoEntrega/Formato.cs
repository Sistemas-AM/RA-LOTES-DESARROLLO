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

namespace FormatoEntrega
{
    public partial class Formato : Form
    {
        public Formato(DataGridView datagridview1)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
           

            foreach (DataGridViewRow item in datagridview1.Rows)
            {
                dt.Rows.Add(item.Cells[0].Value
                    , item.Cells[1].Value
                    , item.Cells[2].Value
                    
                    );
            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\FormatoEntrega\fepdf.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("FolioPa", Variablescompartidas.no);
            rd.SetParameterValue("FechaPa", Variablescompartidas.fecha);
            rd.SetParameterValue("Nombre", Variablescompartidas.nombre);
            rd.SetParameterValue("rfc", Variablescompartidas.rfc);
            //rd.SetParameterValue("DireccionPa", Variablescompartidas.Direccion);
            //rd.SetParameterValue("EmailPa", Variablescompartidas.Email);
            //rd.SetParameterValue("AtencionPa", Variablescompartidas.Atencion);
            //rd.SetParameterValue("SolicitoPa", Variablescompartidas.Solicito);
            //rd.SetParameterValue("textos", Variablescompartidas.Textos);

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
