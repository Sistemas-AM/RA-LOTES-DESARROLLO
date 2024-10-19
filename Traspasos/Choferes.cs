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
    public partial class Choferes : Form
    {
        public Choferes()
        {
            InitializeComponent();
        }

        private void Choferes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'flotillasDataSet.choferes' Puede moverla o quitarla según sea necesario.
            this.choferesTableAdapter.Fill(this.flotillasDataSet.choferes);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("nomcom like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.ChoferNom = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["nomcomDataGridViewTextBoxColumn"].Value.ToString();
            Variablescompartidas.SucursalChofer = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Sucursal"].Value.ToString();
            this.Close();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                Variablescompartidas.ChoferNom = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["nomcomDataGridViewTextBoxColumn"].Value.ToString();
                Variablescompartidas.SucursalChofer = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Sucursal"].Value.ToString();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.ChoferNom = "";
            Variablescompartidas.SucursalChofer = "";
            this.Close();
        }
    }
}
