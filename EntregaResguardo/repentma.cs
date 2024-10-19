using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace EntregaResguardo
{
    public partial class repentma : Form
    {
        public repentma(DataGridView datagridview)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("dgvcodigo", typeof(string));
            dt.Columns.Add("dgvnombre", typeof(string));
            dt.Columns.Add("dgventregado", typeof(string));
            dt.Columns.Add("dgvsaldo", typeof(string));
            dt.Columns.Add("dgvobserva", typeof(string));
            //dt.Columns.Add("kmsalida", typeof(string));
            //dt.Columns.Add("kmllega", typeof(string));
            //dt.Columns.Add("folio", typeof(string));
            //dt.Columns.Add("importe", typeof(string));
            //dt.Columns.Add("direccion", typeof(string));
            //dt.Columns.Add("observa", typeof(string));
            try
            {
                foreach (DataGridViewRow item in datagridview.Rows)
                {
                    //if (item.Cells["column14"].Value.ToString() != "0")
                    //{
                    dt.Rows.Add(item.Cells[0].Value
                              , item.Cells[1].Value
                              , item.Cells[3].Value
                              , item.Cells[4].Value
                              , item.Cells[5].Value
                          

                              );
                    // }
                }
            }
            catch (Exception)
            {


            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\EntregaResguardo\entmat.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("numfac", varglob.factura);
            rd.SetParameterValue("nomcte", varglob.cliente);
            rd.SetParameterValue("fecha", varglob.fecfac);
            rd.SetParameterValue("sucursal", varglob.suc);
            //rd.SetParameterValue("comfin", buscav.wcomfin);
            //rd.SetParameterValue("kmini", Convert.ToInt32(buscav.wkmini));
            //rd.SetParameterValue("kmfin", Convert.ToInt32(buscav.wkmfin));
            // rd.SetParameterValue("empresa", busveh.wemp);
            // rd.SetParameterValue("fecha1", busveh.wfec1);
            // rd.SetParameterValue("fecha2", busveh.wfec2);
            //rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
            //DateTime.Now.ToString("HH:mm:ss")

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
        }
    }
}
