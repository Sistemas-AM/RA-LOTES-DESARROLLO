using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CargaArchivos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OleDbConnection conn;
            OleDbDataAdapter MyDataAdapter;
            DataTable dt;
            String ruta = "";
            string nombreHoja= "Exportar";
            //try
            //{
                OpenFileDialog openfile1 = new OpenFileDialog();
                //openfile1.Filter = "Excel Files |*.*";

                openfile1.Title = "Seleccione el archivo de Excel";
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openfile1.FileName.Equals("") == false)
                    {
                        ruta = openfile1.FileName;
                    }
                }

                Microsoft.Office.Interop.Excel.Application ExcApp;
                Microsoft.Office.Interop.Excel.Workbook wBook;
                Microsoft.Office.Interop.Excel.Worksheet wSheet;
                ExcApp = new Microsoft.Office.Interop.Excel.Application();
                wBook = ExcApp.Workbooks.Open(ruta); // Ruta del archivo de excel 
                wSheet = wBook.Worksheets["Exportar"]; // El nombre de la hoja que se va a leer 
                DateTime fecha = wSheet.Range["C7"].Value;
                textFecha.Text = fecha.ToString("MM/dd/yyyy");// Extraes la informacion de la celda 

                textCodPro.Text = wSheet.Range["C8"].Value;// Extraes la informacion de la celda 

                string proveedor = wSheet.Range["C10"].Value;
                textProveedor.Text = proveedor.ToString();

                string dom = wSheet.Range["C11"].Value;
                textBox3.Text = dom.ToString();

                string Folio = wSheet.Range["K7"].Value;
                textFolio.Text = Folio.ToString();

                double subtotal = wSheet.Range["F16"].Value;
                textSbT.Text = subtotal.ToString();

                double Imp = wSheet.Range["J16"].Value;
                textImp.Text = Imp.ToString();

                double Total = wSheet.Range["K16"].Value;
                textBox8.Text = Total.ToString();

                double kg = wSheet.Range["M16"].Value;
                textBox9.Text = kg.ToString();

                double dif = wSheet.Range["O16"].Value;
                textBox13.Text = dif.ToString();

                double kf = wSheet.Range["Q16"].Value;
                textBox12.Text = kf.ToString();

                double difkg = wSheet.Range["R16"].Value;
                textBox11.Text = difkg.ToString();

                double x = wSheet.Range["S16"].Value;
                textBox10.Text = x.ToString();
            
                ExcApp.DisplayAlerts = false;
                wBook.Close();
            
                ExcApp.Quit();
                wSheet = null/* TODO Change to default(_) if this is not a reference type */;
                wBook = null/* TODO Change to default(_) if this is not a reference type */;
                ExcApp = null/* TODO Change to default(_) if this is not a reference type */;


                //F3 as Almacen, F4 as PzasRec, F5 as Precio, F6 as Neto, F9 as Impuesto, F11 as Total, F12 as KPT, F13 as KgTeo, F14 as PzasFac, F15 as DifPzas, F16 as KgFac/Pzas,  F17 as KgFac, F18 as DifKg, F19 as ImpTotal
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=No'");
                MyDataAdapter = new OleDbDataAdapter("Select F2 as Productos, F20 as Nombre, F3 as Almacen, F4 as PzasRe, F5 as Precio, F6 as Neto, F9 as Impuesto, F11 as Total, F12 as KPT, F13 as KgTeo, F14 as PzasFac, F15 as DifPzas, F16 as KgFacPzas,  F17 as KgFac, F18 as DifKg, F19 as ImpTotal from [" + nombreHoja + "$A18:T317]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //------------------------------ GRABAR LOS CHEQUES ---------------------------------------------
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Column1"].Value.ToString() != "")
                    {

                        //insert into carcom (Folio, Fechaa, Proveedor, Codigo, Nombre, Almacen, Pzasrec, Precio, Neto, Impuesto, Total, Kpt, Kgt, Kgf, difkg) Values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15)
                        string sql = "insert into carcom (Folio, Fecha, Proveedor, Codigo, Nombre, Almacen, Pzasrec, Precio, Neto, Impuesto, Total, Kpt, Kgt, Kgf, difkg, CodProveedor, PzasFac, DifenPzas, KgTeo, KgFac) Values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                        cmd2.Parameters.AddWithValue("@param1", textFolio.Text); //Folio
                        cmd2.Parameters.AddWithValue("@param2", textFecha.Text); //Fecha
                        cmd2.Parameters.AddWithValue("@param3", textProveedor.Text); // Proveedor
                        cmd2.Parameters.AddWithValue("@param4", row.Cells["Column1"].Value.ToString()); //Codigo
                        cmd2.Parameters.AddWithValue("@param5", row.Cells["Column2"].Value.ToString()); //Nombre
                        cmd2.Parameters.AddWithValue("@param6", row.Cells["Column3"].Value.ToString()); //Almacen
                        cmd2.Parameters.AddWithValue("@param7", row.Cells["Column4"].Value.ToString()); //PzasRec
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["Column5"].Value.ToString()); //Precio
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["Column6"].Value.ToString()); //Neto
                        cmd2.Parameters.AddWithValue("@param10", row.Cells["Column7"].Value.ToString()); //Impuesto
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Column8"].Value.ToString()); //Total
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Column9"].Value.ToString()); //Kpt
                        cmd2.Parameters.AddWithValue("@param13", /*float.Parse(row.Cells["Column10"].Value.ToString())*/33); //Kgt
                        cmd2.Parameters.AddWithValue("@param14", row.Cells["Column13"].Value.ToString()); //kgf
                        cmd2.Parameters.AddWithValue("@param15", /*float.Parse(row.Cells["Column15"].Value.ToString())*/33); //difkg
                        cmd2.Parameters.AddWithValue("@param16", textCodPro.Text); //codPro
                        cmd2.Parameters.AddWithValue("@param17", row.Cells["Column11"].Value.ToString()); //PzasFac
                        cmd2.Parameters.AddWithValue("@param18", row.Cells["Column12"].Value.ToString()); //Dif en Piezas
                        cmd2.Parameters.AddWithValue("@param19", row.Cells["Column10"].Value.ToString()); //KG TEO
                        cmd2.Parameters.AddWithValue("@param20", row.Cells["Column14"].Value.ToString()); //KG FAC
                        
                        //cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        cmd2.ExecuteNonQuery();
                        //label25.Text = (row.Cells["Column1"].Value.ToString());
                        sqlConnection1.Close(); 
                    }
                }
              

            }
            catch (NullReferenceException)
            {

            }
            MessageBox.Show("Guardado");
        }
    }
}
