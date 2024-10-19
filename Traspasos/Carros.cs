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
    public partial class Carros : Form
    {
        public Carros()
        {
            InitializeComponent();
            //var Binding = (BindingSource)dataGridView1.DataSource;
            //Binding.Filter = string.Format("asigna like '%{0}%'", Variablescompartidas.SucursalEnvio.Trim().Replace("'", "''"));
        }

        private void Carros_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'flotillasDataSet1.vehiculos' Puede moverla o quitarla según sea necesario.
            this.vehiculosTableAdapter.Fill(this.flotillasDataSet1.vehiculos);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Carro = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["marcaDataGridViewTextBoxColumn"].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["tipoDataGridViewTextBoxColumn"].Value.ToString();
            Variablescompartidas.Placa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["placasDataGridViewTextBoxColumn"].Value.ToString();
            Variablescompartidas.asigna = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Asigna"].Value.ToString();
            this.Close();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Variablescompartidas.Carro = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["marcaDataGridViewTextBoxColumn"].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["tipoDataGridViewTextBoxColumn"].Value.ToString();
                Variablescompartidas.Placa = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["placasDataGridViewTextBoxColumn"].Value.ToString();
                Variablescompartidas.asigna = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Asigna"].Value.ToString();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Carro = "";
            Variablescompartidas.Placa = "";
            Variablescompartidas.asigna = "";
            this.Close();
        }
    }
}
