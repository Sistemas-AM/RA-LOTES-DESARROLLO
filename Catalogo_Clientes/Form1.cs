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

namespace Catalogo_Clientes
{
    public partial class Form1 : MaterialForm
    {
        DataTable dt = new DataTable();
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
            LlenaTablaClientes();
        }

        private void LlenaTablaClientes()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT cidclienteproveedor, ccodigocliente, crazonsocial,
                cfechaalta, crfc, CCURP, CDENCOMERCIAL, CEMAIL1, CLISTAPRECIOCLIENTE, CESTATUS,     
                CREGIMFISC FROM dbo.admClientes", Principal.Variablescompartidas.AcerosConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackColor = ColorTranslator.FromHtml("#3FACA9");
        }

        private void BusquedaText_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"ccodigocliente LIKE '%{BusquedaText.Text}%' or crazonsocial LIKE '%{BusquedaText.Text}%'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PantallaCliente.idPasa = "0";
            using (PantallaCliente pc = new PantallaCliente())
            {
                pc.ShowDialog();
            }
            dt.Rows.Clear();
            LlenaTablaClientes();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PantallaCliente.idPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["idCliente"].Value.ToString();
            PantallaCliente.NombrePasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["RazonSocial"].Value.ToString();
            PantallaCliente.RFCPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["RFC"].Value.ToString();
            PantallaCliente.DemonPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Denom"].Value.ToString();
            PantallaCliente.CurpPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["curp"].Value.ToString();
            PantallaCliente.CorreoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["correo"].Value.ToString();
            PantallaCliente.codigoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString();
            PantallaCliente.RegimenPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Regimen"].Value.ToString();
            using (PantallaCliente pc = new PantallaCliente())
            {
                pc.ShowDialog();
            }
            dt.Rows.Clear();
            LlenaTablaClientes();
        }
    }
}
