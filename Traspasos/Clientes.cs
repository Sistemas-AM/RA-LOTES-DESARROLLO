using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'adAMSACONTPAQiDataSet1.admClientes' Puede moverla o quitarla según sea necesario.
            this.admClientesTableAdapter.Fill(this.adAMSACONTPAQiDataSet1.admClientes);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.codigoCliente = "";
            Variablescompartidas.nombreCliente = "";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("CRAZONSOCIAL like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.codigoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Variablescompartidas.nombreCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
