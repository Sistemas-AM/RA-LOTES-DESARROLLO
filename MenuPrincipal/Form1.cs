using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MenuPrincipal
{
    public partial class MenuAmsa : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        public MenuAmsa()
        {
            InitializeComponent();
            activa();
            label2.Text = Variablescompartidas.sucursal;
            obtenSucursal();
            Principal.Variablescompartidas.sucursalcorta = label3.Text;
            Principal.Variablescompartidas.sucural = label2.Text;
            button4.BackColor = ColorTranslator.FromHtml("#F44336");

        }

        private void obtenSucursal()
        {
            cmd.CommandText = "select sucursal from folios where sucnom = '"+Variablescompartidas.sucursal+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                label3.Text = reader["sucursal"].ToString();

            }
            sqlConnection1.Close();
        }

        private void activa()
        {
            //cmd.CommandText = "select permiso from permisosUsuarios where num = '" + Variablescompartidas.num + "'";
            cmd.CommandText = @"select * from Usuarios inner join PermisoPerfil on Usuarios.perfilId = PermisoPerfil.Num
            where Usuarios.Num ='" + Variablescompartidas.num + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            while (reader.Read())
            {
                 //--------------------------------- COTIZACIONES ---------------------
                if (reader["Permiso"].ToString() == "Ventas Especiales")
                {
                    cotizadorVentasEspecialesToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Punto de Venta")
                {
                    puntoDeVentaToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Cotizaciones Techumbre")
                {
                    cotizacionesTechumbreToolStripMenuItem.Visible = true;
                }

               

                else if (reader["Permiso"].ToString() == "Autorizacion Planta")
                {
                    autorizacionesPlantaToolStripMenuItem.Visible = true;
                }

                //------------------------ REPORTES ----------------------------------
                else if (reader["Permiso"].ToString() == "Reporte Cobranza")
                {
                    ReporteCobranza.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Compras")
                {
                    ReporteCompras.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas a Credito Por Sucursal")
                {
                    ReporteVentasCredito.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas Diarias")
                {
                    ReporteVentasDiarias.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas Diarias Sucursales")
                {
                    reporteDeVentasDiariasSucursalesToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas por Unidades")
                {
                    ReporteVentaUnidades.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas por Unidades Sucursales")
                {
                    reporteDeVentasPorUnidadesSucursalesToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Devoluciones")
                {
                    ReporteDevoluciones.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Descuentos por Productos")
                {
                    reporteDescuentos.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas por Sucursal")
                {
                    ReporteVentasSucursal.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas Caja General")
                {
                    ReporteCajaGeneral.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas Planta Caja General")
                {
                    PlantaCajaGen.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Inventario Fisico")
                {
                    InventarioFisico.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Ventas Contabilidad")
                {
                    VentasContabilidad.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte Capacidad Venta y Surtido")
                {
                    CapacidadVenta.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte Material No Conforme")
                {
                    MaterialNoConforme.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Capas")
                {
                    ReporteCapas.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Traspasos")
                {
                    ReporteTraspasos.Visible = true;
                }
                // ---------------------- CORTE -------------------------------------
                else if (reader["Permiso"].ToString() == "Corte")
                {
                    Corte.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Corte General")
                {
                    CorteGeneral.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Formato Corte")
                {
                    FormatoCorte.Visible = true;
                }
                //------------------------ OTROS -----------------------------------
                else if (reader["Permiso"].ToString() == "Carga Archivos")
                {
                    CargaArchivos.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Catalogo de Precios")
                {
                    Catalogo.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Distribucion de Materiales")
                {
                    Distribucion.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Pedido")
                {
                    PedidoRep.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Surtido Para Sucursales Accesorios")
                {
                    SurtidoAccesorios.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Distribucion Materiales Sucursales")
                {
                    DistribucionMat.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Tipo Cambio")
                {
                    TipoCambio.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Carga Maximos Minimos")
                {
                    TipoCambio.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Alta Usuarios")
                {
                    Usuarios.Variablescompartidas.ususario = Variablescompartidas.usuario;
                    Alta.Visible = true;
                }
                else if (reader["Permiso"].ToString()== "Reporte Material Pendiente Entregar")
                {
                    reporteMaterialPendienteEntrengarToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Requerimiento Grua Hiab")
                {
                    gruaHiabToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte Monitor de Retiros")
                {
                    monitorDeRetirosToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Reporte de Relaciones")
                {
                    reporteDeRelacionesToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Entregas Planta (Impresion)")
                {
                    EntregasPla.Form1.Permiso = "Impresion";
                    entregasPlantaToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Entregas Planta (Vista)")
                {
                    EntregasPla.Form1.Permiso = "Vista";
                    entregasPlantaToolStripMenuItem.Visible = true;
                }
                else if (reader["Permiso"].ToString() == "Encuestas")
                {
                    encuestaToolStripMenuItem.Visible = true;
               }


                //------------------------------------- PLANTA------------------------------------------

                else if (reader["Permiso"].ToString() == "Cotizaciones Planta")
                {
                    cotizacionesToolStripMenuItem1.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Ordenes Trabajo")
                {
                    ordenesDeTrabajoToolStripMenuItem.Visible = true;
                }


                else if (reader["Permiso"].ToString() == "Tipo de Clientes")
                {
                    tiposDeClienteToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Categorias")
                {
                    categoriasToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "LaborTeams")
                {
                    laborTeamsToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Operaciones")
                {
                    operacionesToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Autorizacion Operaciones")
                {
                    autorizaOperacionesToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Entrega Materiales")
                {
                    entregaMaterialesToolStripMenuItem.Visible = true;
                }

                else if (reader["Permiso"].ToString() == "Finalizar Orden")
                {
                    salidaMaterialesToolStripMenuItem.Visible = true;
                }
                
            }
            sqlConnection1.Close();

        }

        private void ventasEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion.Form1();
            childForm.Show();
        }

        private void Cotizaciones_Click(object sender, EventArgs e)
        {

            Form childForm = new Cotizacion2020.Form1();
            childForm.Show();
        }

        private void Techumbre_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2.Form1();
            childForm.Show();
        }

        private void ReporteCobranza_Click(object sender, EventArgs e)
        {
            Form childForm = new Reporte_de_Cobranza.Form1();
            childForm.Show();
        }

        private void ReporteCompras_Click(object sender, EventArgs e)
        {
            Form childForm = new Reporte_de_Compras.Form1();
            childForm.Show();
        }

        private void ReporteVentasCredito_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteVentasSucursal.Form1();
            childForm.Show();
        }

        private void ReporteVentasDiarias_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteDeVentasDiarias.Form1();
            childForm.Show();
        }

        private void ReporteVentaUnidades_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteGeneralDeVentasPorUnidades.Form1();
            childForm.Show();
        }

        private void ReporteDevoluciones_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteDevoluciones.Form1();
            childForm.Show();
        }

        private void reporteDescuentos_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteDescuentos.Form1();
            childForm.Show();
        }

        private void ReporteVentasSucursal_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteVentasporSucursal.Form1();
            childForm.Show();
        }

        private void ReporteCajaGeneral_Click(object sender, EventArgs e)
        {
            Form childForm = new RepoLore.Form1();
            childForm.Show();
        }

        private void PlantaCajaGen_Click(object sender, EventArgs e)
        {
            Form childForm = new CortePlanta.Form1();
            childForm.Show();
        }

        private void InventarioFisico_Click(object sender, EventArgs e)
        {
            Form childForm = new repoInv.Form1();
            childForm.Show();
        }

        private void VentasContabilidad_Click(object sender, EventArgs e)
        {
            Form childForm = new Repo2.Form1();
            childForm.Show();
        }

        private void CapacidadVenta_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteCapSucursales.Form1();
            childForm.Show();
        }

        private void MaterialNoConforme_Click(object sender, EventArgs e)
        {
            Form childForm = new RepoMaterialNoConforme.Form1();
            childForm.Show();
        }

        private void ReporteCapas_Click(object sender, EventArgs e)
        {
            Form childForm = new RepoCapas.Form1();
            childForm.Show();
        }

        private void ReporteTraspasos_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteTraspasos.Form1();
            childForm.Show();
        }

        private void Corte_Click(object sender, EventArgs e)
        {
            Form childForm = new Corte.Form1();
            childForm.Show();
        }

        private void CorteGeneral_Click(object sender, EventArgs e)
        {
            Form childForm = new CorteGeneral.Form1();
            childForm.Show();
        }

        private void FormatoCorte_Click(object sender, EventArgs e)
        {
            Form childForm = new Formato.Form1();
            childForm.Show();
        }

        private void CatalogoPrecios_Click(object sender, EventArgs e)
        {
            Form childForm = new CatalagoPrecios.Form1();
            childForm.Show();
        }

        private void CargaArchivos_Click(object sender, EventArgs e)
        {
            Form childForm = new CargaArchivos.Form1();
            childForm.Show();
        }

        private void Distribucion_Click(object sender, EventArgs e)
        {
            Form childForm = new DistribucionMateriales.Form1();
            childForm.Show();
        }

        private void PedidoRep_Click(object sender, EventArgs e)
        {
            Form childForm = new Pedido.Form1();
            childForm.Show();
        }

        private void SurtidoAccesorios_Click(object sender, EventArgs e)
        {
            Form childForm = new SurtidoAccesorios.Form1();
            childForm.Show();
        }

        private void DistribucionMat_Click(object sender, EventArgs e)
        {
            Form childForm = new DistribucionMaterialesSucursales.Form1();
            childForm.Show();
        }

        private void TipoCambio_Click(object sender, EventArgs e)
        {
            Form childForm = new Tipo_Cambio.Form1();
            childForm.Show();
        }

        private void MaximosMin_Click(object sender, EventArgs e)
        {
            Form childForm = new MaximosMinimos.Form1();
            childForm.Show();
        }

        private void FormatoSolidos_Click(object sender, EventArgs e)
        {
            Form childForm = new FormatoSolidos.Form1();
            childForm.Show();
        }

        private void Alta_Click(object sender, EventArgs e)
        {
            Form childForm = new Usuarios.Form1();
            childForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reporteMaterialPendienteEntrengarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteMRPendientes.Form1();
            childForm.Show();
        }

        private void MenuAmsa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reporteDeVentasPorUnidadesSucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteGeneralVentasUnidadesSucursales.Form1();
            childForm.Show();
        }

        private void reporteDeVentasDiariasSucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteDeVentasDiariasSucursales.Form1();
            childForm.Show();
        }

        private void gruaHiabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new GruaHiab.Form1();
            childForm.Show();
        }

        private void monitorDeRetirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new MonitoreoRetiro.Form1();
            childForm.Show();
        }

        private void reporteDeRelacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new ReporteRelaciones.Form1();
            childForm.Show();
        }

        private void entregasPlantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new EntregasPla.Form1();
            childForm.Show();
        }

        private void cotizaciones2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2021.Form1();
            childForm.Show();
        }

        private void ordenesTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new OrdenesOT.Form1();
            childForm.Show();
        }

        private void autorizacionesPlantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new AutorizacionPlanta.Form1();
            childForm.Show();
        }

        private void cotizacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2021.Form1();
            childForm.Show();
        }

        private void ordenesDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new OrdenesOT.Form1();
            childForm.Show();
        }

        private void tiposDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cliente.Form1();
            childForm.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Categorias.Form1();
            childForm.Show();
        }

        private void laborTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new LaborTeams.Form1();
            childForm.Show();
        }

        private void operacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Operaciones.Form1();
            childForm.Show();
        }

        private void encuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Encuesta.Form1();
            childForm.Show();
        }

        private void MenuAmsa_Load(object sender, EventArgs e)
        {

        }

        private void autorizaOperacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form childForm = new AuthOps.Form1();
            //childForm.Show();
        }

        private void entregaMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form childForm = new OrdenEstatus.Form1();
            //childForm.Show();
        }

        private void salidaMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form childForm = new SalidaOrdenes.Form1();
            //childForm.Show();
        }

        private void existenciaCostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cotizacion2020.PantallaProductos.Ocultamos = "1";
            Cotizacion2020.PantallaProductos.sucursalViene = "MAT";
            Form childForm = new Cotizacion2020.PantallaProductos();
            childForm.Show();
        }

        private void cotiza2022ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2022.Form1();
            childForm.Show();
        }

        private void puntoDeVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2022.Form1();
            childForm.Show();
        }

        private void cotizadorVentasEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion.Form1();
            childForm.Show();
        }

        private void cotizacionesTechumbreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Cotizacion2.Form1();
            childForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dfNToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}