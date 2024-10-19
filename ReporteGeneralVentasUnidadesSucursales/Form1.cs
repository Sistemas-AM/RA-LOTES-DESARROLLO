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

namespace ReporteGeneralVentasUnidadesSucursales
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);

        public Form1()
        {
            InitializeComponent();
            cargaSucursales();

            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox3.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.Text = Principal.Variablescompartidas.sucursalcorta;
            }

        }

        private void cargaSucursales()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucursal, idalmacen from folios", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucursal", typeof(string));

            dt.Load(reader);

            comboBox3.ValueMember = "idalmacen";
            comboBox3.DisplayMember = "sucursal";
            comboBox3.DataSource = dt;

            sqlConnection2.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select * from admclasificaciones where cidclasificacion > '24' and cidclasificacion < '31'", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "CIDCLASIFICACION";
            comboBox1.DisplayMember = "CNOMBRECLASIFICACION";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnection1.Close();
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select * from admclasificacionesvalores where cidclasificacion = '" + comboBox1.SelectedValue.ToString() + "'", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));

            dt.Load(reader);

            comboBox2.ValueMember = "CIDVALORCLASIFICACION";
            comboBox2.DisplayMember = "CVALORCLASIFICACION";
            comboBox2.DataSource = dt;
            sqlConnection1.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = false;

            button5.Visible = true;
            button4.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            button3.Visible = true;

            button5.Visible = false;
            button4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
                Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                using (Form2 rep = new Form2())
                {
                    rep.ShowDialog();
                }
            }
            else if (radioButton2.Checked)
            {
                if (comboBox1.Text == "Accesorios")
                {
                    Variablescompartidas.accesorios = "1";
                    DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                    DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                    Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                    Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                    Variablescompartidas.familia = comboBox2.SelectedValue.ToString();
                    Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
                    Variablescompartidas.sp = "spVentaUnidadGeneralSucursalesFamilia";

                    using (Form3 rep = new Form3())
                    {
                        rep.ShowDialog();
                    }
                }
                else
                {
                    Variablescompartidas.accesorios = "0";
                    DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                    DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                    Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                    Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                    Variablescompartidas.familia = comboBox2.SelectedValue.ToString();
                    Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
                    using (Form3 rep = new Form3())
                    {
                        rep.ShowDialog();
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Variablescompartidas.accesorios = "1";

            DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
            DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
            Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
            Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
            Variablescompartidas.familia = "";
            Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
            Variablescompartidas.sp = "spVentaUnidadGeneralSucursalesFamilia2";
            //GeneraExcel();
            using (Form3 rep = new Form3())
            {
                rep.ShowDialog();
            }
        }


        private void GeneraExcel(string sp)
        {
            Thread hilocarga = new Thread(new ThreadStart(PantallaCar));
            hilocarga.Start();
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Plantilla.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Copia\Reporte" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand(sp, sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechini", Convert.ToString(Variablescompartidas.fecini));
            cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(Variablescompartidas.fecfin));
            cmd.Parameters.AddWithValue("@familia", Convert.ToString(Variablescompartidas.familia));
            cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox3.SelectedValue.ToString()));
            //SqlDataReader dr = cmd.ExecuteReader();

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
            x.Range["C4"].Value = dateTimePicker1.Value.ToShortDateString();
            x.Range["E3"].Value = dateTimePicker2.Value.ToShortDateString();
            int co = 9;
            int co2 = 9;

            int devo1 = 9;
            int devo2 = 9;

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    if ((k + 1) >= 19)
                    {
                        x.Cells[j + 9, k + 4] = miTabla.Rows[j].ItemArray[k].ToString();
                    }
                    else
                    {
                        x.Cells[j + 9, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    }

                }
                //=SUMA(V15,X15,Z15,AB15,AD15,AF15)

                //x.Range["S" + co].Formula = "=SUM(C" + co + "+E" + co + "+G" + co + "+I" + co + "+K" + co + "+M" + co + "+O" + co + "+Q" + co + " )";
                //co += 1;

                //x.Range["T" + co2].Formula = "=SUM(D" + co2 + "+F" + co2 + "+H" + co2 + "+J" + co2 + "+L" + co2 + "+N" + co2 + "+P" + co2 + "+R" + co2 + " )";
                //co2 += 1;

                //x.Range["AH" + devo1].Formula = "=SUM(V" + devo1 + "+X" + devo1 + "+Z" + devo1 + "+AB" + devo1 + "+AD" + devo1 + "+AF" + devo1 + " )";
                //devo1 += 1;
                ////=SUMA(W9,Y9,AA9,AC9,AE9,AG9)
                //x.Range["AI" + devo2].Formula = "=SUM(W" + devo2 + "+Y" + devo2 + "+AA" + devo2 + "+AC" + devo2 + "+AE" + devo2 + "+AG" + devo2 + " )";
                //devo2 += 1;
            }
            int conta = miTabla.Rows.Count + 8;
            int colu = miTabla.Rows.Count + 10;
            x.Range["C" + colu].Formula = "=SUM(C9:" + "C" + conta + ")";
            x.Range["D" + colu].Formula = "=SUM(D9:" + "D" + conta + ")";
            x.Range["E" + colu].Formula = "=SUM(E9:" + "E" + conta + ")";
            x.Range["F" + colu].Formula = "=SUM(F9:" + "F" + conta + ")";
            //x.Range["G" + colu].Formula = "=SUM(G9:" + "G" + conta + ")";
            //x.Range["H" + colu].Formula = "=SUM(H9:" + "H" + conta + ")";
            //x.Range["I" + colu].Formula = "=SUM(I9:" + "I" + conta + ")";
            //x.Range["J" + colu].Formula = "=SUM(J9:" + "J" + conta + ")";
            //x.Range["K" + colu].Formula = "=SUM(K9:" + "K" + conta + ")";
            //x.Range["L" + colu].Formula = "=SUM(L9:" + "L" + conta + ")";
            //x.Range["M" + colu].Formula = "=SUM(M9:" + "M" + conta + ")";
            //x.Range["N" + colu].Formula = "=SUM(N9:" + "N" + conta + ")";
            //x.Range["O" + colu].Formula = "=SUM(O9:" + "O" + conta + ")";
            //x.Range["P" + colu].Formula = "=SUM(P9:" + "P" + conta + ")";
            //x.Range["Q" + colu].Formula = "=SUM(Q9:" + "Q" + conta + ")";
            //x.Range["R" + colu].Formula = "=SUM(R9:" + "R" + conta + ")";
            //x.Range["S" + colu].Formula = "=SUM(S9:" + "S" + conta + ")";
            //x.Range["T" + colu].Formula = "=SUM(T9:" + "T" + conta + ")";

            //x.Range["V" + colu].Formula = "=SUM(V9:" + "V" + conta + ")";
            //x.Range["W" + colu].Formula = "=SUM(W9:" + "W" + conta + ")";
            //x.Range["X" + colu].Formula = "=SUM(X9:" + "X" + conta + ")";
            //x.Range["Y" + colu].Formula = "=SUM(Y9:" + "Y" + conta + ")";
            //x.Range["Z" + colu].Formula = "=SUM(Z9:" + "Z" + conta + ")";
            //x.Range["AA" + colu].Formula = "=SUM(AA9:" + "AA" + conta + ")";
            //x.Range["AB" + colu].Formula = "=SUM(AB9:" + "AB" + conta + ")";
            //x.Range["AC" + colu].Formula = "=SUM(AC9:" + "AC" + conta + ")";
            //x.Range["AD" + colu].Formula = "=SUM(AD9:" + "AD" + conta + ")";
            //x.Range["AE" + colu].Formula = "=SUM(AE9:" + "AE" + conta + ")";
            //x.Range["AF" + colu].Formula = "=SUM(AF9:" + "AF" + conta + ")";
            //x.Range["AG" + colu].Formula = "=SUM(AG9:" + "AG" + conta + ")";
            //x.Range["AH" + colu].Formula = "=SUM(AH9:" + "AH" + conta + ")";
            //x.Range["AI" + colu].Formula = "=SUM(AI9:" + "AI" + conta + ")";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");

                GeneraExcelGeneral();

            }
            else if (radioButton2.Checked)
            {
                if (comboBox1.Text == "Accesorios")
                {
                    DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                    DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                    Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                    Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                    Variablescompartidas.familia = comboBox2.SelectedValue.ToString();
                    Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
                    //Thread hilo1 = new Thread(new ThreadStart(GeneraExcel("spVentaUnidadAccesorios")));
                    //hilo1.Start();
                    GeneraExcel("spVentaUnidadGeneralSucursalesAccesorios");

                }
                else
                {
                    //VariablesCompartidas.accesorios = "0";
                    DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                    DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                    Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                    Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                    Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
                    Variablescompartidas.familia = comboBox2.SelectedValue.ToString();
                    GeneraExcelFamilia();
                }
            }
        }

        private void PantallaCar()
        {
            using (PantallaCar pc = new PantallaCar())
            {
                pc.ShowDialog();
            }
        }


        private void GeneraExcelGeneral()
        {
            Thread hilocarga = new Thread(new ThreadStart(PantallaCar));
            hilocarga.Start();
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Plantilla.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Copia\Reporte" + DateTime.Now.Second.ToString() + ".xlsx";
            //Especifica la ruta
            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand("spVentaUnidadGeneralSucursales", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechini", Convert.ToString(Variablescompartidas.fecini));
            cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(Variablescompartidas.fecfin));
            cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox3.SelectedValue.ToString()));

            //SqlDataReader dr = cmd.ExecuteReader();

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
            x.Range["C4"].Value = dateTimePicker1.Value.ToShortDateString();
            x.Range["E4"].Value = dateTimePicker2.Value.ToShortDateString();
            int co = 9;
            int co2 = 9;

            int devo1 = 9;
            int devo2 = 9;

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    if ((k + 1) >= 19)
                    {
                        x.Cells[j + 9, k + 4] = miTabla.Rows[j].ItemArray[k].ToString();
                    }
                    else
                    {
                        x.Cells[j + 9, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    }

                }
            }
            int conta = miTabla.Rows.Count + 8;
            int colu = miTabla.Rows.Count + 10;
            x.Range["C" + colu].Formula = "=SUM(C9:" + "C" + conta + ")";
            x.Range["D" + colu].Formula = "=SUM(D9:" + "D" + conta + ")";
            x.Range["E" + colu].Formula = "=SUM(E9:" + "E" + conta + ")";
            x.Range["F" + colu].Formula = "=SUM(F9:" + "F" + conta + ")";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
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


        private void GeneraExcelFamilia()
        {
            Thread hilocarga = new Thread(new ThreadStart(PantallaCar));
            hilocarga.Start();
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Plantilla.xlsx";
            string copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ReporteGeneralVentasUnidadesSucursales\Plantilla\Copia\Reporte" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }

            vArchivo.CopyTo(copia);

            sqlConnection1.Open();
            SqlCommand cmd = new SqlCommand("spVentaUnidadGeneralSucursalesFamilia", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechini", Convert.ToString(Variablescompartidas.fecini));
            cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(Variablescompartidas.fecfin));
            cmd.Parameters.AddWithValue("@familia", Convert.ToString(Variablescompartidas.familia));

            cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox3.SelectedValue.ToString()));

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
            x.Range["C4"].Value = dateTimePicker1.Value.ToShortDateString();
            x.Range["E4"].Value = dateTimePicker2.Value.ToShortDateString();
            int co = 9;
            int co2 = 9;

            int devo1 = 9;
            int devo2 = 9;

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    if ((k + 1) >= 19)
                    {
                        x.Cells[j + 9, k + 4] = miTabla.Rows[j].ItemArray[k].ToString();
                    }
                    else
                    {
                        x.Cells[j + 9, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                    }

                }
                //=SUMA(V15,X15,Z15,AB15,AD15,AF15)

               
            }
            int conta = miTabla.Rows.Count + 8;
            int colu = miTabla.Rows.Count + 10;
            x.Range["C" + colu].Formula = "=SUM(C9:" + "C" + conta + ")";
            x.Range["D" + colu].Formula = "=SUM(D9:" + "D" + conta + ")";
            x.Range["E" + colu].Formula = "=SUM(E9:" + "E" + conta + ")";
            x.Range["F" + colu].Formula = "=SUM(F9:" + "F" + conta + ")";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Variablescompartidas.accesorios = "1";

            DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
            DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
            Variablescompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
            Variablescompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
            Variablescompartidas.familia = "";
            Variablescompartidas.sucursal2 = comboBox3.SelectedValue.ToString();
            GeneraExcel("spVentaUnidadGeneralSucursalesFamilia2");
        }
    }
}
