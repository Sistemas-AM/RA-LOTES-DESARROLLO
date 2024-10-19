using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cotizacion
{
    public partial class ctzaciones : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int count = 0;

        public ctzaciones()
        {
            InitializeComponent();


            //string query = "select folio, fecha, sum(importe) as importe from bdcotizao group by folio, fecha";
            //SqlCommand cmd = new SqlCommand(query, sqlConnection1);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //dataGridView1.DataSource = dt;
           
        }

        private void ctzaciones_Load(object sender, EventArgs e)
        {
            if (Variablescompartidas.Serie == "VE")
            {
                // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet3.bdcotizao' Puede moverla o quitarla según sea necesario.
                this.bdcotizaoTableAdapter.FillBy(this.reportesAMSADataSet3.bdcotizao);

            }
            else if (Variablescompartidas.Serie == "AG")
            {
                // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet3.bdcotizao' Puede moverla o quitarla según sea necesario.
                this.bdcotizaoTableAdapter.FillBy1(this.reportesAMSADataSet3.bdcotizao);
            }
            //// TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet3.bdcotizao' Puede moverla o quitarla según sea necesario.
            //this.bdcotizaoTableAdapter.FillBy(this.reportesAMSADataSet3.bdcotizao);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Foliocot = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() ;
            Variablescompartidas.Fechacot = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()).ToString("yyyy-MM-dd");
            Variablescompartidas.copia = "0";
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Clear();
            cargarcopia();
        }

        private void cargarcopia()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select max(foliocopia) as copias from bdcotizao2 where folioori = '"+ dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["folioDataGridViewTextBoxColumn"].Value.ToString() +"' group by foliocopia";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[count].Cells[0].Value = reader["copias"].ToString();

                    count++;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar la lista de jefes " + e);

            }
            sqlConnection1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Foliocot = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Copias"].Value.ToString();
            //Variablescompartidas.Fechacot = DateTime.Parse(comboBox2.Text).ToString("yyyy-MM-dd");
            Variablescompartidas.copia = "1";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("folio like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }
    }
}