using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CortePlanta
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();

            try
            {
                cmd.CommandText = "select idcredito, idcontado from Folios where sucursal = 'PLA'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["idcredito"].ToString();
                    textBox3.Text = reader["idcontado"].ToString();


                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fech = DateTime.Parse(dateTimePicker1.Text);
            DateTime fech2 = DateTime.Parse(dateTimePicker2.Text);
            Variablescompartidas.fecha = fech.ToString("MM/dd/yyyy");
            Variablescompartidas.fecha2 = fech2.ToString("MM/dd/yyyy");
            Variablescompartidas.credito = textBox2.Text;
            Variablescompartidas.contado = textBox3.Text;
            //Variablescompartidas.notas = textBox3.Text;
            Variablescompartidas.nombresuc = label3.Text;

            using (Reporte rp = new Reporte())
            {
                rp.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
