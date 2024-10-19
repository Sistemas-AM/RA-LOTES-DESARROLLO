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

namespace Usuarios
{
    public partial class CargaPerfiles : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        int count = 0;
        DataTable dt = new DataTable();
        public CargaPerfiles()
        {
            InitializeComponent();
            button2.BackColor = ColorTranslator.FromHtml("#E47070");
            button1.BackColor = ColorTranslator.FromHtml("#64C251");
            cargar();
        }

        private void cargar()
        {
            SqlCommand cmd = new SqlCommand(@"Select * from PerfilesUsers", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();
            //count = 0;
            //cmd.CommandText = "Select * from PerfilesUsers";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //sqlConnection1.Open();
            //reader = cmd.ExecuteReader();

            //try
            //{
            //    Data is accessible through the DataReader object here.
            //    while (reader.Read())
            //    {
            //        dataGridView1.Rows.Add();

            //        dataGridView1.Rows[count].Cells[0].Value = reader["Num"].ToString();
            //        dataGridView1.Rows[count].Cells[1].Value = reader["Perfil"].ToString();


            //        count++;

            //    }
            //}
            //catch (Exception)
            //{


            //}
            //sqlConnection1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.numPerfil = "cancelado";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.numPerfil = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"perfil LIKE '%{textBox1.Text}%'";
        }
    }
}
