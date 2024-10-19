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

namespace RepoLore
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public static string idCreditoPasa { get; set; }

        public Form1()
        {
            InitializeComponent();
            try
            {
                comboBox1.Enabled = true;

                sqlConnection1.Open();
                SqlCommand sc = new SqlCommand("select sucnom, idalmacen from Folios", sqlConnection1);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sucnom", typeof(string));

                dt.Load(reader);

                comboBox1.ValueMember = "idalmacen";

                comboBox1.DisplayMember = "sucnom";



                comboBox1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }

            try
            {
                cmd.CommandText = "select idcontado, idnotas, idcredito from Folios where idalmacen = '" + comboBox1.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["idcontado"].ToString();
                    textBox3.Text = reader["idnotas"].ToString();
                    idCreditoPasa = reader["idcredito"].ToString();


                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
               try
               {
                   cmd.CommandText = "select idcontado, idnotas, idcredito from Folios where idalmacen = '" + comboBox1.SelectedValue.ToString() + "'";
                   cmd.CommandType = CommandType.Text;
                   cmd.Connection = sqlConnection1;
                   sqlConnection1.Open();
                   reader = cmd.ExecuteReader();
                   // Data is accessible through the DataReader object here.
                   if (reader.Read())
                   {
                       textBox2.Text = reader["idcontado"].ToString();
                       textBox3.Text = reader["idnotas"].ToString();
                       idCreditoPasa = reader["idcredito"].ToString();

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
            Variablescompartidas.fecha = fech.ToString("MM/dd/yyyy");
            Variablescompartidas.credito = idCreditoPasa;
            Variablescompartidas.contado = textBox2.Text;
            //Variablescompartidas.notas = textBox3.Text;
            Variablescompartidas.nombresuc = comboBox1.Text;
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