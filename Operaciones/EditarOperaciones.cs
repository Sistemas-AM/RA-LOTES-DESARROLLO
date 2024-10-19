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

namespace Operaciones
{
    public partial class EditarOperaciones : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string LT;
        public static int Guardo;

        public EditarOperaciones()
        {
            InitializeComponent();

            IdOpereacion.Text = Form1.IdOperacion;
            comboBox1.ValueMember = Form1.Categoria2;
            DescripcionText.Text = Form1.Descripcion2;
            ClaveOPText.Text = Form1.ClaveOportunidad;
            SupervisorText.Text = Form1.Supervisor2;
            comboBox1.Text = Form1.IdCategoria;
            comboBox2.Text = Form1.LaborTeams;

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
            BtnGuardar.BackColor = ColorTranslator.FromHtml("#76CA62");
            btnSalir.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void LlenarCategorias()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("Select Id_Categoria, Descripcion from Catalogo_Categorias", sqlConnection1);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "Id_Categoria";
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.DataSource = dt;

            sqlConnection1.Close();
        }
        private void LlenarLaborTeams()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("Select Id_Lb, Nombre from laborteams", sqlConnection1);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            comboBox2.ValueMember = "Id_Lb";
            comboBox2.DisplayMember = "Nombre";
            comboBox2.DataSource = dt;

            sqlConnection1.Close();
        }

        private void EditarOperaciones_Load(object sender, EventArgs e)
        {
            LlenarCategorias();
            LlenarLaborTeams();
            comboBox1.SelectedValue = Form1.IdCategoria;
            comboBox2.SelectedValue = Form1.LaborTeams;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Guardo = 0;
            this.Close();
        }

        private void Guardar(string LT)
        {
            try
            {
                string sql = "update Catalogo_Operaciones set Codigo = @Codigo, Descripcion = @Descripcion, Supervisor = @Supervisor, Id_Categoria = @Id_Categoria, Id_Lb = @Id_Lb  where Id_Operacion = @Id_Operacion";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Id_Operacion", IdOpereacion.Text);
                cmd.Parameters.AddWithValue("@Descripcion", DescripcionText.Text);
                cmd.Parameters.AddWithValue("@Codigo", ClaveOPText.Text);
                cmd.Parameters.AddWithValue("@Supervisor", SupervisorText.Text);
                cmd.Parameters.AddWithValue("@Id_Categoria", comboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Id_Lb", comboBox2.SelectedValue.ToString());

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("ACTUALIZADO CON ÉXITO", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SupervisorText.Text) || !string.IsNullOrEmpty(DescripcionText.Text) || !string.IsNullOrEmpty(ClaveOPText.Text))
            {
                Guardar(IdOpereacion.Text);
                Guardo = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("LLENAR LOS DATOS", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
