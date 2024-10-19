using System;
using System.Windows.Forms;

namespace Cotizacion2
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'adAMSACONTPAQiDataSet.admClientes' Puede moverla o quitarla según sea necesario.
            this.admClientesTableAdapter.Fill(this.adAMSACONTPAQiDataSet.admClientes);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var Binding = (BindingSource)dataGridView1.DataSource;
                Binding.Filter = string.Format("CRAZONSOCIAL like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }
    }
}
