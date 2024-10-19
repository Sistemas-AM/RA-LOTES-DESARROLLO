using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cotizacion
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet1.admClientes' Puede moverla o quitarla según sea necesario.
                this.admClientesTableAdapter.Llenar(this.adACEROS_MEXICODataSet1.admClientes);
                // TODO: esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet.admClientes' Puede moverla o quitarla según sea necesario.
                this.admClientesTableAdapter.Llenar(this.adACEROS_MEXICODataSet.admClientes);
            }
            catch (Exception)
            {

               
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Codigo = comboBox1.Text;
            this.Close();
        }
    }
}
