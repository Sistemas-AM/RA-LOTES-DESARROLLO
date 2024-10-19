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

namespace ReporteCapSucursales
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int proceso = 0;
        string copia = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generaexcel();
        }

        private void generaexcel()
        {
            progressBar1.Value = 0;
            proceso = 0;
           string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteCapSucursales\Plantilla\Plantilla.xlsx";
            copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteCapSucursales\Plantilla\Copia\Formato" + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection1.Open();

            string sp = "";
            if (DateTime.Parse(dateTimePicker1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                sp = "spCapSucursalesHOYBIEN";
            }else
            {
                sp = "spCapSucursalesBIEN";
            }
            SqlCommand cmd = new SqlCommand(sp, sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            //da.Fill(ds);
            DataTable miTabla = ds.Tables[0];
            sqlConnection1.Close();

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];
            x.Range["B1"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy");
            //x.Range["G2"].Value = textBox2.Text;
            //x.Range["K2"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("dd/MM/yyyy");

            //double columnas = 100 / miTabla.Rows.Count;
            //double filas = columnas / miTabla.Columns.Count;
            int pro = 1;

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    //x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                    //x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                    //x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                    //x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    x.Cells[j + 4, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();

                   
                }
                proceso += 1;
                if (proceso >= 1000)
                {
                    proceso = 0;
                }
                progressBar1.Value = proceso;
            }

            //x.Range["I11"].Value = "XL";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            progressBar1.Value = 1000;
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
