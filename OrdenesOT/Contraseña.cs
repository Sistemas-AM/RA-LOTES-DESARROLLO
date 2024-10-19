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
    public partial class Contraseña : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        int permiteCerrar = 0;
        public static int Validado { get; set; } = 0;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Contraseña()
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
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
            permiteCerrar = 0;
            Validado = 0;
        }

        private void Contraseña_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar == 0)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            permiteCerrar = 1;
            Validado = 0;
            this.Close();
        }

        private bool validar()
        {
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection1);
                SqlCommand cmd = new SqlCommand(@"select Pass from usuarios where Num = @Num", sqlConnection1);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Num", Principal.Variablescompartidas.num);
                reader = cmd.ExecuteReader();

                string Pass = "";
                if (reader.Read())
                {
                    Pass = reader["Pass"].ToString();
                }


                if (materialSingleLineTextField1.Text == Pass)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection1);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text.Length != 0)
            {
                if (validar())
                {
                    Validado = 1;
                    permiteCerrar = 1;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La contraseña es Incorrecta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingresa una Contraseña", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
