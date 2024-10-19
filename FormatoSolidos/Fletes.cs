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

namespace FormatoSolidos
{
    public partial class Fletes : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int count = 0;

        public Fletes()
        {
            InitializeComponent();
            //cargar();
        }

        private void cargar()
        {
            //int count = 0; Este se agrega al inicio del programa
            
            cmd.CommandText = "Select flete, fecha from solidos group by flete, fecha";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[count].Cells[0].Value = reader["Flete"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = DateTime.Parse(reader["Fecha"].ToString()).ToShortDateString();
                    

                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }

        private void Fletes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.Solidos' Puede moverla o quitarla según sea necesario.
            this.solidosTableAdapter.Fill(this.reportesAMSADataSet.Solidos);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("Flete like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.codigo = "";
            this.Close();
        }
    }
}
