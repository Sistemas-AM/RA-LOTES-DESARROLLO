using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace Reporte_de_Cobranza
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
            crystalReportViewer1.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // this.pagosTableAdapter.Fill(this.dataSet1.pagos, dateTimePicker1.Value, dateTimePicker2.Value);
            try
            {

                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("pagos", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecini", DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@fecfin", DateTime.Parse(dateTimePicker2.Text).ToString("MM/dd/yyyy"));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ReportDocument rd = new ReportDocument();
                rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+ @"\Reporte de Cobranza\ReporteCobranza.rpt");
                rd.SetDataSource(ds.Tables[0]);
                rd.SetParameterValue("FInicial", dateTimePicker1.Text);
                rd.SetParameterValue("FFinal", dateTimePicker2.Text);
                rd.SetParameterValue("FeHOY", DateTime.Now.ToString("MM/dd/yyyy"));
                rd.SetParameterValue("HoraHOY", DateTime.Now.ToString("HH:mm:ss"));
                //DateTime.Now.ToString("HH:mm:ss")

                crystalReportViewer1.ReportSource = rd;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Show();
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
            

            //SqlDataAdapter sda = new SqlDataAdapter("select CFecha, CFolio, CRAZONSOCIAL,CFOLIO = (select CFOLIO from admdocumentos where ciddocumento = CIDDOCUMENTOCARGO), CFECHA = (select CFECHA from admdocumentos where ciddocumento = CIDDOCUMENTOCARGO), CNETO  = (select CNETO from admdocumentos where ciddocumento = CIDDOCUMENTOCARGO), CIMPUESTO1 = (select CIMPUESTO1 from admdocumentos where ciddocumento = CIDDOCUMENTOCARGO), CTOTAL  = (select CTOTAL from admdocumentos where ciddocumento = CIDDOCUMENTOCARGO), CIMPORTEABONO, CIDMONEDA from admdocumentos inner join admAsocCargosAbonos  on CIDDOCUMENTO = CIDDOCUMENTOABONO where cidconceptodocumento = 13 and cfecha between '01/03/2018' and '31/03/2018'", sqlConnection1);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //foreach (DataRow item in dt.Rows)
            //{
            //    int n = dataGridView1.Rows.Add();
            //    DateTime parsedDate = DateTime.Parse(item[0].ToString());
            //    dataGridView1.Rows[n].Cells[0].Value = parsedDate.ToString("dd/MM/yyyy");
            //    dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
            //    dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
            //    dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
            //    DateTime parsedDate2 = DateTime.Parse(item[4].ToString());
            //    dataGridView1.Rows[n].Cells[4].Value = parsedDate2.ToString("dd/MM/yyyy");



            //    if (item[6].ToString() == "0")
            //    {
            //        dataGridView1.Rows[n].Cells[5].Value = "0";
            //        dataGridView1.Rows[n].Cells[6].Value = item[5].ToString();
            //        dataGridView1.Rows[n].Cells[7].Value = "0";
            //        dataGridView1.Rows[n].Cells[8].Value = item[7].ToString();
            //    }
            //    else
            //    {
            //        dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
            //        dataGridView1.Rows[n].Cells[6].Value = "0";
            //        dataGridView1.Rows[n].Cells[7].Value = item[6].ToString();
            //        dataGridView1.Rows[n].Cells[8].Value = item[7].ToString();
            //    }
            //    dataGridView1.Rows[n].Cells[9].Value = item[8].ToString();

            //    if (item[9].ToString() == "1")
            //    {
            //        dataGridView1.Rows[n].Cells[10].Value = "MXP";

            //    }
            //    else if (item[9].ToString() == "2")
            //    {
            //        dataGridView1.Rows[n].Cells[10].Value = "DLS";
            //    }

            //}

        }

        

        private void crystalReportViewer1_ReportRefresh(object source, CrystalDecisions.Windows.Forms.ViewerEventArgs e)
        {

        }

        
    }
}

        
