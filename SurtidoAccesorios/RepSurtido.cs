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

namespace SurtidoAccesorios
{
    public partial class RepSurtido : Form
    {
        public RepSurtido(DataGridView datagridview1)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Letra", typeof(string));
            dt.Columns.Add("Clasificacion", typeof(string));
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("MaxVenta", typeof(string));
            dt.Columns.Add("Existencia", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Cedis", typeof(float));
            dt.Columns.Add("Desabasto", typeof(string));
            dt.Columns.Add("Col", typeof(float));


            try
            {
                foreach (DataGridViewRow item in datagridview1.Rows)
                {
                    if (item.Cells["column8"].Value.ToString() != "0")
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
                                  

                                  );
                    }
                }
            }
            catch (Exception)
            {


            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\SurtidoAccesorios\CrystalReport1.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            rd.SetParameterValue("sucursal", Variablescompartidas.sucursal);
            rd.SetParameterValue("v1", Variablescompartidas.v1);
            rd.SetParameterValue("v2", Variablescompartidas.v2);
            

            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();

        }
    }
}
