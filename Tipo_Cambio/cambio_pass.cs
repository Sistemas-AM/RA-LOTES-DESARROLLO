using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tipo_Cambio
{
    public partial class cambio_pass : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int validar = 0;
        public cambio_pass()
        {
            InitializeComponent();
        }
        private void valida ()
        {
            validar = 0;
            cmd.CommandText = "Select pass from users where Usuario = 'TipoCambio'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            String pass = "";
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                pass = reader["pass"].ToString();

            }
            sqlConnection1.Close();


            if (textBox1.Text == pass)
            {
                
                validar =1;
            }
            else
            {
                MessageBox.Show("La contraseña actual es incorrecta");
                validar = 0;
            }
        }

        private void validarAmbas()
        {
            if (textBox2.Text == textBox3.Text)
            {
                
                updatepass();
            }else
            {
                MessageBox.Show("Contraseñas Incorrectas");
                validar = 1;
            }
        }

        private void updatepass()
        {
            try
            {
                string sql = "Update users set Pass = @param1 where Usuario = @param2";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", textBox2.Text); //Para grabar algo de un textbox
                cmd.Parameters.AddWithValue("@param2", "TipoCambio"); //Para grabar una columna


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Contraseña actualizada correctamente");
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al actualizar la contraseña");

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valida();
            if (validar == 1)
            {
                validarAmbas();
            }
            
        }
    }
}
