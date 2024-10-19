using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace EntregasPla
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();
        public static string ciddocu { get; set; }
        public static string Permiso { get; set; }

        public static string BarrasPasa { get; set; }

        public Form1()
        {
            InitializeComponent();
            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

        }

        private void recarga()
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand(@"select * from EntregasPendientes
			order by CFECHA desc, ciddocumento desc", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");

            //         SqlCommand cmd = new SqlCommand(@"with facturas as (
            //         select ciddocumento,
            //         'PLANTA' AS CIDCONCEPTODOCUMENTO,
            //         CSERIEDOCUMENTO,
            //         docu.CFOLIO,
            //         CFECHA,
            //         CRAZONSOCIAL,
            //         docu.CRFC,
            //         CESTADO,
            //         CPESO,
            //         CCANCELADO
            //         from admdocumentos as docu 
            //         inner join admfoliosdigitales on 
            //         docu.CIDDOCUMENTO = ciddocto
            //         where (CIDCONCEPTODOCUMENTO = '3118' or CIDCONCEPTODOCUMENTO = '3124')
            //         and cimpreso = 1 
            //         and CESTADO = 2
            //         and cpeso = 0),

            //         notas as (select ciddocumento,
            //         'PLANTA' AS CIDCONCEPTODOCUMENTO,
            //         CSERIEDOCUMENTO,
            //         docu.CFOLIO,
            //         CFECHA,
            //         CRAZONSOCIAL,
            //         docu.CRFC,
            //         '' as CESTADO,
            //         CPESO,
            //         CCANCELADO
            //         from admdocumentos as docu 
            //         where CIDCONCEPTODOCUMENTO = '3051'
            //         and cimpreso = 1 
            //         and cpeso = 0),

            //         notas2 as (select ciddocumento,
            //         'MERCADOS ESPECIALES' AS CIDCONCEPTODOCUMENTO,
            //         CSERIEDOCUMENTO,
            //         docu.CFOLIO,
            //         CFECHA,
            //         CRAZONSOCIAL,
            //         docu.CRFC,
            //         '' AS CESTADO,
            //         CPESO,
            //         CCANCELADO
            //         from admdocumentos as docu 
            //         where CIDCONCEPTODOCUMENTO = '3213'
            //         and cimpreso = 1 
            //         and cpeso = 0),

            //todo as(

            //         select * from facturas
            //         union
            //         select * from notas
            //         union
            //         select * from notas2)

            
            //select * from todo order by CFECHA desc, ciddocumento desc
            //         ", sqlConnection1);




            SqlCommand cmd = new SqlCommand(@"select * from EntregasPendientes 
			order by CFECHA desc, ciddocumento desc
            ", sqlConnection1);



            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["concepto"].Value.ToString().Contains("TRASPASO"))
            //{
            //    MessageBox.Show("Es traspaso");
            //}else
            //{
            //    MessageBox.Show("No Es traspaso");
            //}

            if (Permiso == "Impresion")
            {
                DialogResult result = MessageBox.Show("¿Deseas Imprimir el documento?\nTen en cuenta que una vez impreso se quitara de la lista", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    string documento = "";
                    string concepto = "";
                    string Folio = "";
                    string Fecha = "";
                    string FechaEntrega = "";
                    documento = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Documento"].Value.ToString();
                    concepto = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Concepto"].Value.ToString();
                    Folio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Folio"].Value.ToString();
                    Fecha = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString()).ToString(Principal.Variablescompartidas.FormatoFecha);
                    FechaEntrega = DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha);


                    // MessageBox.Show(documento);
                    ciddocu = documento;


                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["concepto"].Value.ToString().Contains("TRASPASO"))
                    {
                        guardar(documento, "2");
                        update2(documento);
                    }
                    else
                    {
                        guardar(documento, "1");
                        update(documento);
                    }
                    Barras(documento);

                    //guardar(documento, "1");
                    //update(documento);

                    using (ReporteCrystal rp = new ReporteCrystal())
                    {
                        rp.ShowDialog();
                    }


                    recarga();
                }
                else if (result == DialogResult.No)
                {

                }


            }
            else if (Permiso == "Vista")
            {
                Entregas.idPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Documento"].Value.ToString();
                Desgloce_Entregas.esEntrega = "0";
                using (Desgloce_Entregas de = new Desgloce_Entregas())
                {
                    de.ShowDialog();
                }
            }
        }

      
           

    

        private void update(string documento)
        {
            string sql = @"update admDocumentos set CPESO = 1
            where ciddocumento = @Documeto";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
            cmd.Parameters.AddWithValue("@Documeto", documento); 
           
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }


        private void update2(string documento)
        {
            string sql = @"update traspasos set EntregaPlanta = 1
            where iddocumento = @Documeto";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@Documeto", documento);

            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }


        private void Barras(string convierte)
        {
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            var barcodeImage = barcode.Draw(convierte, 150,3);

            var resultImage = new Bitmap(barcodeImage.Width, barcodeImage.Height + 80);
            //var resultImage = new Bitmap(barcodeImage.Width, barcodeImage.Height + 30); // 20 is bottom padding, adjust to your text

            using (var graphics = Graphics.FromImage(resultImage))
            using (var font = new Font("Arial", 14))
            using (var font2 = new Font("Arial", 13))
            using (var brush = new SolidBrush(Color.Black))
            using (var format = new StringFormat()
            {
                Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
                LineAlignment = StringAlignment.Far
            })
            {
                graphics.Clear(Color.White);
                graphics.DrawImage(barcodeImage, 0, 20);
                //graphics.DrawString(textBox2.Text, font2, brush, resultImage.Width / 2, 13, format);
                //graphics.DrawString(textBox2.Text, font, brush, resultImage.Width / 2, resultImage.Height, format);

            }

            pictureBox1.BackgroundImage = resultImage;

            Image imgFinal = (Image)pictureBox1.BackgroundImage.Clone();
            string path = (Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\EntregasPla\Codigos\");
            imgFinal.Save(path + @"\" + convierte + ".jpg", ImageFormat.Jpeg);
            BarrasPasa = path + @"\" + convierte + ".jpg";
        }

        private void guardar(string idDocu, string tipo)
        {

            SqlCommand cmd = new SqlCommand("InsertaEntregasPlanta", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@idDocu", idDocu);
            cmd.Parameters.AddWithValue("@Tipo", tipo);

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        ////public int Folio()
        ////{
        ////    int folio = 0;
        ////    cmd.CommandText = "select top 1 folio + 1 as siguiete from Entregas_Planta order by folio desc";
        ////    cmd.CommandType = CommandType.Text;
        ////    cmd.Connection = sqlConnection2;
        ////    sqlConnection2.Open();
        ////    reader = cmd.ExecuteReader();

        ////    // Data is accessible through the DataReader object here.
        ////    if (reader.Read())
        ////    {
        ////        folio = Int32.Parse(reader["siguiete"].ToString());

        ////    }
        ////    sqlConnection2.Close();
        ////    return folio;
        ////}

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Entregas en = new Entregas())
            {
                en.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt.Clear();
            recarga();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Barras("3312");
        }
    }
}
