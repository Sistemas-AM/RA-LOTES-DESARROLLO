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

namespace OrdenesOT
{
    public partial class PantallaClientes : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        public static string codigo { get; set; }
        public static string IdPasa { get; set; }
        public static string Nombre { get; set; }
        public static string esLocal { get; set; }

        public static string SegmentoPasa { get; set; }

        public PantallaClientes()
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

        private void PantallaClientes_Load(object sender, EventArgs e)
        {
            codigo = "";
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
            cargargrid();
            Busqueda.Select();
            Busqueda.Select();
        }
        private void cargargrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT CIDCLIENTEPROVEEDOR, CCODIGOCLIENTE, CRAZONSOCIAL, ctextoextra1 FROM dbo.admClientes where cestatus = 1", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void cargargridLOC()
        {
            SqlCommand cmd = new SqlCommand("SELECT CodigoCliente as CCODIGOCLIENTE, Nombre as CRAZONSOCIAL FROM ctelocal", sqlConnection2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (materialCheckBox1.Checked)
            {
                esLocal = "1";
            }else
            {
                esLocal = "0";
            }
            IdPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value.ToString();
            codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
            Nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["NombreCli"].Value.ToString();
            SegmentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ctexto"].Value.ToString();
            this.Close();
        }

        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"CCODIGOCLIENTE LIKE '%{Busqueda.Text}%' or CRAZONSOCIAL LIKE '%{Busqueda.Text}%'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            codigo = null;
            Nombre = null;
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (materialCheckBox1.Checked)
            {
                esLocal = "1";
            }
            else
            {
                esLocal = "0";
            }
            IdPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value.ToString();
            codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codigo"].Value.ToString();
            Nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["NombreCli"].Value.ToString();
            SegmentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ctexto"].Value.ToString();
            this.Close();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (materialCheckBox1.Checked)
                {
                    esLocal = "1";
                }
                else
                {
                    esLocal = "0";
                }
                IdPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Id"].Value.ToString();
                codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["codigo"].Value.ToString();
                Nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["NombreCli"].Value.ToString();
                SegmentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["ctexto"].Value.ToString();
                this.Close();
            }
        }

        private void Busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Select();
            }
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckBox1.Checked)
            {
                dt.Clear();
                cargargridLOC();
            }
            else
            {
                dt.Clear();
                cargargrid();
            }
        }
    }
}
