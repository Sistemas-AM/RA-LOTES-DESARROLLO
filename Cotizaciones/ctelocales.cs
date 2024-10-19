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
    public partial class ctelocales : Form
    {
        public ctelocales()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctelocales_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.ctelocal' Puede moverla o quitarla según sea necesario.
            this.ctelocalTableAdapter.Fill(this.reportesAMSADataSet.ctelocal);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VariablesCompartidas.Codigocte = comboBox1.Text;
            this.Close();
        }
    }
}
