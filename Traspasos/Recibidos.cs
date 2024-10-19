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

namespace Traspasos
{
    public partial class Recibidos : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int count = 0;
        string query = "";
        public Recibidos()
        {
            InitializeComponent();
            
            if (Variablescompartidas.Recibir == "1")
            {
                query = "Select folio, fecha, Destinoreal from traspasos where estatus != 'R' group by folio, Fecha, Destinoreal order by fecha desc";
                Variablescompartidas.Recibir = "0";
            }else {
                query = "select folio, fecha, Destinoreal from traspasos where Destinoreal = '" + Variablescompartidas.SucursalRecibir + "' and Estatus != 'R' group by folio, Fecha, DestinoReal order by fecha desc";
            }
            llenadata();

        }

        private void llenadata()
        {

            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = query;
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
                    // textBox1.Text = reader["ALGO"].ToString();
                    dataGridView1.Rows[count].Cells[0].Value = reader["Folio"].ToString();
                    DateTime dt = DateTime.Parse(reader["Fecha"].ToString());
                    dataGridView1.Rows[count].Cells[1].Value = dt.ToShortDateString();
                    dataGridView1.Rows[count].Cells[2].Value = reader["Destinoreal"].ToString();


                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Folio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }
    }
}