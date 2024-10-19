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
    public partial class Condicion : Form
    {
        public Condicion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Condicion_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.Condicion' Puede moverla o quitarla según sea necesario.
                this.condicionTableAdapter.Fill(this.reportesAMSADataSet.Condicion);
            }
            catch (Exception)
            {

                
            }

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
                        condicion.Add("* "+dr.Cells[1].Value.ToString());
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
