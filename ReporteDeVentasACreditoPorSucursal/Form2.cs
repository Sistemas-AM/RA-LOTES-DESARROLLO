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

namespace ReporteDeVentasACreditoPorSucursal
{
    public partial class Form2 : Form
    {
        public bool esEfectivo = false;
        public DateTime fecha;
        public string sucursal;
        public Form2(bool esEfectivo, DateTime fecha, string sucursal)
        {
            InitializeComponent();
            this.fecha = fecha;
            this.sucursal = sucursal;
            if (esEfectivo)
            {
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                esEfectivo = true;
            }
        }

      

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text =  DatosBanco.dbFactura;
            label2.Text =  DatosBanco.dbProveedor;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatosBanco.dbImporte = 0;
            try
            {
                DatosBanco.dbImporte = float.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Favor de ingresar numeros solamente");

            }
            finally
            {
                textBox1.Text = "";
            }

            if (!esEfectivo)
            {

                SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader reader2;
                DatosBanco.dbBanco = "na";
                DatosBanco.dbClabe = "na";
                DatosBanco.dbCuenta = "na";
                DatosBanco.dbNoCheque = "na";
                if (!textBox2.Text.Equals(""))
                {
                    DatosBanco.dbNoCheque = textBox2.Text;
                }
                if (!textBox3.Text.Equals(""))
                {
                    DatosBanco.dbBanco = textBox3.Text;
                }
                if (!textBox4.Text.Equals(""))
                {
                    DatosBanco.dbClabe = textBox4.Text;
                }
                if (!textBox5.Text.Equals(""))
                {
                    DatosBanco.dbCuenta = textBox5.Text;
                }

                cmd2.CommandText = "DELETE FROM dbfextras " +
                    "WHERE fecha = '" + fecha.ToString("yyyy-dd-MM 00:00:00.000") + "' AND factura = '"+DatosBanco.dbFactura+"'";
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = sqlConnection3;

                sqlConnection3.Open();

                reader2 = cmd2.ExecuteReader();

                sqlConnection3.Close();
                
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "INSERT INTO dbfextras(sucursal, fecha, factura, proveedor, cheque, banco, clabe, cuenta) VALUES " +
                            "('" +sucursal+ "','" + fecha.ToString("yyyy-dd-MM 00:00:00.000") + "'," +DatosBanco.dbFactura + ",'" + DatosBanco.dbProveedor + "','" + DatosBanco.dbClabe + "','" + DatosBanco.dbBanco + "','" + DatosBanco.dbClabe + "','" + DatosBanco.dbCuenta + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection3;

                        sqlConnection3.Open();

                try
                {
                    reader = cmd.ExecuteReader();
                } catch (System.Data.SqlClient.SqlException)
                {

                }
                        

                        sqlConnection3.Close();
                  
            }
            this.Close();
        }
    }
}
