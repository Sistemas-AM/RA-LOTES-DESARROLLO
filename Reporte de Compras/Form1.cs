using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Reporte_de_Compras
{
    public partial class Form1 : Form
    {

        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        int count = 0;
        public Form1()
        {
            InitializeComponent();
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select Codigo from carcom where fecha between '"+DateTime.Parse(dateTimePicker1.Text).ToString("yyyy-MM-dd")+"' and '"+DateTime.Parse(dateTimePicker2.Text).ToString("yyyy-MM-dd")+"'", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            
            dt.Load(reader);

            comboBox1.ValueMember = "Codigo";
            comboBox1.DisplayMember = "Codigo";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int almacen = 1;
            double existencia = 0;

            while (almacen < 6)
            {

                sqlConnection2.Open();
                SqlCommand cmd = new SqlCommand("conexia", sqlConnection2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                cmd.Parameters.AddWithValue("@codigo", Convert.ToString(comboBox1.Text));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataTable ds = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    DataRow row = ds.Rows[0];
                    if (almacen != 2)
                    {
                        existencia = existencia + Convert.ToDouble(row["existencia"]);
                    } 

                    almacen = almacen + 1;
                }
                sqlConnection2.Close();
            }
            VariablesCompartidas.existencia = existencia.ToString();
            
            VariablesCompartidas.fecini = dateTimePicker1.Text;
            VariablesCompartidas.fecfin = dateTimePicker2.Text;
            VariablesCompartidas.codigo = comboBox1.Text;
            VariablesCompartidas.mes = dateTimePicker1.Value.Month.ToString();

            using (reporte rp = new reporte())
            {
                rp.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void GeneraExcel()
        {
            //            sqlConnection1.Open();
            //            string sql = @"select Folio, Fecha, CodProveedor, Proveedor, Pzasrec, PzasFac, DifenPzas, Kpt, KgTeo, Kgf, KgFac, (KgTeo - KgFac) as dif
            //, ((KgTeo - KgFac)/Kgf) as util, nombre from carcom where Codigo = '"+comboBox1.Text+ "' and fecha between '"+dateTimePicker1.Value.ToShortDateString()+"' and '"+dateTimePicker2.Value.ToShortDateString()+"'";
            //            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            //            DataSet ds = new DataSet();
            //            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //            da.Fill(ds);
            //            DataTable miTabla = ds.Tables[0];

            //            sqlConnection1.Close();

            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Reporte de Compras\Plantilla\Reporte.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Reporte de Compras\Plantilla\Copia\Reporte-" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];
            string codigo = "";
            int cuenta = 0;

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //  {
            //      codigo = row.Cells["co"].Value.ToString();
            //      x.Cells[5, cuenta] = codigo;
            //      cuenta++;

            //      //}
            //  }
            x.Range["C4"].Value = comboBox1.Text;
            x.Range["C5"].Value = dataGridView1.Rows[0].Cells[13].Value.ToString();
            x.Range["F4"].Value = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            x.Range["H4"].Value = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            x.Range["I6"].Value = VariablesCompartidas.existencia;

            //Recorremos el DataGridView Esto es para el contenido de las columnas
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count-1; j++)
                {
                   
                       // DateTime fec = DateTime.Parse(dataGridView1.Rows[i].Cells["Column" + (j + 1).ToString()].Value.ToString());
                        x.Cells[i + 8, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    
                }
            }

            //x.Cells[5, 1] = "Hola que tal";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);


        }

        private void cargagrid()
        {
            SqlCommand cmd = new SqlCommand(@"select Folio, Fecha, CodProveedor, Proveedor, Pzasrec, PzasFac, DifenPzas, Kpt, KgTeo, Kgf, KgFac, (KgTeo - KgFac) as dif
            ,((KgTeo - KgFac)/KgFac)*100 as util, nombre from carcom where Codigo = '" + comboBox1.Text + "' and fecha between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void existencia()
        {
            int almacen = 1;
            double existencia = 0;

            while (almacen < 9)
            {

                sqlConnection2.Open();
                SqlCommand cmd = new SqlCommand("conexia", sqlConnection2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                cmd.Parameters.AddWithValue("@codigo", Convert.ToString(comboBox1.Text));
                //SqlDataReader dr = cmd.ExecuteReader();

                DataTable ds = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    DataRow row = ds.Rows[0];
                    if (almacen != 6 && almacen != 7 && almacen !=2)
                    {
                        existencia = existencia + Convert.ToDouble(row["existencia"]);
                    }

                    almacen = almacen + 1;
                }
                sqlConnection2.Close();
            }
            VariablesCompartidas.existencia = existencia.ToString();

            //VariablesCompartidas.fecini = dateTimePicker1.Text;
            //VariablesCompartidas.fecfin = dateTimePicker2.Text;
            //VariablesCompartidas.codigo = comboBox1.Text;
            //VariablesCompartidas.mes = dateTimePicker1.Value.Month.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            existencia();
            cargagrid();
            
            GeneraExcel();
        }
    }
    }