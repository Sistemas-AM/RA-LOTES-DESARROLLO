using MaterialSkin;
using MaterialSkin.Controls;
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

namespace AutorizacionPlanta
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
            button7.BackColor = ColorTranslator.FromHtml("#d93a2c");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"select * from EntregasPendientes 
			order by CFECHA desc, ciddocumento desc
            ", sqlConnection1);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Deseas Autorizar el documento?\nTen en cuenta que una vez autorizado se quitara de la lista", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

                ciddocu = documento;

                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["concepto"].Value.ToString().Contains("TRASPASO"))
                {
                    update2(documento);
                }
                else
                {
                    update(documento);
                }

                recarga();
            }
            else if (result == DialogResult.No)
            {

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
    }
}
