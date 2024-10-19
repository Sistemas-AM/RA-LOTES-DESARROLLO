using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RepTraspasos
{
    public partial class Form1 : Form

    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        string iddoc;
        public Form1()
        {
            InitializeComponent();
            cargaCombo();
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucural;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iddoc = comboBox1.SelectedValue.ToString();
        }
        private void cargaCombo()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucursal, sucnom, idtrassal from folios where idtrassal != 0", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "idtrassal";
            comboBox1.DisplayMember = "sucnom";
            comboBox1.DataSource = dt;
            sqlConnection2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepTraspasos\Plantilla\Reporte de Traspasos.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepTraspasos\Plantilla\Copia\Reporte de Traspasos Cop.xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand("traspasos", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fecini", dtfecini.Value.ToString("MM-dd-yyyy"));
            cmd.Parameters.AddWithValue("@fecfin", dtfecfin.Value.ToString("MM-dd-yyyy"));
            cmd.Parameters.AddWithValue("@documento", iddoc);

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

            x.Cells[2,3] = "Periodo del " + dtfecini.Text + " al "+dtfecfin.Text;
            x.Cells[4,1] = "Traspasos "+comboBox1.Text;
            x.Cells[3, 9] = DateTime.Now.ToShortDateString();
            x.Cells[4, 9] = DateTime.Now.ToShortTimeString();

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
