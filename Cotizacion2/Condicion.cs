using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cotizacion2
{
    public partial class Condicion : Form
    {
      
        public Condicion()
        {
            InitializeComponent();
        }

        private void Condicion_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet1.Condicion2' Puede moverla o quitarla según sea necesario.
            this.condicion2TableAdapter.Fill(this.reportesAMSADataSet1.Condicion2);

        }

        public List<string> condicion = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[0] != null)
                {
                    if (Convert.ToInt32(dr.Cells["Column1"].Value) == 1)
                    {
                        // Checked
                        condicion.Add("* " + dr.Cells[1].Value.ToString());
                    }
                    else if (dr.Cells["Column1"].Value == null)
                    {
                        // Unchecked
                    }
                }

            }
            this.Close();
        }
    }
}
