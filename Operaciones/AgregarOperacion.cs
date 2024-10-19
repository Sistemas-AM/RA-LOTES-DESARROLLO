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
    public partial class AgregarOperacion : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string Ao;
        public static int Guardo;


        
        public AgregarOperacion()
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

        private void AgregarOperacion_Load(object sender, EventArgs e)
        {
            LlenarCategorias();
            LlenarLaborTeams();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DescripcionText.Text) || !string.IsNullOrEmpty(ClaveOPText.Text) || !string.IsNullOrEmpty(SupervisorText.Text))
            {
                ObtenSiguienteCodigo(); 
                Guardar(Ao);
                Guardo = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("LLENAR LOS DATOS", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObtenSiguienteCodigo()
        {
            try
            {
                cmd.CommandText = "select top 1   right('00000' + CAST(Id_Operacion+ 1 as nvarchar) ,5) as siguiente from Catalogo_Operaciones order by Id_Operacion desc";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    Ao = reader["siguiente"].ToString();

                }
                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void Guardar(string Ao)
        {
            try
            {
                string sql = @"insert into Catalogo_Operaciones ([Id_Categoria],[Descripcion],[Codigo],[Supervisor],[Id_Lb]) values(@Id_Categoria, @Descripcion, @Codigo, @SupervisorText, @Id_Lb)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);

                cmd.Parameters.AddWithValue("@Id_Categoria", comboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Descripcion", DescripcionText.Text);
                cmd.Parameters.AddWithValue("@Codigo", ClaveOPText.Text);
                cmd.Parameters.AddWithValue("@SupervisorText", SupervisorText.Text);
                cmd.Parameters.AddWithValue("@Id_Lb", comboBox2.SelectedValue.ToString());

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("GUARDADO CON ÉXITO", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Guardo = 0;
            this.Close();
        }
    }
}
