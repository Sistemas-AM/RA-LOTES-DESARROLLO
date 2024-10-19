using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FormatoEntrega
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string id = "";
        string id2 = "";
        int count = 0;
        int cant = 0;
        string fecha = "";
        public Form1()
        {
            InitializeComponent();
            cargaCombo();
        }

        private void cargaCombo()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucursal from folios", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucursal", typeof(string));

            dt.Load(reader);

            comboBox1.ValueMember = "sucursal";
            comboBox1.DisplayMember = "sucursal";
            comboBox1.DataSource = dt;

            sqlConnection2.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox2.Visible = true;
            textNombre.Clear();
            textRfc.Clear();
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            groupBox2.Visible = false;
            textNombre.Clear();
            textRfc.Clear();
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargardata()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select CCODIGOPRODUCTO, CNOMBREPRODUCTO, CUNIDADESCAPTURADAS from admmovimientos inner join admProductos on admMovimientos.CIDPRODUCTO = admProductos.CIDPRODUCTO where ciddocumento = '"+id2+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView1.Rows[count].Cells[0].Value = reader["CCODIGOPRODUCTO"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["CNOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[count].Cells[2].Value = reader["CUNIDADESCAPTURADAS"].ToString();
                    
                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                dataGridView1.Rows.Clear();
                if (Factura.Checked)
                {
                    if (Credito.Checked)
                    {
                        cmd.CommandText = "select idcredito from folios where sucursal = '"+comboBox1.Text+"'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection2;
                        sqlConnection2.Close();
                        sqlConnection2.Open();
                        //reader = cmd.ExecuteReader();
                        cant = Convert.ToInt32(cmd.ExecuteScalar());

                        if (cant == 0)
                        {
                            MessageBox.Show("No existe");
                        }
                        else
                        {
                            reader = cmd.ExecuteReader();
                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                id = reader["idcredito"].ToString();

                            }
                            sqlConnection2.Close();

                        }
                      


                        //Sacar los datos del cliente
                        cmd.CommandText = "select CIDDOCUMENTO, CRAZONSOCIAL, CRFC, CFECHA from admdocumentos where cidconceptodocumento = '"+id+"' and cfolio = '"+textBox1.Text+"'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Close();
                        sqlConnection1.Open();
                        //reader = cmd.ExecuteReader();

                        // Data is accessible through the DataReader object here.


                        cant = Convert.ToInt32(cmd.ExecuteScalar());

                        if (cant == 0)
                        {
                            MessageBox.Show("No existe");
                        }
                        else if(cant > 0)
                        {
                            reader = cmd.ExecuteReader();
                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                id2 = reader["CIDDOCUMENTO"].ToString();
                                textNombre.Text = reader["CRAZONSOCIAL"].ToString();
                                textRfc.Text = reader["CRFC"].ToString();
                                fecha = reader["CFECHA"].ToString();

                                cargardata();
                            }
                        }
                    
                        sqlConnection1.Close();

                        

                    }
                    else if (Contado.Checked)
                    {
                        cmd.CommandText = "select idcontado from folios where sucursal = '" + comboBox1.Text + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection2;
                        sqlConnection2.Open();
                        reader = cmd.ExecuteReader();

                        // Data is accessible through the DataReader object here.
                        if (reader.Read())
                        {
                            id = reader["idcontado"].ToString();

                        }
                        sqlConnection2.Close();


                        //Sacar los datos del cliente
                        cmd.CommandText = "select CIDDOCUMENTO, CRAZONSOCIAL, CRFC, CFECHA from admdocumentos where cidconceptodocumento = '" + id + "' and cfolio = '" + textBox1.Text + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        cant = Convert.ToInt32(cmd.ExecuteScalar());

                        if (cant == 0)
                        {
                            MessageBox.Show("No existe");
                        }
                        else if (cant > 0)
                        {
                            reader = cmd.ExecuteReader();
                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {
                                id2 = reader["CIDDOCUMENTO"].ToString();
                                textNombre.Text = reader["CRAZONSOCIAL"].ToString();
                                textRfc.Text = reader["CRFC"].ToString();
                                fecha = reader["CFECHA"].ToString();

                                cargardata();
                            }
                        }

                        sqlConnection1.Close();

                    }
                }
                else if (Nota.Checked)
                {
                    cmd.CommandText = "select idnotas from folios where sucursal = '" + comboBox1.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        id = reader["idnotas"].ToString();

                    }
                    sqlConnection2.Close();


                    //Sacar los datos del cliente
                    cmd.CommandText = "select CIDDOCUMENTO, CRAZONSOCIAL, CRFC, CFECHA from admdocumentos where cidconceptodocumento = '" + id + "' and cfolio = '" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cant = Convert.ToInt32(cmd.ExecuteScalar());

                    if (cant == 0)
                    {
                        MessageBox.Show("No existe");
                    }
                    else if (cant > 0)
                    {
                        reader = cmd.ExecuteReader();
                        // Data is accessible through the DataReader object here.
                        if (reader.Read())
                        {
                            id2 = reader["CIDDOCUMENTO"].ToString();
                            textNombre.Text = reader["CRAZONSOCIAL"].ToString();
                            textRfc.Text = reader["CRFC"].ToString();
                            fecha = reader["CFECHA"].ToString();

                            cargardata();
                        }
                    }

                    sqlConnection1.Close();
                }
                }

            }

        private void Credito_CheckedChanged(object sender, EventArgs e)
        {
            textNombre.Clear();
            textRfc.Clear();
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }

        private void Contado_CheckedChanged(object sender, EventArgs e)
        {
            textNombre.Clear();
            textRfc.Clear();
            textBox1.Clear();
            dataGridView1.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.serie = comboBox1.Text;
            Variablescompartidas.no = textBox1.Text;
            Variablescompartidas.nombre = textNombre.Text;
            Variablescompartidas.rfc = textRfc.Text;
            Variablescompartidas.fecha = DateTime.Parse(fecha).ToString("dd/MM/yyyy");
            using (Formato fm = new Formato(dataGridView1))
            {
                fm.ShowDialog();
            }
        }
    }


    }

