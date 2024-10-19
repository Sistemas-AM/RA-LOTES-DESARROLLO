using System;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class Reporte : Form
    {
        public static string fecha1 { get; set; }
        public static string fecha2 { get; set; }
        public static string Ban { get; set; }


        public Reporte()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fecha1 = DateTime.Parse(dateTimePicker1.Text).ToString("MM/dd/yyyy");
            fecha2 = DateTime.Parse(dateTimePicker2.Text).ToString("MM/dd/yyyy");
            Ban = "1";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ban = "0";
            this.Close();
        }
    }
}
