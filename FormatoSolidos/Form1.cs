using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormatoSolidos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string peso = "";
        string copia = "";
        //int proceso = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
            {
                Cotizacion.Variablescompartidas.CodigoProFormato = "0";
                using (Cotizacion.Productos cp = new Cotizacion.Productos())
                {
                    cp.ShowDialog();
                }
                if (Cotizacion.Variablescompartidas.CodigoProFormato != "0")
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value == null)
                    {
                        obtenpeso(Cotizacion.Variablescompartidas.CodigoProFormato);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Codigo"].Value = Cotizacion.Variablescompartidas.CodigoProFormato;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Nombre"].Value = Cotizacion.Variablescompartidas.nombreFormato;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PesoTeo"].Value = peso;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a1"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a2"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a3"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a4"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a5"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a6"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a7"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a8"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a9"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["a10"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["numAtados"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Kilos"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PiezasTeo"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["PiezasFis"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["difPieza"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["difKil"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Quita"].Value = "X";
                    }else
                    {
                        obtenpeso(Cotizacion.Variablescompartidas.CodigoProFormato);
                        //dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value = Cotizacion.Variablescompartidas.CodigoProFormato;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value = Cotizacion.Variablescompartidas.nombreFormato;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PesoTeo"].Value = peso;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a1"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a2"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a3"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a4"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a5"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a6"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a7"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a8"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a9"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["a10"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["numAtados"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Kilos"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PiezasTeo"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["PiezasFis"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["difPieza"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["difKil"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Quita"].Value = "X";
                    }

                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Quita")
            {
                try
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
                catch (InvalidOperationException)
                {
                    
                }
            }
            
        }

        private void obtenpeso(string codigo)
        {
            peso = "";
            cmd.CommandText = "select CPRECIO10  from admProductos where CCODIGOPRODUCTO = '"+codigo+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                peso = reader["CPRECIO10"].ToString();

            }
            sqlConnection1.Close();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Cells["PiezasTeo"].Value = Math.Round(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Kilos"].Value.ToString()) / float.Parse(dataGridView1.Rows[e.RowIndex].Cells["PesoTeo"].Value.ToString()), 2);
                dataGridView1.Rows[e.RowIndex].Cells["PiezasFis"].Value = Math.Round(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a1"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a2"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a3"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a4"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a5"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a6"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a7"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a8"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a9"].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells["a10"].Value.ToString()) , 2);

                dataGridView1.Rows[e.RowIndex].Cells["difPieza"].Value = Math.Round(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["PiezasTeo"].Value.ToString()) - float.Parse(dataGridView1.Rows[e.RowIndex].Cells["PiezasFis"].Value.ToString()), 2);
                dataGridView1.Rows[e.RowIndex].Cells["difKil"].Value = Math.Round(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["difPieza"].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells["PesoTeo"].Value.ToString()), 2);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ingresa solo numeros");
            }
            catch (NullReferenceException)
            {

            }
        }

        private void guardar()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    string sql = @"insert into solidos values (@Flete, @proveedor, @fecha, @PiezasTeo, @PesoTeo,
                        @Kilos, @PiezasFisi, @NumAtados, @a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @difPiezas,
                        @difKilos, @codigopro, @nombrepro)";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                    cmd.Parameters.AddWithValue("@Flete", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Proveedor", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy"));

                    cmd.Parameters.AddWithValue("@PiezasTeo", row.Cells["PiezasTeo"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PesoTeo", row.Cells["PesoTeo"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Kilos", row.Cells["Kilos"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PiezasFisi", row.Cells["PiezasFis"].Value.ToString());
                    cmd.Parameters.AddWithValue("@NumAtados", row.Cells["NumAtados"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a1", row.Cells["a1"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a2", row.Cells["a2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a3", row.Cells["a3"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a4", row.Cells["a4"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a5", row.Cells["a5"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a6", row.Cells["a6"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a7", row.Cells["a7"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a8", row.Cells["a8"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a9", row.Cells["a9"].Value.ToString());
                    cmd.Parameters.AddWithValue("@a10", row.Cells["a10"].Value.ToString());
                    cmd.Parameters.AddWithValue("@DifPiezas", row.Cells["difPieza"].Value.ToString());
                    cmd.Parameters.AddWithValue("@difKilos", row.Cells["difKil"].Value.ToString());
                    cmd.Parameters.AddWithValue("@codigopro", row.Cells["codigo"].Value.ToString());
                    cmd.Parameters.AddWithValue("@nombrepro", row.Cells["nombre"].Value.ToString());
                    
                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    
                }
                
            }
            catch (NullReferenceException)
            {


            }
            MessageBox.Show("Guardado");

        }

        private void delete()
        {
            string sql = "delete from solidos where flete = @param1";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@param1", textBox1.Text); 
            
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                delete();
                guardar();
                DialogResult result = MessageBox.Show("Desea Imprimir?", "Imprimir", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    generaExcel();
                }
                else if (result == DialogResult.No)
                {

                }
            }
            else
            {
                MessageBox.Show("Ingresa Un Flete");
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            using (Fletes fp = new Fletes())
            {
                fp.ShowDialog();
            }

            //int count = 0; Este se agrega al inicio del programa
            int count = 0;
            cmd.CommandText = "select * from solidos where flete = '"+Variablescompartidas.codigo+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    textBox1.Text = reader["flete"].ToString();
                    textBox2.Text = reader["proveedor"].ToString();
                    dateTimePicker1.Text = reader["fecha"].ToString();
                    dataGridView1.Rows[count].Cells["Codigo"].Value = reader["CodigoPro"].ToString();
                    dataGridView1.Rows[count].Cells["Nombre"].Value = reader["NombrePro"].ToString();
                    dataGridView1.Rows[count].Cells["PiezasTeo"].Value = reader["PiezasTeo"].ToString();
                    dataGridView1.Rows[count].Cells["PesoTeo"].Value = reader["PesoTeo"].ToString();
                    dataGridView1.Rows[count].Cells["Kilos"].Value = reader["Kilos"].ToString();
                    dataGridView1.Rows[count].Cells["PiezasFis"].Value = reader["PiezasFisi"].ToString();
                    dataGridView1.Rows[count].Cells["NumAtados"].Value = reader["NumAtados"].ToString();
                    dataGridView1.Rows[count].Cells["a1"].Value = reader["a1"].ToString();
                    dataGridView1.Rows[count].Cells["a2"].Value = reader["a2"].ToString();
                    dataGridView1.Rows[count].Cells["a3"].Value = reader["a3"].ToString();
                    dataGridView1.Rows[count].Cells["a4"].Value = reader["a4"].ToString();
                    dataGridView1.Rows[count].Cells["a5"].Value = reader["a5"].ToString();
                    dataGridView1.Rows[count].Cells["a6"].Value = reader["a6"].ToString();
                    dataGridView1.Rows[count].Cells["a7"].Value = reader["a7"].ToString();
                    dataGridView1.Rows[count].Cells["a8"].Value = reader["a8"].ToString();
                    dataGridView1.Rows[count].Cells["a9"].Value = reader["a9"].ToString();
                    dataGridView1.Rows[count].Cells["a10"].Value = reader["a10"].ToString();
                    dataGridView1.Rows[count].Cells["difPieza"].Value = reader["difPiezas"].ToString();
                    dataGridView1.Rows[count].Cells["difKil"].Value = reader["difKilos"].ToString();
                    dataGridView1.Rows[count].Cells["Quita"].Value = "X";

                    count++; 

                }
            }
            catch (Exception)
            {


            }
            sqlConnection2.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            generaExcel();
        }

        private void pantalla()
        {
            using (PantallaCarga pc = new PantallaCarga())
            {
                pc.ShowDialog();
            }
        }

        private void generaExcel()
        {
            Thread hilo1 = new Thread(new ThreadStart(pantalla));
            hilo1.Start();
            // progressBar1.Value = 0;
            //proceso = 0;
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\FormatoSolidos\Plantilla\Plantilla.xlsx";
             copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\FormatoSolidos\Plantilla\Copia\Formato" + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection2.Open();
            string sql = @"select codigopro, nombrepro, piezasteo, pesoteo, kilos, piezasfisi, numatados, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, difpiezas, difkilos  from solidos where flete = '" + textBox1.Text+"'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable miTabla = ds.Tables[0];

            sqlConnection2.Close();

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];
            x.Range["E2"].Value = textBox1.Text;
            x.Range["G2"].Value = textBox2.Text;
            x.Range["K2"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("dd/MM/yyyy");

            double columnas = 100 / miTabla.Rows.Count;
            double filas = columnas / miTabla.Columns.Count;
            int pro = Convert.ToInt32(filas);

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    x.Cells[j + 6, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    //proceso += pro;
                    //if (proceso >= 100)
                    //{
                    //    proceso = 99;
                    //}
                    ////progressBar1.Value = proceso;
                }
            }

            //x.Range["I11"].Value = "XL";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            //progressBar1.Value = 100;
            Thread.Sleep(5000);
            hilo1.Abort();
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(Path.GetFullPath(copia));
            }
            catch (ArgumentException)
            {

                
            }
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            dataGridView1.Rows.Clear();
            dateTimePicker1.Text = DateTime.Now.ToString();
            //progressBar1.Value = 0;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.IO.File.Delete(Path.GetFullPath(copia));
            }
            catch (ArgumentException)
            {


            }
        }
    }
}
