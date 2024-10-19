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

namespace ReporteDescuentos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Enabled = false;

            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                radioButton1.Visible = true;
            }
            else
            {
                radioButton1.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //comboBox1.Enabled = true;
            try
            {
                //comboBox1.Enabled = true;

                sqlConnection1.Open();
                SqlCommand sc = new SqlCommand("select sucnom, idcredito from Folios", sqlConnection1);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sucnom", typeof(string));

                dt.Load(reader);

                comboBox1.ValueMember = "idcredito";

                comboBox1.DisplayMember = "sucnom";



                comboBox1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
            try
            {
                cmd.CommandText = "select idcontado, idnotas from Folios where idcredito = '" + comboBox1.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["idcontado"].ToString();
                    textBox3.Text = reader["idnotas"].ToString();


                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucural;
            }
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedValue.ToString();
            try
            {
                cmd.CommandText = "select idcontado, idnotas from Folios where idcredito = '" + comboBox1.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["idcontado"].ToString();
                    textBox3.Text = reader["idnotas"].ToString();


                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DateTime fech1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fech2 = DateTime.Parse(dateTimePicker2.Text);
                VariablesCompartidas.fechini = fech1.ToString("MM/dd/yyyy");
                VariablesCompartidas.fechfin = fech2.ToString("MM/dd/yyyy");

                using (ReporteGeneral rg = new ReporteGeneral())
                {
                    rg.ShowDialog();
                }
            }
            if (radioButton2.Checked)
            {
                DateTime fech1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fech2 = DateTime.Parse(dateTimePicker2.Text);
                VariablesCompartidas.fechini = fech1.ToString("MM/dd/yyyy");
                VariablesCompartidas.fechfin = fech2.ToString("MM/dd/yyyy");
                VariablesCompartidas.idcredito = textBox1.Text;
                VariablesCompartidas.idcontado = textBox2.Text;
                VariablesCompartidas.inotas = textBox3.Text;
                VariablesCompartidas.sucursal = comboBox1.Text;


                using (RepSucursal rs = new RepSucursal())
                {
                    rs.ShowDialog();
                }

            }
        }
    }
}
