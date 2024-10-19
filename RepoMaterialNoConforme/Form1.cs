using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepoMaterialNoConforme
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string peso = "";
        string copia = "";
        int proceso = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
            {
                Cotizacion.Variablescompartidas.codigoNoConfo = "0";
                using (Cotizacion.Productos cp = new Cotizacion.Productos())
                {
                    cp.ShowDialog();
                }

                if (Cotizacion.Variablescompartidas.codigoNoConfo != "0")
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value == null)
                    {
                        obtenpeso(Cotizacion.Variablescompartidas.codigoNoConfo);

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Codigo"].Value = Cotizacion.Variablescompartidas.codigoNoConfo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Producto"].Value = Cotizacion.Variablescompartidas.NombreNoConfo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Kilos"].Value = peso;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cant"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Total"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Observaciones"].Value = "-";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Quita"].Value = "X";
                    }else
                    {
                        obtenpeso(Cotizacion.Variablescompartidas.codigoNoConfo);

                        //dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value = Cotizacion.Variablescompartidas.codigoNoConfo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Producto"].Value = Cotizacion.Variablescompartidas.NombreNoConfo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Kilos"].Value = peso;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cant"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Total"].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Observaciones"].Value = "-";
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
            cmd.CommandText = "select CPRECIO10  from admProductos where CCODIGOPRODUCTO = '" + codigo + "'";
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
                dataGridView1.Rows[e.RowIndex].Cells["Total"].Value = Math.Round(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Kilos"].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Cant"].Value.ToString()), 2);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ingresa solo numeros");
            }
            catch (NullReferenceException)
            {

            }
        }

        private void delete()
        {
            string sql = "delete from NoConforme where NumTrailer = @param1";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@param1", trailer.Text);

            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void guardar()
        {
           
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                try
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    string sql = @"insert into NoConforme values (@Proveedor, @Fecha, @NumTrailer, @Codigo, @Nombre,
                    @kilos, @cantidad, @Total, @Observaciones)";
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection2);

                    cmd.Parameters.AddWithValue("@Proveedor", proveedor.Text);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@NumTrailer", trailer.Text);
                    cmd.Parameters.AddWithValue("@Codigo", row.Cells["Codigo"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Nombre", row.Cells["Producto"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Kilos", row.Cells["Kilos"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cant"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Total", row.Cells["Total"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Observaciones", row.Cells["Observaciones"].Value.ToString());

                    sqlConnection2.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                }
                catch (NullReferenceException)
                {

                    
                }
                    
                }
           
            MessageBox.Show("Guardado");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (trailer.Text !="")
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
                MessageBox.Show("Agrega Un Número De Trailer");
                trailer.Focus();
            }
        }

        private void cargar(string codigoTra)
        {
            int count = 0;
            cmd.CommandText = "select * from NoConforme where NumTrailer = '" + codigoTra + "'";
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
                    trailer.Text = codigoTra;
                    proveedor.Text = reader["proveedor"].ToString();
                    dateTimePicker1.Text = reader["fecha"].ToString();
                    dataGridView1.Rows[count].Cells["Codigo"].Value = reader["codigo"].ToString();
                    dataGridView1.Rows[count].Cells["Producto"].Value = reader["nombre"].ToString();
                    dataGridView1.Rows[count].Cells["Kilos"].Value = reader["Kilos"].ToString();
                    dataGridView1.Rows[count].Cells["Cant"].Value = reader["Cantidad"].ToString();
                    dataGridView1.Rows[count].Cells["Total"].Value = reader["Total"].ToString();
                    dataGridView1.Rows[count].Cells["Observaciones"].Value = reader["Observaciones"].ToString();
                    dataGridView1.Rows[count].Cells["Quita"].Value = "X";
                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            trailer.Clear();
            proveedor.Clear();
            using (Trailers tr = new Trailers())
            {
                tr.ShowDialog();
            }
            cargar(Trailers.codigo);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            generaExcel();
        }

        private void generaExcel()
        {
            proceso = 0;
            progressBar1.Value = 0;
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepoMaterialNoConforme\Plantilla\Plantilla.xlsx";
            copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepoMaterialNoConforme\Plantilla\Copia\Formato" + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection2.Open();
            string sql = @"select codigo, nombre, kilos, cantidad, total, observaciones from noconforme where numtrailer = '"+trailer.Text+"'";
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
            x.Range["D2"].Value = trailer.Text;
            x.Range["F2"].Value = proveedor.Text;
            x.Range["D3"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("dd/MM/yyyy");

            double columnas = 100 / miTabla.Rows.Count;
            double filas = columnas / miTabla.Columns.Count;
            int pro = Convert.ToInt32(filas);

           

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    x.Cells[j + 5, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                    x.Cells[j + 5, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                    x.Cells[j + 5, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                    x.Cells[j + 5, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    x.Cells[j + 5, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    proceso += pro;
                    if (proceso >=100 )
                    {
                        proceso = 99;
                    }
                    progressBar1.Value = proceso;
                }
            }

            //x.Range["I11"].Value = "XL";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            progressBar1.Value = 100;
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
            catch (Exception)
            {


            }
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //try
            //{
            //    System.IO.File.Delete(Path.GetFullPath(copia));
            //}
            //catch (Exception)
            //{


            //}
            //this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            trailer.Clear();
            proveedor.Clear();
            dataGridView1.Rows.Clear();
            dateTimePicker1.Text = DateTime.Now.ToString();
            progressBar1.Value = 0;
        }
    }
}
