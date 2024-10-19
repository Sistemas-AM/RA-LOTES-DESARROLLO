using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cotizaciones
{
    public partial class Clientesadm : Form
    {
        public Clientesadm()
        {
            InitializeComponent();
        }

        private void Clientesadm_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'adAMSACONTPAQiDataSet.admClientes' Puede moverla o quitarla según sea necesario.
            this.admClientesTableAdapter.Fill(this.adAMSACONTPAQiDataSet.admClientes);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Buscar por textBox
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("CRAZONSOCIAL like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VariablesCompartidas.Codigocte = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VariablesCompartidas.Codigocte = "";
            this.Close();
        }
    }
}
