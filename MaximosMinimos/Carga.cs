using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaximosMinimos
{
    public partial class Carga : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int porcentaje = 1;
        int conta = 0;
        public Carga()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button2.Enabled = false;
            OleDbConnection conn;
            OleDbDataAdapter MyDataAdapter;
            DataTable dt;
            String ruta = "";
            String str = "";
            string nombreHoja = "Aceros 25-05-2017";
            //try
            //{
            OpenFileDialog openfile1 = new OpenFileDialog();
            //openfile1.Filter = "Excel Files |*.*";
            openfile1.Title = "Seleccione el archivo de Excel";
            if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openfile1.FileName.Equals("") == false)
                {
                    ruta = openfile1.FileName;
                    str = Path.GetFileNameWithoutExtension(ruta);
                }

                try
                {
                    Microsoft.Office.Interop.Excel.Application ExcApp;
                    Microsoft.Office.Interop.Excel.Workbook wBook;
                    Microsoft.Office.Interop.Excel.Worksheet wSheet;
                    ExcApp = new Microsoft.Office.Interop.Excel.Application();
                    wBook = ExcApp.Workbooks.Open(ruta); // Ruta del archivo de excel 
                    wSheet = wBook.Worksheets[1]; // El nombre de la hoja que se va a leer 

                    conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=No'");
                    MyDataAdapter = new OleDbDataAdapter("Select * from [" + wSheet.Name.ToString() + "$]", conn);
                    dt = new DataTable();
                    MyDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    ExcApp.DisplayAlerts = false;
                    wBook.Close();
                    ExcApp.Quit();

                    //dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                    //dataGridView1.Rows.Remove(dataGridView1.Rows[0]);



                }
                catch (Exception j)
                {

                    MessageBox.Show("Error al cargar la hoja " + j);
                }
            }

            

            //if (!backgroundWorker2.IsBusy)
            //{
            //    progressBar1.Value = 0;
                
            //    backgroundWorker2.RunWorkerAsync();
            //}
        }

        private void insertaMat()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    if (row.Cells["Column11"].Value != null)
                    {
                        string sql = "update admMaximosMinimos set CEXISTENCIAMINBASE = @param2, CEXISTENCIAMAXBASE = @param1 where CIDPRODUCTO = @param3 and CIDALMACEN = '1'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@param1", row.Cells["Column3"].Value.ToString()); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param2", row.Cells["Column4"].Value.ToString()); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param3", row.Cells["Column11"].Value.ToString()); //Para grabar una columna


                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        //int percentage = (i + 1) * 100 / filesCount;
                        conta += 1;
                        int a = dataGridView1.Rows.Count * 4;
                        porcentaje = (conta) * 100 / a;
                        backgroundWorker1.ReportProgress(porcentaje);

                    }
                    //}
                }
            }
            catch (NullReferenceException)
            {


            }

            //MessageBox.Show("Guardado");

        }

        private void insertaLP()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    if (row.Cells["Column11"].Value != null)
                    {
                        string sql = "update admMaximosMinimos set CEXISTENCIAMINBASE = @param2, CEXISTENCIAMAXBASE = @param1 where CIDPRODUCTO = @param3 and CIDALMACEN = '3'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@param1", row.Cells["Column5"].Value.ToString()); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param2", row.Cells["Column6"].Value.ToString()); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param3", row.Cells["Column11"].Value.ToString()); //Para grabar una columna

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        conta += 1;
                        int a = dataGridView1.Rows.Count * 4;
                        porcentaje = (conta) * 100 / a;
                        backgroundWorker1.ReportProgress(porcentaje);

                    }
                    //}
                }
            }
            catch (NullReferenceException)
            {


            }
        }

        private void insertaIS()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    if (row.Cells["Column11"].Value != null)
                    {
                        string sql = "update admMaximosMinimos set CEXISTENCIAMINBASE = @param2, CEXISTENCIAMAXBASE = @param1 where CIDPRODUCTO = @param3 and CIDALMACEN = '4'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@param1", row.Cells["Column7"].Value.ToString()); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param2", row.Cells["Column8"].Value.ToString()); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param3", row.Cells["Column11"].Value.ToString()); //Para grabar una columna
                        
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        conta += 1;
                        int a = dataGridView1.Rows.Count * 4;
                        porcentaje = (conta) * 100 / a;
                        backgroundWorker1.ReportProgress(porcentaje);

                    }
                    //}
                }
            }
            catch (NullReferenceException)
            {


            }
        }

        private void insertaSP()
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Column11"].Value != null)
                    {
                        string sql = "update admMaximosMinimos set CEXISTENCIAMINBASE = @param2, CEXISTENCIAMAXBASE = @param1 where CIDPRODUCTO = @param3 and CIDALMACEN = '5'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@param1", row.Cells["Column9"].Value.ToString()); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param2", row.Cells["Column10"].Value.ToString()); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param3", row.Cells["Column11"].Value.ToString()); //Para grabar una columna
                        
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        conta += 1;
                        int a = dataGridView1.Rows.Count * 4;
                        porcentaje = (conta) * 100 / a;
                        backgroundWorker1.ReportProgress(porcentaje);
                        
                    }
                }
            }
            catch (NullReferenceException)
            {


            }
        }

        private void insertaCD()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    if (row.Cells["Column11"].Value != null)
                    {
                        string sql = "update admMaximosMinimos set CEXISTENCIAMINBASE = @param2, CEXISTENCIAMAXBASE = @param1 where CIDPRODUCTO = @param3 and CIDALMACEN = '8'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        cmd.Parameters.AddWithValue("@param1", row.Cells["CDMax"].Value.ToString()); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param2", row.Cells["cdMin"].Value.ToString()); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param3", row.Cells["Column11"].Value.ToString()); //Para grabar una columna

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();
                        conta += 1;
                        int a = dataGridView1.Rows.Count * 4;
                        porcentaje = (conta) * 100 / a;
                        backgroundWorker1.ReportProgress(porcentaje);

                    }
                    //}
                }
            }
            catch (NullReferenceException)
            {


            }
        }

        private void id()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    string sql = "select CIDPRODUCTO from admProductos where CCODIGOPRODUCTO = @param1";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                    //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
                    cmd.Parameters.AddWithValue("@param1", row.Cells["Column1"].Value.ToString()); //Para grabar una columna
                    
                    sqlConnection1.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        row.Cells["Column11"].Value = reader["CIDPRODUCTO"].ToString();
                    }

                    sqlConnection1.Close();
                    conta += 1;
                    int a = dataGridView1.Rows.Count;
                    porcentaje = (conta) * 100 / a;
                    backgroundWorker2.ReportProgress(porcentaje);
                }
            }
            catch (NullReferenceException)
            {


            }

        }

        private void obtenID()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                //{
                string sql = "select CIDPRODUCTO from admProductos where CCODIGOPRODUCTO = @codigo";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Codigo", row.Cells["Column1"].Value.ToString()); 


                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    row.Cells["Column11"].Value = reader["CIDPRODUCTO"].ToString();
                }
                sqlConnection1.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obtenID();

            //if (!backgroundWorker1.IsBusy)
            //{
            //    progressBar1.Value = 0;
            //    button1.Enabled = false;
            //    button2.Enabled = false;
            //    backgroundWorker1.RunWorkerAsync();
            //}
            insertaMat();
            insertaLP();
            insertaIS();
            insertaSP();
            insertaCD();
            MessageBox.Show("Guardado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            conta = 0;
            
            //progressBar1.Value = 0;
            
            insertaMat();
            insertaLP();
            insertaIS();
            insertaSP();
            backgroundWorker1.ReportProgress(100);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Guardado");
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            id();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button2.Enabled = true;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
