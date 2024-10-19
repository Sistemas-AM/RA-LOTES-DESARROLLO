using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReporteVentasporSucursal
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public string idCreditoPasa { get; set; }
        public Form1()
        {
            InitializeComponent();
           

            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {



                try
                {
                    //comboBox1.Enabled = true;

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
                    cmd.CommandText = "select idcredito, idcontado, idnotas from Folios where idalmacen = '" + comboBox1.SelectedValue.ToString() + "'";
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

                comboBox1.Enabled = true;
            }


            else if (Principal.Variablescompartidas.sucural == "PLANTA                   ")
            {

                comboBox1.Enabled = true;



                try
                {
                    //comboBox1.Enabled = true;

                    sqlConnection1.Open();
                    SqlCommand sc = new SqlCommand("select sucnom, idalmacen from Folios where idalmacen in(6,17)", sqlConnection1);
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
                    cmd.CommandText = "select idcredito, idcontado, idnotas from Folios where idalmacen = '" + comboBox1.SelectedValue.ToString() + "'";
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



            else
            {

                try
                {
                    //comboBox1.Enabled = true;

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
                    cmd.CommandText = "select idcredito, idcontado, idnotas from Folios where idalmacen = '" + comboBox1.SelectedValue.ToString() + "'";
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


                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucural;


            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            Variablescompartidas.fecha = fech.ToString(Principal.Variablescompartidas.FormatoFecha);
            Variablescompartidas.fecha2 = dateTimePicker2.Value.ToString(Principal.Variablescompartidas.FormatoFecha);
            Variablescompartidas.credito = idCreditoPasa;
            Variablescompartidas.contado = textBox2.Text;
            Variablescompartidas.notas = textBox3.Text;
            Variablescompartidas.nombresuc = comboBox1.Text;
            using (Reporte rp = new Reporte())
            {
                rp.ShowDialog();
            }
        }
    }
}
