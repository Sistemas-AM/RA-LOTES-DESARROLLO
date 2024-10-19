using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DistribucionMaterialesSucursales
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int fol = 0;
        public Form1()
        {
            InitializeComponent();
            ////-------------------------  Sacar el Folio  ------------------------------
            //cmd.CommandText = "select folio from dbSurtido order by folio desc";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection2;
            //sqlConnection2.Open();
            //reader = cmd.ExecuteReader();

            //// Data is accessible through the DataReader object here.
            //if (reader.Read())
            //{
            //    int fol = int.Parse(reader["folio"].ToString()) + 1;
            //    textBox3.Text = fol.ToString();

            //}
            //sqlConnection2.Close();


            //Llena el combobox con las sucursales

            textBox3.Text = "0";
            try
            {

                sqlConnection2.Open();
                SqlCommand sc = new SqlCommand("select sucnom, idalmacen from Folios", sqlConnection2);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("sucnom", typeof(string));

                dt2.Load(reader);

                comboBox2.ValueMember = "idalmacen";

                comboBox2.DisplayMember = "sucnom";

                comboBox2.DataSource = dt2;

                sqlConnection2.Close();
            }
            catch (Exception)
            {


            }
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox2.Text = Principal.Variablescompartidas.sucural;
            }
            

            //Llena el datagrid con los datos 
            //try
            //{
            //    sqlConnection1.Close();
            //    sqlConnection1.Open();
            //    SqlCommand cmd = new SqlCommand("spMatSucursales_cop", sqlConnection1);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd")));
            //    cmd.Parameters.AddWithValue("@v1", Convert.ToString(0));
            //    cmd.Parameters.AddWithValue("@v2", Convert.ToString(0));
            //    cmd.Parameters.AddWithValue("@almacen", Convert.ToString(0));
            //    cmd.Parameters.AddWithValue("@tipo", Convert.ToString(1));
            //    //cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
            //    //SqlDataReader dr = cmd.ExecuteReader();

            //    DataSet ds = new DataSet();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    dataGridView1.DataSource = dt;
            //    sqlConnection1.Close();
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("No se pudieron cargar los datos");
            //}


        }

        private void sacafolio()
        {
            cmd.CommandText = "select folioreq from foliostraspasos";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                fol = int.Parse(reader["folioreq"].ToString()) + 1;
                textBox3.Text = fol.ToString()+"-R";

            }
            sqlConnection2.Close();

        }

        private void updatefolio()
        {
            string sql = "update foliostraspasos set folioreq = @param1";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@param1", fol.ToString());
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void regresa()
        {
            //-------------------------  Sacar el Folio  ------------------------------
            cmd.CommandText = "select folio from dbSurtido order by folio desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["folio"].ToString()) + 1;
                textBox3.Text = fol.ToString();

            }
            sqlConnection2.Close();
            //

            //Llena el combobox con las sucursales
            try
            {

                sqlConnection2.Open();
                SqlCommand sc = new SqlCommand("select sucnom, idalmacen from Folios", sqlConnection2);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("sucnom", typeof(string));

                dt2.Load(reader);

                comboBox2.ValueMember = "idalmacen";

                comboBox2.DisplayMember = "sucnom";

                comboBox2.DataSource = dt2;

                sqlConnection2.Close();
            }
            catch (Exception)
            {


            }

            try
            {
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spMatSucursales_copBIEN", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy")));
                cmd.Parameters.AddWithValue("@v1", Convert.ToString(textBox1.Text));
                cmd.Parameters.AddWithValue("@v2", Convert.ToString(textBox2.Text));
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox2.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@tipo", Convert.ToString(2));
                cmd.Parameters.AddWithValue("@Ejercicio", Principal.Variablescompartidas.Ejercicio);
                //cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                ////SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudieron cargar los datos");
            }
            //Marcar Check todas las casillas del datagrid
            Double totalCantidad = 0;
            Double totalKilos = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                totalCantidad += Convert.ToDouble(row.Cells["Column7"].Value);
                totalKilos += Convert.ToDouble(row.Cells["Column7"].Value) * Convert.ToDouble(row.Cells["Column12"].Value);
                row.Cells[Column11.Name].Value = true;
            }
            textBox4.Text = totalCantidad.ToString();
            textBox5.Text = totalKilos.ToString();
            button3.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                sqlConnection1.Close();
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spMatSucursales_copBIEN", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy")));
                cmd.Parameters.AddWithValue("@v1", Convert.ToString(textBox1.Text));
                cmd.Parameters.AddWithValue("@v2", Convert.ToString(textBox2.Text));
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox2.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@tipo", Convert.ToString(2));
                cmd.Parameters.AddWithValue("@Ejercicio", Principal.Variablescompartidas.Ejercicio);
                //cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                ////SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {
                sqlConnection1.Close();
                MessageBox.Show("No se pudieron cargar los datos");
            }
            //Marcar Check todas las casillas del datagrid
            Double totalCantidad = 0;
            Double totalKilos = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                totalCantidad += Convert.ToDouble(row.Cells["Column7"].Value);
                totalKilos += Convert.ToDouble(row.Cells["Column7"].Value) * Convert.ToDouble(row.Cells["Column12"].Value);
                row.Cells[Column11.Name].Value = true;
            }
            textBox4.Text = totalCantidad.ToString();
            textBox5.Text = totalKilos.ToString();
            button3.Enabled = true;


        }

        private void suma()
        {
            try
            {
                //Marcar Check todas las casillas del datagrid
                Double totalCantidad = 0;
                Double totalKilos = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Column11"].Value.ToString() != "0")
                    {
                        totalCantidad += Convert.ToDouble(row.Cells["Column7"].Value);
                        totalKilos += Convert.ToDouble(row.Cells["Column7"].Value) * Convert.ToDouble(row.Cells["Column12"].Value);
                    }

                }
                textBox4.Text = totalCantidad.ToString();
                textBox5.Text = totalKilos.ToString();
                //button3.Enabled = true; 
            }
            catch (Exception)
            {
                
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            sacafolio();
            updatefolio();
            guardar();
            Variablescompartidas.sucursal = comboBox2.Text;
            Variablescompartidas.v1 = textBox1.Text;
            Variablescompartidas.v2 = textBox2.Text;
            Variablescompartidas.folio = textBox3.Text;
            using (Rep1 rs = new Rep1(dataGridView1))
            {
                rs.ShowDialog();
            }

           // regresa();
        }

        private void guardar()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["column11"].Value.ToString() != "0")
                    {
                        string sql = "insert into dbSurtido (folio, fecha, codigo, nombre, sucent, porde,porhas, exiced, kilos, maxgen, exigen, surgen, cap, Estatus) values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13, @Estatus)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection2);
                        cmd2.Parameters.AddWithValue("@param1", textBox3.Text); //folio
                        cmd2.Parameters.AddWithValue("@param2", (DateTime.Now.ToString("MM/dd/yyyy"))); //fecha
                        cmd2.Parameters.AddWithValue("@param3", row.Cells["Column1"].Value.ToString()); //Codigo
                        cmd2.Parameters.AddWithValue("@param4", row.Cells["Column2"].Value.ToString()); //Nombre
                        cmd2.Parameters.AddWithValue("@param5", comboBox2.Text); //sucent sucursal entrada
                        cmd2.Parameters.AddWithValue("@param6", textBox1.Text); //porde 
                        cmd2.Parameters.AddWithValue("@param7", textBox2.Text); //porhas
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["Column8"].Value.ToString()); //exiced existencia cedis
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["Column12"].Value.ToString()); //kilos
                        cmd2.Parameters.AddWithValue("@param10", "0"); //maxgen maximo venta
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Column6"].Value.ToString()); //exigen existencia sucursal
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Column7"].Value.ToString()); //surgen cantidad
                        cmd2.Parameters.AddWithValue("@param13", row.Cells["Column10"].Value.ToString()); //cap desabasto
                        cmd2.Parameters.AddWithValue("@Estatus", "0"); //cap desabasto

                        //cmd.Connection = sqlConnection1;
                        sqlConnection2.Open();
                        cmd2.ExecuteNonQuery();
                        //label25.Text = (row.Cells["Column1"].Value.ToString());
                        sqlConnection2.Close();

                    }
                }
                MessageBox.Show("Guardado");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al guardar");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.sucursal = comboBox2.SelectedValue.ToString();
            Variablescompartidas.name = comboBox2.Text;
            using (sinexistencia se = new sinexistencia())
            {
                se.ShowDialog();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.sucursal = comboBox2.SelectedValue.ToString();
            Variablescompartidas.name = comboBox2.Text;
            using (exisinsuficiente ex = new exisinsuficiente())
            {
                ex.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //suma();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            suma();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection1.Close();
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("spMatSucursales_copBIEN", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy")));
                cmd.Parameters.AddWithValue("@v1", Convert.ToString(textBox1.Text));
                cmd.Parameters.AddWithValue("@v2", Convert.ToString(textBox2.Text));
                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(comboBox2.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@tipo", Convert.ToString(2));
                cmd.Parameters.AddWithValue("@Ejercicio", Principal.Variablescompartidas.Ejercicio);
                //cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                ////SqlDataReader dr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (Exception)
            {
                sqlConnection1.Close();
                MessageBox.Show("No se pudieron cargar los datos");
            }
            //Marcar Check todas las casillas del datagrid
            Double totalCantidad = 0;
            Double totalKilos = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                totalCantidad += Convert.ToDouble(row.Cells["Column7"].Value);
                totalKilos += Convert.ToDouble(row.Cells["Column7"].Value) * Convert.ToDouble(row.Cells["Column12"].Value);
                row.Cells[Column11.Name].Value = true;
            }
            textBox4.Text = totalCantidad.ToString();
            textBox5.Text = totalKilos.ToString();
            button3.Enabled = true;
        }
    }
}