using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corte
{
    public partial class Form4 : Form
    {
        private DataGridView dataGridView3;
        private DataGridView dataGridView4;
        private DataGridView dataGridView5;
        private DataGridView dataGridView7;
        private DataGridView dataGridView1;
        private DataGridView dataGridView6;
        private Totales1 totales;
        private Totales2 totales2;
        private DataGridView dataGridView8;
        private DataGridView dataGridView9;
        private DataGridView dataGridView11;
        private DataGridView dataGridView10;
        private Consecutivos consecutivo;
        private Elaboro elaboro;
        private string nombreDeSucursal;
        private string codSucursal;
        public Form4(DataGridView dgFacturas,
                     DataGridView dgVales,
                     DataGridView dgCheques,
                     DataGridView dgEfectivo,
                     DataGridView dgFondo,
                     DataGridView dgOtros,
                     Totales1 totales,
                     Totales2 totales2,
                     DataGridView dgChequesDevueltos,
                     DataGridView dgAnticipos,
                     DataGridView dgDocumentos,
                     DataGridView dgNotas,
                     Consecutivos consecutivos,
                     Elaboro elaboro,
                     string sucursal,
                     string codSucursal)
        {
            InitializeComponent();
            this.dataGridView3 = dgFacturas;
            this.dataGridView4 = dgVales;
            this.dataGridView5 = dgCheques;
            this.dataGridView7 = dgEfectivo;
            this.dataGridView1 = dgFondo;
            this.dataGridView6 = dgOtros;
            this.totales = totales;
            this.totales2 = totales2;
            this.dataGridView8 = dgChequesDevueltos;
            this.dataGridView9 = dgAnticipos;
            this.dataGridView11 = dgDocumentos;
            this.dataGridView10 = dgNotas;
            this.consecutivo = consecutivos;
            this.elaboro = elaboro;
            this.nombreDeSucursal = sucursal;
            this.codSucursal = codSucursal;
    }

        private void button1_Click(object sender, EventArgs e)
        {
            using(Form3 fr = new Form3(elaboro, codSucursal, elaboro.sucursal))
            {
                fr.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form2 fr = new Form2(dataGridView3,
                                       dataGridView4,
                                       dataGridView5,
                                       dataGridView7,
                                       dataGridView1,
                                       dataGridView6,
                                       totales,
                                       totales2,
                                       dataGridView8,
                                       dataGridView9,
                                       dataGridView11,
                                       dataGridView10,
                                       consecutivo,
                                       elaboro,
                                       nombreDeSucursal,
                                       codSucursal))
            {
                fr.ShowDialog();
            }
        }
    }
}
