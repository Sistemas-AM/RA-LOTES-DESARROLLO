using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Menu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cotizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Cotizacion.Form1 form = new Cotizacion.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasACreditoPorSucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ReporteVentasSucursal.Form1 form = new ReporteVentasSucursal.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reporte_de_Compras.Form1 form = new Reporte_de_Compras.Form1())
            {
                form.ShowDialog();
            }
        }

        private void cotizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Cotizaciones.Form1 form = new Cotizaciones.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeCobranzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Reporte_de_Cobranza.Form1 form = new Reporte_de_Cobranza.Form1())
            {
                form.ShowDialog();
            }
        }

        private void corteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using(Corte.Form1 form = new Corte.Form1())
            {
                form.ShowDialog();
            }
        }

        private void corteGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(CorteGeneral.Form1 form = new CorteGeneral.Form1())
            {
                form.ShowDialog();
            }
        }

        private void cargaArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(CargaArchivos.Form1 form = new CargaArchivos.Form1())
            {
                form.ShowDialog();
            }
        }

        private void catalagoDePreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(CatalagoPrecios.Form1 form = new CatalagoPrecios.Form1())
            {
                form.ShowDialog();
            }
        }

        private void distribucionDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(DistribucionMateriales.Form1 form = new DistribucionMateriales.Form1())
            {
                form.ShowDialog();
            }
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(Pedido.Form1 form = new Pedido.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasDiariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ReporteDeVentasDiarias.Form1 form = new ReporteDeVentasDiarias.Form1())
            {
                form.ShowDialog();
            }
        }

        private void formatoCorteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Formato.Form1 form = new Formato.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasPorUnidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(ReporteGeneralDeVentasPorUnidades.Form1 form = new ReporteGeneralDeVentasPorUnidades.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeDevolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ReporteDevoluciones.Form1 form = new ReporteDevoluciones.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeDescuentosPorProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ReporteDescuentos.Form1 form = new ReporteDescuentos.Form1())
            {
                form.ShowDialog();
            }
        }

        private void surtidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SurtidoAccesorios.Form1 form = new SurtidoAccesorios.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasPorSucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(ReporteVentasporSucursal.Form1 form = new ReporteVentasporSucursal.Form1())
            {
                form.ShowDialog();
            }
        }

        private void distribucionMaterialesSucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(DistribucionMaterialesSucursales.Form1 form = new DistribucionMaterialesSucursales.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasCajaGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RepoLore.Form1 form = new RepoLore.Form1())
            {
                form.ShowDialog();
            }
        }

        private void reporteDeVentasPlantaCajaGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CortePlanta.Form1 form = new CortePlanta.Form1())
            {
                form.ShowDialog();
            }

        }

        private void tipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Tipo_Cambio.Form1 tc = new Tipo_Cambio.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void reporteInventarioFisicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (repoInv.Form1 tc = new repoInv.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void cargaMaximosMinimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MaximosMinimos.Form1 tc = new MaximosMinimos.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void reporteDeVentasContabilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Repo2.Form1 tc = new Repo2.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void cotizacionesTechumbreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Cotizacion2.Form1 tc = new Cotizacion2.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void reporteCapacidadVentaYSurtidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ReporteCapSucursales.Form1 tc = new ReporteCapSucursales.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reporteDeTraspasosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RepTraspasos.Form1 tc = new RepTraspasos.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void formatoDeSolidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormatoSolidos.Form1 tc = new FormatoSolidos.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void reporteMaterialNoConformeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RepoMaterialNoConforme.Form1 tc = new RepoMaterialNoConforme.Form1())
            {
                tc.ShowDialog();
            }
        }

        private void reporteDeCapasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RepoCapas.Form1 tc = new RepoCapas.Form1())
            {
                tc.ShowDialog();
            }
        }
    }
}
