using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DistribucionMateriales
{
    public partial class ReporteForm : Form
    {
        public ReporteForm(DataGridView datagridview5)
        {
            InitializeComponent();
            //Pasar el data grid DataGridView datagridview1
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Existen", typeof(string));
            dt.Columns.Add("Cap", typeof(string));
            dt.Columns.Add("Max", typeof(string));
            dt.Columns.Add("Surtir", typeof(string));
            dt.Columns.Add("Existen2", typeof(string));
            dt.Columns.Add("Cap2", typeof(string));
            dt.Columns.Add("Max2", typeof(string));
            dt.Columns.Add("Sutrir2", typeof(string));
            dt.Columns.Add("Existen3", typeof(string));
            dt.Columns.Add("Cap3", typeof(string));
            dt.Columns.Add("Max3", typeof(string));
            dt.Columns.Add("Surtir3", typeof(string));
            dt.Columns.Add("Existen4", typeof(string));
            dt.Columns.Add("Cap4", typeof(string));
            dt.Columns.Add("Max4", typeof(string));
            dt.Columns.Add("Surtir4", typeof(string));
            dt.Columns.Add("Planta", typeof(string));
            dt.Columns.Add("SurtidoSugerido", typeof(float));
            dt.Columns.Add("SurtidoReal", typeof(float));
            dt.Columns.Add("TotalKilos", typeof(float));
            dt.Columns.Add("Letra", typeof(string));




            try
            {
                foreach (DataGridViewRow item in datagridview5.Rows)
                {
                    if (item.Cells["Column30"].Value.ToString() != "0")
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
                                  , item.Cells[16].Value
                                  , item.Cells[17].Value
                                  , item.Cells[18].Value
                                  , item.Cells[19].Value
                                  , item.Cells[20].Value
                                  , item.Cells[21].Value
                                  , item.Cells[22].Value



                                  );
                }
            }
            }
            catch (Exception)
            {

                
            }
            ds.Tables.Add(dt);


            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\DistribucionMateriales\Repo1.rpt");
            // rd.SetDataSource(ds.Tables[0]);
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);

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
