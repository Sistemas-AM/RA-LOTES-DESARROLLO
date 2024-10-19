using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteDeVentasDiarias
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha1 = DateTime.Parse(dateTimePicker1.Text);
            DateTime fecha2 = DateTime.Parse(dateTimePicker2.Text);

            VariablesCompartidas.fecini = fecha1.ToString("MM/dd/yyyy");
            VariablesCompartidas.fecfin = fecha2.ToString("MM/dd/yyyy");
            using (Form2 rep = new Form2())
            {
                rep.ShowDialog();
            }
        }
    }
}
