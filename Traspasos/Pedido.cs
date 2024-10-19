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
    public partial class Pedido : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int count = 0;

        public Pedido()
        {
            InitializeComponent();
            cargardata();
        }

        private void cargardata()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select folio, cast(fecha as nvarchar) as fecha from dbSurtido where sucent = '"+Variablescompartidas.SucursalPedido+"' and Estatus = '0' group by folio, fecha";
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
                    dataGridView1.Rows[count].Cells[0].Value = reader["Folio"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["Fecha"].ToString();
                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.FolioPedido = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.FolioPedido = "";
            this.Close();
        }
    }
}