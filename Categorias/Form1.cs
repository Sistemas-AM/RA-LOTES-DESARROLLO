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

namespace Categorias
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();

        public static string Codigo2 { get; set; }
        public static string Nombre2 { get; set; }

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

            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#AFC1BB");
            BtnGuardar.BackColor = ColorTranslator.FromHtml("#76CA62");
            btnEliminar.BackColor = ColorTranslator.FromHtml("#D66F6F");
            btnSalir.BackColor = ColorTranslator.FromHtml("#D66F6F");
            button1.BackColor = ColorTranslator.FromHtml("#6DADA6");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"select * from Catalogo_Categorias order by Id_Categoria", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminar(string Codigo2)
        {
            try
            {
                string sql = "delete from Catalogo_Categorias where Id_Categoria = @Id_Categoria";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Id_Categoria", Codigo2);

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("ELIMINADO CON ÉXITO", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿SEGURO QUE DESEA ELIMINAR LA CATEGORÍA " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descripcion"].Value.ToString().ToUpper() + "?", "ELIMINAR", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                eliminar(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id_Categoria"].Value.ToString());
                dt.Clear();
                SqlCommand cmd = new SqlCommand(@"select * from Catalogo_Categorias order by Id_Categoria", sqlConnection1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlConnection1.Close();
            }
            else if (result == DialogResult.No)
            {
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            using (AgregarCategoria AC = new AgregarCategoria())
            {
                AC.ShowDialog();
            }

            if (AgregarCategoria.Guardo == 1)
            {
                dt.Clear();
                SqlCommand cmd = new SqlCommand(@"select * from Catalogo_Categorias order by Id_Categoria", sqlConnection1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlConnection1.Close();
            }
        }

        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dt.DefaultView.RowFilter = $"Descripcion LIKE '%{Busqueda.Text}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("COLOCAR SOLO CARACTERES VÁLIDOS","AVISO DEL SISTEMA",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Codigo2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id_Categoria"].Value.ToString();
            Nombre2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Descripcion"].Value.ToString();

            using (EditarCategoria EC = new EditarCategoria())
            {
                EC.ShowDialog();
            }
            if (EditarCategoria.Guardo == 1)
            {
                dt.Clear();
                SqlCommand cmd = new SqlCommand(@"select * from Catalogo_Categorias order by Id_Categoria", sqlConnection1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlConnection1.Close();
            }
        }
    }
}
