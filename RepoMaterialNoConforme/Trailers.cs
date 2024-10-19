using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepoMaterialNoConforme
{
    public partial class Trailers : Form
    {
        public static string codigo { get; set; }
        public Trailers()
        {
            InitializeComponent();
        }

        private void Trailers_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.NoConforme' Puede moverla o quitarla según sea necesario.
            this.noConformeTableAdapter.Fill(this.reportesAMSADataSet.NoConforme);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("NumTrailer like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["numTrailerDataGridViewTextBoxColumn"].Value.ToString();
            this.Close();
    }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                codigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["numTrailerDataGridViewTextBoxColumn"].Value.ToString();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            codigo = "";
            this.Close();
        }
    }
}
