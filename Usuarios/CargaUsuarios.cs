using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Usuarios
{
    public partial class CargaUsuarios : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        int count = 0;
        public CargaUsuarios()
        {
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
                    );
            InitializeComponent();
            button2.BackColor = ColorTranslator.FromHtml("#E47070");
            button1.BackColor = ColorTranslator.FromHtml("#64C251");
            cargar();
        }

        private void cargar()
        {

            SqlCommand cmd = new SqlCommand(@"Select Nombre, Usuario, Num, sucursal, perfil from Usuarios where usuario != 'Master'", sqlConnection1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection1.Close();

            //count = 0;
            //cmd.CommandText = "Select Nombre, Usuario, Num from Usuarios where usuario != 'Master'";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //sqlConnection1.Open();
            //reader = cmd.ExecuteReader();

            //try
            //{
            //    // Data is accessible through the DataReader object here.
            //    while (reader.Read())
            //    {
            //        dataGridView1.Rows.Add();

            //        dataGridView1.Rows[count].Cells[0].Value = reader["Nombre"].ToString();
            //        dataGridView1.Rows[count].Cells[1].Value = reader["Usuario"].ToString();
            //        dataGridView1.Rows[count].Cells[2].Value = reader["Num"].ToString();


            //        count++;

            //    }
            //}
            //catch (Exception)
            //{


            //}
            //sqlConnection1.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.num = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.num = "cancelado";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"nombre LIKE '%{textBox1.Text}%' or usuario LIKE '%{textBox1.Text}%'";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
