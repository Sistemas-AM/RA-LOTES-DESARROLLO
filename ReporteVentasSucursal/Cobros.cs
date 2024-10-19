using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReporteVentasSucursal
{
    public partial class Cobros : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string cheque1 = "";
        string banco1 = "";
        string clave1 = "";
        string cuenta1 = "";
        int cuentaexiste = 0;
        public Cobros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valida();
            Variablescompartidas.cantidad = cantidad.Text;
            guardar();
            this.Close();
        }

        private void carga()
        {

            //cmd.CommandText = "Selec Algo from Tabla";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //sqlConnection1.Open();
            //reader = cmd.ExecuteReader();

            //// Data is accessible through the DataReader object here.
            //if (reader.Read() == true)
            //{
            //    reader.Close();
            //    reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        dataGridView4.Rows.Add();
            //        //textBox1.Text = reader["ALGO"].ToString();
            //        dataGridView4.Rows[count5].Cells[0].Value = reader["factura"].ToString();
            //        dataGridView4.Rows[count5].Cells[1].Value = reader["proveedor"].ToString();
            //        dataGridView4.Rows[count5].Cells[2].Value = reader["importe"].ToString();
            //        count5++;

            //    }
            //}
            //else
            //{
            //    cuentaexiste += 1;

            //}
            //sqlConnection1.Close();
        }


        private void guardar()
        {
            try
            {
                string sql = "insert into dbfextras (sucursal, fecha, factura, proveedor, cheque, banco, clabe, cuenta, tipo) values (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9)";
                  SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                  cmd.Parameters.AddWithValue("@param1", Variablescompartidas.sucursal); 
                  cmd.Parameters.AddWithValue("@param2", Variablescompartidas.fecha); 
                  cmd.Parameters.AddWithValue("@param3", Variablescompartidas.factura); 
                  cmd.Parameters.AddWithValue("@param4", Variablescompartidas.provedor); 
                  cmd.Parameters.AddWithValue("@param5", cheque1); 
                  cmd.Parameters.AddWithValue("@param6", banco1); 
                  cmd.Parameters.AddWithValue("@param7", clave1); 
                  cmd.Parameters.AddWithValue("@param8", cuenta1); 
                  cmd.Parameters.AddWithValue("@param9", Variablescompartidas.tipo); 

                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection1.Close();
                     //MessageBox.Show("Guardado");
                //}

            }
                catch (Exception)
                {
                MessageBox.Show("Ocurrio un error al guardar");

            }

                
        }


        private void valida()
        {
            if (cheque.Text == "")
            {
                cheque1 = "na";

            }else
            {
                cheque1 = cheque.Text;
            }
            if (banco.Text == "")
            {
                banco1 = "na";

            }else
            {
                banco1 = banco.Text;
            }
            if (clave.Text == "")
            {
                clave1 = "na";
            }else
            {
                clave1 = clave.Text;
            }
            if (cuenta.Text == "")
            {
                cuenta1 = "na";
            } else
            {
                cuenta1 = cuenta.Text;
            }
            if (cantidad.Text == "")
            {
                MessageBox.Show("LLenar campo cantidad");
                cantidad.Focus();
            }
        }

        private void cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }
    }
    }

