using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CatalagoPrecios
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet.admProductos' Puede moverla o quitarla según sea necesario.
            this.admProductosTableAdapter.Fill(this.adACEROS_MEXICODataSet.admProductos);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var columna = new[] { "CCODIGOPRODUCTO", "CNOMBREPRODUCTO", "CIMPORTEEXTRA2", "CIMPORTEEXTRA1", "CPRECIO1" }.ToList();
            var Binding = (BindingSource)dataGridView1.DataSource;
            try
            {
                if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < 2 && !textBox1.Text.Equals(""))
                {
                    Binding.Filter = string.Format("{1} LIKE '{0}%'", textBox1.Text.Trim().Replace("'", "''"), columna[comboBox1.SelectedIndex]);
                    //dataGridView1.Refresh();
                }
                else if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex > 1 && !textBox1.Text.Equals(""))
                {
                    Binding.Filter = string.Format("{1} = '{0}'", Double.Parse(textBox1.Text.Trim().Replace("'", "''")), columna[comboBox1.SelectedIndex]);
                    //dataGridView1.Refresh();
                }
                else if (textBox1.Text.Equals(""))
                {
                    Binding.Filter = string.Empty;

                }
            }
            catch (EvaluateException)
            {
                MessageBox.Show("No se permite ese tipo de caracter");
                textBox1.Text = "";
            }catch (FormatException)
            {
                MessageBox.Show("Favor de solo usar números");
                textBox1.Text = "";
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    hoja_trabajo.Cells[1, i] = dataGridView1.Columns[i].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count-1; j++)
                    {
                        hoja_trabajo.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j + 1].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                MessageBox.Show("Reporte guardado con éxito");
                libros_trabajo.Close(true);
                aplicacion.Quit();
                DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
                if (AbrirExcel == DialogResult.Yes)
                {
                    aplicacion.Visible = true;
                    aplicacion.Workbooks.Open(fichero.FileName.ToString());
                }
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(aplicacion);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
