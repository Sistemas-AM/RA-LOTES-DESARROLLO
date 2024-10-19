using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Corte.DBHelper;

namespace Corte
{
    public partial class RetirosForm : Form
    {
        private string sucursal;
        private float total;
        private DateTime fecha;
        public string nombre { set;  get; }
        public RetirosForm(string sucursal, float total, DateTime fecha)
        {
            InitializeComponent();
            textBox3.Text = total.ToString();
            textBox6.Text = fecha.ToString("dd/MM/yyyy");
            textBox1.Text = (getFolioRetiro(sucursal)+1).ToString();
            textBox2.Text = fecha.ToString("HH:mm:ss");
            this.total = total;
            this.fecha = fecha;
            this.sucursal = sucursal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                actualizaConsecutivoFolio(sucursal);
                guardaRetiro(DateTime.Parse(textBox6.Text), sucursal, DateTime.Now.ToString("HH:mm:ss"), getFolioRetiro(sucursal), total, textBox4.Text, textBox5.Text);
                nombre = textBox4.Text;
                this.Close();
            }else
            {
                MessageBox.Show("Asegurate de capturar todos los datos");
            }
        }
    }
}