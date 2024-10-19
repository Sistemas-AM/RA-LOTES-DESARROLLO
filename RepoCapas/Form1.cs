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

namespace RepoCapas
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
            cargacombo();
        }

        private void GenerarExcel()
        {
            Thread hilo1 = new Thread(new ThreadStart(pantalla));
            hilo1.Start();
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepoCapas\Plantilla\PlantillaCapas.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\RepoCapas\Plantilla\Copia\Reporte" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);
            int count = 0;
            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand("SpCapas", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecini", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@fecfin", dateTimePicker2.Value.ToString("MM/dd/yyyy"));
            if (radioButton2.Checked)
            {
                cmd.Parameters.AddWithValue("@sucu", comboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@tipo", "2");
            }
            else if (radioButton1.Checked)
            {
                cmd.Parameters.AddWithValue("@sucu", "0");
                cmd.Parameters.AddWithValue("@tipo", "1");
            }

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            DataTable miTabla = ds.Tables[0];
            //dataGridView1.DataSource = miTabla;
            sqlConnection1.Close();
            
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];
            if (radioButton2.Checked)
            {
                x.Range["A3"].Value = "SUCURSAL:";
                x.Range["B3"].Value = comboBox1.Text;
            }
            x.Range["I4"].Value = DateTime.Now.Date.ToString("MM/dd/yyyy");
            x.Range["I5"].Value = DateTime.Now.ToShortTimeString();
            x.Range["B4"].Value = "Del: " + dateTimePicker1.Value.ToString("MM/dd/yyyy") + " al: " + dateTimePicker2.Value.ToString("MM/dd/yyyy");
            int row = 0;
            int plus = 9;
            int jCambio = 0;
            string familia = miTabla.Rows[0].ItemArray[9].ToString();

            float sumauni =     float.Parse(miTabla.Rows[0].ItemArray[2].ToString());
            float sumaCosto =   float.Parse(miTabla.Rows[0].ItemArray[3].ToString());
            float sumaTotal =   float.Parse(miTabla.Rows[0].ItemArray[4].ToString());
            float sumaCanti =   float.Parse(miTabla.Rows[0].ItemArray[5].ToString());
            float sumaPrecio =  float.Parse(miTabla.Rows[0].ItemArray[6].ToString());
            float sumaTotal2 =  float.Parse(miTabla.Rows[0].ItemArray[7].ToString());
            float sumaMC =      float.Parse(miTabla.Rows[0].ItemArray[8].ToString());

            float totaluni = 0;
            float totalcosto = 0;
            float totaltotal = 0;
            float totalcanti = 0;
            float totalprecio = 0;
            float totaltotal2 = 0;
            float totalmc = 0;


            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count-1; k++)
                {
                    
                    if (row != 0)
                    {
                        if (miTabla.Rows[j].ItemArray[9].ToString() == familia)
                        {
                            if (jCambio != j)
                            {
                                sumauni =    sumauni + float.Parse(miTabla.Rows[j].ItemArray[2].ToString());
                                sumaCosto =  sumaCosto + float.Parse(miTabla.Rows[j].ItemArray[3].ToString());
                                sumaTotal =  sumaTotal + float.Parse(miTabla.Rows[j].ItemArray[4].ToString());
                                sumaCanti =  sumaCanti + float.Parse(miTabla.Rows[j].ItemArray[5].ToString());
                                sumaPrecio = sumaPrecio + float.Parse(miTabla.Rows[j].ItemArray[6].ToString());
                                sumaTotal2 = sumaTotal2 + float.Parse(miTabla.Rows[j].ItemArray[7].ToString());
                                sumaMC =     sumaMC + float.Parse(miTabla.Rows[j].ItemArray[8].ToString());
                                
                                jCambio = j;
                            }
                            if (j == miTabla.Rows.Count - 1 && k == miTabla.Columns.Count - 2)
                            {
                                totaluni = totaluni + sumauni;

                                totalcosto = totalcosto + sumaCosto;
                                totaltotal = totaltotal + sumaTotal;
                                totalcanti = totalcanti + sumaCanti;
                                totalprecio = totalprecio + sumaPrecio;
                                totaltotal2 = totaltotal2 + sumaTotal2;
                                totalmc = totalmc + sumaMC;


                                x.Cells[j + plus, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                                x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                                x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                                x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                                x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                                plus = plus + 2;

                                x.Cells[j + plus, 2] = "Total de "+familia;

                                x.Cells[j + plus, 2].Font.Bold = true;
                                x.Cells[j + plus, 3].Font.Bold = true;
                                x.Cells[j + plus, 4].Font.Bold = true;
                                x.Cells[j + plus, 5].Font.Bold = true;
                                x.Cells[j + plus, 6].Font.Bold = true;
                                x.Cells[j + plus, 7].Font.Bold = true;
                                x.Cells[j + plus, 8].Font.Bold = true;
                                x.Cells[j + plus, 9].Font.Bold = true;

                                x.Cells[j + plus, 3]  = sumauni.ToString();
                                x.Cells[j + plus, 4]  = sumaCosto.ToString();
                                x.Cells[j + plus, 5]  = sumaTotal.ToString();
                                x.Cells[j + plus, 6]  = sumaCanti.ToString();
                                x.Cells[j + plus, 7]  = sumaPrecio.ToString();
                                x.Cells[j + plus, 8]  = sumaTotal2.ToString();
                                x.Cells[j + plus, 9] = sumaMC.ToString();

                                x.Cells[j + plus + 2, 2] = "Totales:";
                                x.Cells[j + plus + 2, 3] = totaluni.ToString();
                                x.Cells[j + plus + 2, 4] = totalcosto.ToString();
                                x.Cells[j + plus + 2, 5] = totaltotal.ToString();
                                x.Cells[j + plus + 2, 6] = totalcanti.ToString();
                                x.Cells[j + plus + 2, 7] = totalprecio.ToString();
                                x.Cells[j + plus + 2, 8] = totaltotal2.ToString();
                                x.Cells[j + plus + 2, 9] = totalmc.ToString();

                                x.Cells[j + plus + 2, 2].Font.Bold = true;
                                x.Cells[j + plus + 2, 3].Font.Bold = true;
                                x.Cells[j + plus + 2, 4].Font.Bold = true;
                                x.Cells[j + plus + 2, 5].Font.Bold = true;
                                x.Cells[j + plus + 2, 6].Font.Bold = true;
                                x.Cells[j + plus + 2, 7].Font.Bold = true;
                                x.Cells[j + plus + 2, 8].Font.Bold = true;
                                x.Cells[j + plus + 2, 9].Font.Bold = true;


                            }

                            x.Cells[j + plus, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;

                            if (j == miTabla.Rows.Count - 1 && k == miTabla.Columns.Count - 2)
                            {
                               
                                x.Cells[j + plus, 9] = sumaMC.ToString();

                            }

                        }
                        else
                        {
                            totaluni = totaluni + sumauni;
                            totalcosto = totalcosto + sumaCosto;
                            totaltotal = totaltotal + sumaTotal;
                            totalcanti = totalcanti + sumaCanti;
                            totalprecio = totalprecio + sumaPrecio;
                            totaltotal2 = totaltotal2 + sumaTotal2;
                            totalmc = totalmc + sumaMC;

                            plus = plus + 1;

                            x.Cells[j + plus, 2] = "Total de " + familia;
                            x.Cells[j + plus, 3] = sumauni.ToString();
                            x.Cells[j + plus, 4] = sumaCosto.ToString();
                            x.Cells[j + plus, 5] = sumaTotal.ToString();
                            x.Cells[j + plus, 6] = sumaCanti.ToString();
                            x.Cells[j + plus, 7] = sumaPrecio.ToString();
                            x.Cells[j + plus, 8] = sumaTotal2.ToString();
                            x.Cells[j + plus, 9] = sumaMC.ToString();

                            x.Cells[j + plus, 2].Font.Bold = true;
                            x.Cells[j + plus, 3].Font.Bold = true;
                            x.Cells[j + plus, 4].Font.Bold = true;
                            x.Cells[j + plus, 5].Font.Bold = true;
                            x.Cells[j + plus, 6].Font.Bold = true;
                            x.Cells[j + plus, 7].Font.Bold = true;
                            x.Cells[j + plus, 8].Font.Bold = true;
                            x.Cells[j + plus, 9].Font.Bold = true;

                            plus = plus + 2;
                            x.Cells[j + plus, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                            x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                            familia = miTabla.Rows[j].ItemArray[9].ToString();

                            sumauni =   0;
                            sumaCosto = 0;
                            sumaTotal = 0;
                            sumaCanti = 0;
                            sumaPrecio =0;
                            sumaTotal2 =0;
                            sumaMC = 0;  
                        }
                    }else
                    {

                        x.Cells[j + plus, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                        x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                        x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                        x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                        x.Cells[j + plus, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    }
                    
                   //colu += 1;
                }
                row += 1;
            }

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            Thread.Sleep(1000);
            hilo1.Abort();
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }

        private void cargacombo()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select idalmacen, sucnom from folios where idalmacen = 1 or idalmacen = 3 or idalmacen = 4 or idalmacen = 5 or idalmacen = 8", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "idalmacen";
            comboBox1.DisplayMember = "sucnom";
            comboBox1.DataSource = dt;

            sqlConnection2.Close();
        }

        private void pantalla()
        {
            using (Pantacar pc = new Pantacar())
            {
                pc.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Thread excel = new Thread(new ThreadStart(GenerarExcel));
           // excel.Start();
           //// excel.Abort();
            GenerarExcel();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            label1.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
