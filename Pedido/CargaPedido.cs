using System;
using System.Windows.Forms;

namespace Pedido
{
    public partial class CargaPedido : Form
    {
        public CargaPedido()
        {
            InitializeComponent();
        }

        private void CargaPedido_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.bdpbno' Puede moverla o quitarla según sea necesario.
            this.bdpbnoTableAdapter.Fill(this.reportesAMSADataSet.bdpbno);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Folio = comboBox1.Text;
            this.Close();
        }
    }
}