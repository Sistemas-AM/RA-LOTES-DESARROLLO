using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
//Agregamos para ejecutar el elnace
using System.Diagnostics;
namespace LoginAmsa
{
    public partial class Login : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        public Login()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
        Primary.Grey800, Primary.Grey900,
        Primary.Grey500, Accent.LightGreen700,
        TextShade.WHITE
            );
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void ingresar()
        {
            String user = textBox1.Text;
            String pass = textBox2.Text;
            if (user == "" || pass == "")
            {
                MessageBox.Show("Ingrese un usuario y contraseña");
                textBox1.Focus();
            }
            else
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


                if (textBox2.Text == pass2)
                {

                    if (pass2 == "1Aceros")
                    {
                        MenuPrincipal.Variablescompartidas.num = num;
                        MenuPrincipal.Variablescompartidas.nombre = nombre;
                        MenuPrincipal.Variablescompartidas.usuario = user1;
                        MenuPrincipal.Variablescompartidas.sucursal = sucursal;

                        Form childForm2 = new CambioPass.Form1();
                        childForm2.Show();
                        textBox2.Clear();
                    }
                    else
                    {
                        //MessageBox.Show("Login Correcto");
                        Ejercicio();

                        MenuPrincipal.Variablescompartidas.num = num;
                        MenuPrincipal.Variablescompartidas.nombre = nombre;
                        MenuPrincipal.Variablescompartidas.usuario = user1;
                        MenuPrincipal.Variablescompartidas.sucursal = sucursal;

                        Principal.Variablescompartidas.usuario = user1;
                        Principal.Variablescompartidas.nombre = nombre;
                        Principal.Variablescompartidas.Pass = pass2;
                        Principal.Variablescompartidas.Perfil = Perfil;
                        Principal.Variablescompartidas.Perfil_id = Perfil_id;
                        Principal.Variablescompartidas.num = num;
                        //MenuPrincipal.Variables

                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();

                        this.Hide();
                        Form childForm = new MenuPrincipal.MenuAmsa();
                        childForm.Show();
                        //this.Close();
                        //using (MenuPrincipal.MenuAmsa ct = new MenuPrincipal.MenuAmsa())
                        //{
                        //    ct.ShowDialog();
                        //}
                    }

                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ingresar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Close();
            string url = "https://www.youtube.com/watch?v=EveiZTEHDe0";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
            MessageBox.Show("Si se olvido su contraseña, ponganse en contacto con departamento de sistemas");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                ingresar();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                ingresar();
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            ingresar();
        }
        public static void Ejercicio()
        {
            try
            {
                using (SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros))
                {
                    sqlConnection2.Open();
                    string EjSql = "SELECT cidejercicio FROM admEjercicios WHERE CEJERCICIO = YEAR(GETDATE())";
                    using (SqlCommand command = new SqlCommand(EjSql, sqlConnection2))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Principal.Variablescompartidas.Ejercicio = reader.GetInt32(0).ToString();
                                // Imprimir el valor en la consola para verificar
                                //MessageBox.Show("El Periodo Selecionado Es: " + Principal.Variablescompartidas.Ejercicio);
                            }
                            else
                            {
                                //MessageBox.Show("No se encontraron registros.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ingresar();
        }
    }
}