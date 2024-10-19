using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace repoInv
{
    public partial class Form1 : Form
    {
        //conexion
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
            cargacombo();

        }

        private void cargacombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select CIDALMACEN, CNOMBREALMACEN from admAlmacenes", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNOMBREALMACEN", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "cidalmacen";
            comboBox1.DisplayMember = "CNOMBREALMACEN";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.almacen = comboBox1.SelectedValue.ToString();
            Variablescompartidas.nombre = comboBox1.Text;
            using (reporte rp = new reporte())
            {
                rp.ShowDialog();
            }
        }

        public class Variablescompartidas
        {
            public static string almacen { get; set; }
            public static string nombre { get; set; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\repoinv\Plantilla\Reporte Inventario.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\repoinv\Plantilla\Copia\Reporte Inventario Cop"+variables.usuario + DateTime.Now.Second.ToString()+".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand("spInventariof", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("MM-dd-yyyy"));
            cmd.Parameters.AddWithValue("@almacen", comboBox1.SelectedValue.ToString());
         

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable miTabla = ds.Tables[0];

            sqlConnection1.Close();

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];

           // x.Cells[2, 3] = "Periodo del " + dtfecini.Text + " al " + dtfecfin.Text;
            x.Cells[4, 1] = "Proforma " + comboBox1.Text;
            x.Cells[3, 9] = DateTime.Now.ToShortDateString();
            x.Cells[4, 9] = DateTime.Now.ToShortTimeString();

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k  < miTabla.Columns.Count; k++)
                {

                    x.Cells[j + 7, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();

                }
            }

            //x.Range["I11"].Value = "XL";

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
    }
}
