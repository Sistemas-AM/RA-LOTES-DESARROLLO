using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ReporteTransitoTraspasos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        string copia = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ExcelTraspaso();
        }

        private void pantallaCarga()
        {
            using (PantallaCarga pc = new PantallaCarga())
            {
                try
                {
                    pc.ShowDialog();
                }
                catch (ThreadAbortException)
                {

                    pc.Close();
                }
            }
        }

        private void ExcelTraspaso()
        {
            Thread hilocarga = new Thread(new ThreadStart(pantallaCarga));
            hilocarga.Start();
            int proceso = 0;
            //progressBar1.Value = 0;
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteTransitoTraspasos\Plantilla\Plantilla.xlsx";
            copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteTransitoTraspasos\Plantilla\Copia\Formato" + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }


            vArchivo.CopyTo(copia);

            sqlConnection2.Open();
            string sql = @"select Folio, Fecha, Codigo, Nombre, Cantidad, Peso,(Cantidad * peso) as total, Destino, Desde, DestinoReal from Traspasos where Estatus != 'R' order by folio, fecha";
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
            x.Range["I3"].Value = DateTime.Now.ToString("dd/MM/yyyy");
            x.Range["I4"].Value = DateTime.Now.ToShortTimeString();
            //x.Range["K2"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("dd/MM/yyyy");

            //double columnas = 100 / miTabla.Rows.Count;
            //double filas = columnas / miTabla.Columns.Count;
            //int pro = Convert.ToInt32(filas);

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    x.Cells[j + 6, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();


                    proceso += 1;
                    if (proceso >= 100)
                    {
                        proceso = 99;
                    }
                    //progressBar1.Value = proceso;
                }
            }

            //x.Range["I11"].Value = "XL";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            //progressBar1.Value = 100;
            Thread.Sleep(5000);
            hilocarga.Abort();
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }
    }
}