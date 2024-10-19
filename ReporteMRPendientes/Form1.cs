using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using MaterialSkin;
using System.IO;

namespace ReporteMRPendientes
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public static string sucursal { get; set; }
        public Form1()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            BtnGuardar.BackColor = ColorTranslator.FromHtml("#76CA62");
            btnEliminar.BackColor = ColorTranslator.FromHtml("#D66F6F");


            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("Select sucnom,idfmr from folios", sqlConnection1);

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Columns.Add("idfmr", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "sucnom";
            comboBox1.DisplayMember = "sucnom";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();


            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox1.Enabled = true;
            }else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucural;
            }
        }

        private void Generaexcel()
        {
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName  + @"\ReporteMRPendientes\Plantilla\Plantilla.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteMRPendientes\Plantilla\Copia\PendientesPorEntregar-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection1.Open();
            string sql = @"with pendiente as (  
            select factura, codigo, facturado,sum(entregado) as entregados,
            facturado -sum(entregado) as PendientesEntrega, tipo
            from matres 
            where sucursal = '"+comboBox1.Text+ "' group by facturado, codigo, factura, tipo) select factura, tipo, codigo, facturado, entregados, pendientesEntrega from pendiente where pendientesEntrega > 0";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable miTabla = ds.Tables[0];

            sqlConnection1.Close();

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;
            try
            {


                sheet = excel.Workbooks.Open(copia);
                x = excel.Worksheets["Hoja1"];

                for (int j = 0; j < miTabla.Rows.Count; j++)
                {
                    for (int k = 0; k < miTabla.Columns.Count; k++)
                    {
                        x.Cells[j + 4, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    }
                }

                //x.Range["I11"].Value = "XL";

                sheet.Save();
                sheet.Close(0);
                excel.Quit();
                //hiloCarga.Abort();
                DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
                if (AbrirExcel == DialogResult.Yes)
                {
                    excel.Visible = true;
                    excel.Workbooks.Open(copia);
                }
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
            }
            catch (Exception)
            {

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
            }
            
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            sucursal = comboBox1.Text;
            using (Reporte rp = new Reporte())
            {
                rp.ShowDialog();
            }
            //Generaexcel();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
    
}