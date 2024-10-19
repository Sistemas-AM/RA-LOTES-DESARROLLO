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

namespace Tipo_Cambio
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
        }


        private void ingresar()
        {
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
                using (CambioTipoCambio ct = new CambioTipoCambio())
                {
                    ct.ShowDialog();
                }
            }else
            {
                MessageBox.Show("Contraseña Incorrecta");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ingresar();
        }

        
        
        private void button2_Click(object sender, EventArgs e)
        {
            using (cambio_pass cp = new cambio_pass())
            {
                cp.ShowDialog();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                ingresar();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
