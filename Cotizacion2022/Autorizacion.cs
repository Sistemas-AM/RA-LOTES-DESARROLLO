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

namespace Cotizacion2022
{
    public partial class Autorizacion : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        int permiteCerrar = 0;
        public static int Validado { get; set; } = 0;

        public static string Anticipo { get; set; } = "";
        public static string Tipo { get; set; } = "";
        public static string Tipo2 { get; set; } = "";
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;


        public Autorizacion()
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
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
            permiteCerrar = 0;
            Validado = 0;
        }

        private void Autorizacion_Load(object sender, EventArgs e)
        {

        }

        private void autorizar(string user, string pass)
        {
            cmd.CommandText = "Select * from Usuarios where Usuario = '" + user + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            string pass2 = "";
            string nombre = "";
            string num = "";
            string user1 = "";
            string sucursal = "";
            string Perfil = "";
            string Perfil_id = "";

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                pass2 = reader["Pass"].ToString();
                nombre = reader["Nombre"].ToString();
                num = reader["Num"].ToString();
                user1 = reader["Usuario"].ToString();
                sucursal = reader["Sucursal"].ToString();
                Perfil = reader["Perfil"].ToString();
                Perfil_id = reader["Perfilid"].ToString();

            }
            sqlConnection1.Close();

            if (Tipo == "Sucursales")
            {
                if (Perfil_id == Form1.GERENTESUCU.ToString())
                {

                    if (pass == pass2)
                    {
                        Validado = 1;
                        permiteCerrar = 1;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Tu perfil no cuenta con permiso para autorizar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (Tipo == "Ventas Especiales")
            {
                if (Tipo2=="Agente")
                {
                    if (Perfil_id == Form1.DIRCOMER.ToString() || Perfil_id == Form1.VENTASESPECIALES.ToString())
                    {

                        if (pass == pass2)
                        {
                            Validado = 1;
                            permiteCerrar = 1;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("La contraseña es incorrecta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tu perfil no cuenta con permiso para autorizar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                else
                {
                    if (Perfil_id == Form1.DIRCOMER.ToString())
                    {

                        if (pass == pass2)
                        {
                            Validado = 1;
                            permiteCerrar = 1;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("La contraseña es incorrecta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tu perfil no cuenta con permiso para autorizar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userText.Text))
            {
                if (!string.IsNullOrEmpty(passText.Text))
                {
                    autorizar(userText.Text, passText.Text);
                }
                else
                {
                    MessageBox.Show("Ingresa una contraseña", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingresa un usuario", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            permiteCerrar = 1;
            Validado = 0;
            this.Close();
        }

        private void Autorizacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar == 0)
            {
                e.Cancel = true;
            }
        }
    }
}
