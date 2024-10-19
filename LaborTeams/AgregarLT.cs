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

namespace LaborTeams
{
    public partial class AgregarLT : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string Lb;
        public static int Guardo;

        public AgregarLT()
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

        private void AgregarLT_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Guardo = 0;
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NombreText.Text) || !string.IsNullOrEmpty(DescripcionText.Text) || !string.IsNullOrEmpty(CodigoText.Text))
            {
                ObtenSiguienteCodigo();
                Guardar(Lb);
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
                cmd.CommandText = "select top 1   right('00000' + CAST(Id_Lb+ 1 as nvarchar) ,5) as siguiente from laborteams order by codigo desc";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    Lb = reader["siguiente"].ToString();

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

        private void Guardar(string Lb)
        {
            try
            {
                string sql = "insert into laborteams values (@Codigo, @Nombre, @Descripcion)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@Codigo", CodigoText.Text);
                cmd.Parameters.AddWithValue("@Nombre", NombreText.Text);
                cmd.Parameters.AddWithValue("@Descripcion", DescripcionText.Text);

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
    }
}
