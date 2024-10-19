using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cotizacion2
{
    public partial class ctzaciones : Form
    {
        public ctzaciones()
        {
            InitializeComponent();
        }

        private void ctzaciones_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet2.bdcotizao' Puede moverla o quitarla según sea necesario.
            this.bdcotizaoTableAdapter.Fill(this.reportesAMSADataSet2.bdcotizao);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Foliocot = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Variablescompartidas.Fechacot = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()).ToString("yyyy-MM-dd");
            this.Close();
        }
    }
}
