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

namespace ReporteDevoluciones
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               // comboBox1.Enabled = true;

                sqlConnection1.Open();
                SqlCommand sc = new SqlCommand("select sucnom, iddevo, iddevocfd from Folios", sqlConnection1);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sucnom", typeof(string));

                dt.Load(reader);

                comboBox1.ValueMember = "iddevo";

                comboBox1.DisplayMember = "sucnom";

                comboBox1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {

                
            }
            try
            {
                cmd.CommandText = "select iddevocfd from Folios where iddevo = '" + comboBox1.SelectedValue.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["iddevocfd"].ToString();


                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {

              
            }
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox1.Enabled = true;
                radioButton1.Visible = true;
            }else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucural;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                VariablesCompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                VariablesCompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                using (general gn = new general())
                {
                    gn.ShowDialog();
                }
            }
            else if (radioButton2.Checked)
            {
                DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
                DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);
                VariablesCompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
                VariablesCompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
                VariablesCompartidas.sucursal = textBox1.Text;
                VariablesCompartidas.sucfac = textBox2.Text;

                using (Sucursal sc = new Sucursal())
                {
                    sc.ShowDialog();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedValue.ToString();
            try
            {
                cmd.CommandText = "select iddevocfd from Folios where iddevo = '"+comboBox1.SelectedValue.ToString()+"'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    textBox2.Text = reader["iddevocfd"].ToString();
                }
                sqlConnection1.Close();
            }
            catch (Exception)
            {

                
            }

        }
    }
}
