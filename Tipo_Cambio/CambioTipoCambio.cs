using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tipo_Cambio
{
    public partial class CambioTipoCambio : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public CambioTipoCambio()
        {
            InitializeComponent();
            tipoactual();
        }
        private void tipoactual()
        {
            cmd.CommandText = "select tc from tipo_cambio where id_tipo_cambio = '1'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                textBox1.Text= reader["tc"].ToString();

            }
            sqlConnection1.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void actualizarcambio()
        {
            try
            {
                string sql = "Update tipo_cambio set tc = @param1 where id_tipo_cambio = @param2";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", textBox2.Text); //Para grabar algo de un textbox
                cmd.Parameters.AddWithValue("@param2", "1"); //Para grabar una columna


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Tipo de cambio actualizado correctamente");
                textBox2.Clear();
                tipoactual();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al actualizar el tipo de cambio");

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                actualizarcambio();
            }
            else
            {
                MessageBox.Show("Ingresa el tipo de cambio");
                textBox2.Focus();
            }
        }
    }
}
