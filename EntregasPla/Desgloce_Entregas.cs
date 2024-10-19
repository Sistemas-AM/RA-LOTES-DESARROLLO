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
using MaterialSkin;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace EntregasPla
{
    public partial class Desgloce_Entregas : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        public static string esEntrega { get; set; }

        public Desgloce_Entregas()
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

        private void Desgloce_Entregas_Load(object sender, EventArgs e)
        {
            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");

            if (esEntrega == "1")
            {
                SqlCommand cmd = new SqlCommand(@"select CCODIGOPRODUCTO, CNOMBREPRODUCTO,
                CUNIDADES, ROUND((CUNIDADES * PESO),2) as PESO_TOTAL
                from Entregas_Planta
                where idDocumento ='" + Entregas.idPasa + "'", sqlConnection2);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlConnection2.Close();
            }
            else if (esEntrega == "0")
            {
                SqlCommand cmd = new SqlCommand(@"SELECT 
                PRODU.CCODIGOPRODUCTO,
                PRODU.CNOMBREPRODUCTO,
                MOV.CUNIDADES,
                ROUND((MOV.CUNIDADES * PRODU.CPRECIO10),2) AS PESO_TOTAL
                FROM admDocumentos AS DOCU
                INNER JOIN ADMCLIENTES AS CLI ON DOCU.CIDCLIENTEPROVEEDOR 
                = CLI.CIDCLIENTEPROVEEDOR
                INNER JOIN admMovimientos AS MOV 
                ON DOCU.CIDDOCUMENTO = MOV.CIDDOCUMENTO
                INNER JOIN admProductos AS PRODU
                ON PRODU.CIDPRODUCTO = MOV.CIDPRODUCTO
                INNER JOIN admAlmacenes AS ALM
                ON MOV.CIDALMACEN = ALM.CIDALMACEN
                WHERE DOCU.CIDDOCUMENTO ='" + Entregas.idPasa + "'", sqlConnection1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlConnection1.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Pass ps = new Pass())
            {
                ps.ShowDialog();
            }

            if (Pass.PassPasa != "Cancelado")
            {
                if (valida(Pass.PassPasa))
                {
                    Form1.ciddocu = Entregas.idPasa;
                    Imprimir();
                    Barras(Form1.ciddocu);

                    using (ReporteCrystal rp = new ReporteCrystal())
                    {
                        rp.ShowDialog();
                    }
                }else
                {
                    MessageBox.Show("LA CONTRASEÑA ES INCORRECTA O NO TIENES PERMISO PARA REIMPRIMIR", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Imprimir()
        {
            SqlCommand cmd = new SqlCommand(@"update Entregas_Planta set impresion = (impresion + 1)  where idDocumento = '" + Entregas.idPasa + "'", sqlConnection2);
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();

            sqlConnection2.Close();
        }


        private void Barras(string convierte)
        {
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            var barcodeImage = barcode.Draw(convierte, 150, 3);

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
            Form1.BarrasPasa = path + @"\" + convierte + ".jpg";
        }

        public bool valida(string Pass)
        {
            if (Principal.Variablescompartidas.Perfil_id == "36")
            {
                if (Principal.Variablescompartidas.Pass == Pass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}
