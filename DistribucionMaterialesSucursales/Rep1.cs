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

namespace DistribucionMaterialesSucursales
{
    public partial class Rep1 : Form
    {
        public Rep1(DataGridView datagridview1)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("DataColumn1", typeof(string));
            dt.Columns.Add("DataColumn2", typeof(string));
            dt.Columns.Add("DataColumn3", typeof(string));
            dt.Columns.Add("DataColumn4", typeof(string));
            dt.Columns.Add("DataColumn5", typeof(string));
            dt.Columns.Add("DataColumn6", typeof(string));
            dt.Columns.Add("DataColumn7", typeof(string));
            dt.Columns.Add("DataColumn8", typeof(float));
            dt.Columns.Add("DataColumn9", typeof(string));
            dt.Columns.Add("DataColumn10", typeof(float));
            dt.Columns.Add("DataColumn11", typeof(float));
            dt.Columns.Add("DataColumn12", typeof(float));
            dt.Columns.Add("DataColumn13", typeof(string));


            try
            {
                foreach (DataGridViewRow item in datagridview1.Rows)
                {
                    if (item.Cells["Column11"].Value.ToString() != "0")
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


                                  );
                    }
                }
            }
            catch (Exception)
            {


            }
            ds.Tables.Add(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\DistribucionMaterialesSucursales\repDistribucion.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);
            

            rd.SetParameterValue("sucursal", Variablescompartidas.sucursal);
            rd.SetParameterValue("v1", Variablescompartidas.v1);
            rd.SetParameterValue("v2", Variablescompartidas.v2);
            rd.SetParameterValue("Folio", Variablescompartidas.folio);

            DateTime hoy = DateTime.Now;
            rd.SetParameterValue("fecha", hoy.ToString("MM/dd/yyyy"));


            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();



        }
    }
}
