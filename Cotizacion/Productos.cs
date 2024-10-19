using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cotizacion
{
    public partial class Productos : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        //SqlDataReader reader;
        public Productos()
        {
            InitializeComponent();
            focus();

        }

        private void focus()
        {
            this.textBox1.Focus();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            focus();
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet2.admProductos' Puede moverla o quitarla según sea necesario.
                this.admProductosTableAdapter1.Fill(this.adACEROS_MEXICODataSet2.admProductos);
                // TODO: esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet1.admProductos' Puede moverla o quitarla según sea necesario.
                this.admProductosTableAdapter.Fill(this.adACEROS_MEXICODataSet1.admProductos);

                focus();

            }
            catch (Exception)
            {

                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.CodigoProFormato = "0";
            Variablescompartidas.codigoNoConfo = "0";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (radioButton2.Checked)
                //{
                    var Binding = (BindingSource)dataGridView1.DataSource;
                    Binding.Filter = string.Format("CNOMBREPRODUCTO like '%{0}%' or CCODIGOPRODUCTO like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
                //}
                //else if (radioButton1.Checked)
                //{
                //    var Binding = (BindingSource)dataGridView1.DataSource;
                //    Binding.Filter = string.Format("CCODIGOPRODUCTO like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
                //}
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.CodigoPro = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Variablescompartidas.CodigoProFormato = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Variablescompartidas.nombreFormato = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            Variablescompartidas.codigoNoConfo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Variablescompartidas.NombreNoConfo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            this.Close();

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Variablescompartidas.CodigoPro = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells[0].Value.ToString();
                Variablescompartidas.CodigoProFormato = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells[0].Value.ToString();
                Variablescompartidas.nombreFormato = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells[1].Value.ToString();
                Variablescompartidas.codigoNoConfo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                Variablescompartidas.NombreNoConfo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                this.Close();
            }
        }
    }
}
