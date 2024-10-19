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
    public partial class Clienteslocales : Form
    {
        public Clienteslocales()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clienteslocales_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet1.ctelocal' Puede moverla o quitarla según sea necesario.
            this.ctelocalTableAdapter.Fill(this.reportesAMSADataSet1.ctelocal);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var Binding = (BindingSource)dataGridView1.DataSource;
                Binding.Filter = string.Format("nombre like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Codigo = comboBox1.Text;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
