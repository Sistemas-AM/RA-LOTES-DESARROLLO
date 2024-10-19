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

namespace Cotizacion2021
{
    public partial class CargarDirecciones : MaterialForm
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();

        public static string idDireccionPasa { get; set; }

        public CargarDirecciones()
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

            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button4.BackColor = ColorTranslator.FromHtml("#D66F6F");
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#CBD0D3");
        }

        private void cargargrid()
        {
            SqlCommand cmd = new SqlCommand(@"select 
            ciddireccion,
            CIDCATALOGO,
            CNOMBRECALLE,
            CCOLONIA,
            CPAIS,
            CESTADO,
            CCIUDAD,
            CMUNICIPIO,
            CNUMEROEXTERIOR,
            CNUMEROINTERIOR
            from direcciones
            where CIDCATALOGO = '"+Form1.idClientePasa+"'", Principal.Variablescompartidas.RepAmsaConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
        }

        private void CargarDirecciones_Load(object sender, EventArgs e)
        {
            cargargrid();
            idDireccionPasa = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                idDireccionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["idDireccion"].Value.ToString();
            }
            catch (NullReferenceException)
            {

                idDireccionPasa = "0";
            }
            this.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idDireccionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["idDireccion"].Value.ToString();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            idDireccionPasa = "0";
            this.Dispose();
        }
    }
}
