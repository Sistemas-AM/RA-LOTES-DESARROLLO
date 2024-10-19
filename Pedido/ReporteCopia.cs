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

namespace Pedido
{
    public partial class ReporteCopia : Form
    {
        public ReporteCopia(DataGridView datagridview1)
        {
           /* if (item.Cells["Pzas3"].Value.ToString() != "0")
            {
                dt.Rows.Add(item.Cells["Codigo"].Value
                          , item.Cells["Nombre"].Value
                          , item.Cells["Clas"].Value
                          , item.Cells["Existe"].Value
                          , item.Cells["Kilos"].Value
                          , item.Cells["Pzas"].Value
                          , item.Cells["Kilos2"].Value
                          , item.Cells["MaxAdmin"].Value
                          , item.Cells["Espacio"].Value
                          , item.Cells["Surtido"].Value
                          , item.Cells["Trans"].Value
                          , item.Cells["Pzas2"].Value
                          , item.Cells["Kilos3"].Value
                          , item.Cells["Pzas3"].Value
                          , item.Cells["Kilos4"].Value
                          , item.Cells["Peso"].Value
                          );
            }*/
            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("codigo", typeof(string));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("clas", typeof(string));
            dt.Columns.Add("MaxAdmin", typeof(string));
            dt.Columns.Add("Existe", typeof(string));
            dt.Columns.Add("MaxCedis", typeof(string));
            dt.Columns.Add("ExiCedis", typeof(string));
            dt.Columns.Add("ExisTotal", typeof(string));
            dt.Columns.Add("ExisReq", typeof(string));
            dt.Columns.Add("Trans", typeof(string));
            dt.Columns.Add("PedRel", typeof(string));
            dt.Columns.Add("Pzas3", typeof(string));
            dt.Columns.Add("Kilos4", typeof(string));
            dt.Columns.Add("MP", typeof(string));

            //try
            //{
            foreach (DataGridViewRow item in datagridview1.Rows)
                {
                    if (item.Cells["Pzas3"].Value.ToString() != "0")
                    {
                        dt.Rows.Add(item.Cells["Codigo"].Value
                                  , item.Cells["Nombre"].Value
                                  , item.Cells["Clas"].Value
                                  , item.Cells["MaxAdmin"].Value
                                  , item.Cells["Existe"].Value
                                  , item.Cells["MaxCedis"].Value
                                  , item.Cells["ExiCedis"].Value
                                  , item.Cells["ExisTotal"].Value
                                  , item.Cells["ExisReq"].Value
                                  , item.Cells["Trans"].Value
                                  , item.Cells["PedRel"].Value
                                  , item.Cells["Pzas3"].Value
                                  , item.Cells["Kilos4"].Value,
                                  item.Cells["MP"].Value
                                  );
                    }
                }
            //}
            //catch (Exception e)
            //{
               

            //}
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Pedido\copia.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

            //rd.SetParameterValue("Folio", Variablescompartidas.Folio);
            rd.SetParameterValue("Fecha", Variablescompartidas.Fecha);
            //rd.SetParameterValue("RC", Variablescompartidas.RC);
            //rd.SetParameterValue("RF", Variablescompartidas.RF);
            //rd.SetParameterValue("Obs", Variablescompartidas.Observaciones);
            rd.SetParameterValue("pro", Variablescompartidas.proveedor);
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
