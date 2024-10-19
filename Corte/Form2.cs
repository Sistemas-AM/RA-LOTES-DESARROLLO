using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Corte.DBHelper;

namespace Corte
{
    public partial class Form2 : Form
    {
        public Form2(DataGridView dgFacturas,
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
            
            List<FacturaTipoPago> facturaTipoPago = getFacturasTipoPago(codSucursal, DateTime.Parse(elaboro.fecha));
            List<Retiro> Retiros = getRetiros(codSucursal, DateTime.Parse(elaboro.fecha));
            List<FacturaCancelada> facturaCancelada = getFacturasCanceladas(codSucursal, DateTime.Parse(elaboro.fecha));

            DataSet ds = new DataSet();

            DataTable dtFacturas = new DataTable();
            dtFacturas.Columns.Add("concepto", typeof(string));
            dtFacturas.Columns.Add("proveedor", typeof(string));
            dtFacturas.Columns.Add("nofac", typeof(string));
            dtFacturas.Columns.Add("total", typeof(float));

            DataTable dtVales = new DataTable();
            dtVales.Columns.Add("concepto", typeof(string));
            dtVales.Columns.Add("total", typeof(float));

            DataTable dtCheques = new DataTable();
            dtCheques.Columns.Add("concepto", typeof(string));
            dtCheques.Columns.Add("total", typeof(float));

            DataTable dtEfectivo = new DataTable();
            dtEfectivo.Columns.Add("desgloce", typeof(float));
            dtEfectivo.Columns.Add("cantidad", typeof(int));
            dtEfectivo.Columns.Add("total", typeof(float));

            DataTable dtFondo = new DataTable();
            dtFondo.Columns.Add("desgloce", typeof(float));
            dtFondo.Columns.Add("cantidad", typeof(int));
            dtFondo.Columns.Add("total", typeof(float));

            DataTable dtOtros = new DataTable();
            dtOtros.Columns.Add("proveedor", typeof(string));
            dtOtros.Columns.Add("cheque", typeof(float));
            dtOtros.Columns.Add("importe", typeof(float));

            DataTable dtTotales = new DataTable();
            dtTotales.Columns.Add("facturado", typeof(float));
            dtTotales.Columns.Add("notasDeVenta", typeof(float));
            dtTotales.Columns.Add("totalVentasDelDia", typeof(float));
            dtTotales.Columns.Add("credito", typeof(float));
            dtTotales.Columns.Add("totalVentasDeContado", typeof(float));
            dtTotales.Columns.Add("chequesDevueltos", typeof(float));
            dtTotales.Columns.Add("anticipos", typeof(float));
            dtTotales.Columns.Add("devSobreVentas", typeof(float));
            dtTotales.Columns.Add("devDeRemision", typeof(float));
            dtTotales.Columns.Add("comisionPorDev", typeof(float));
            dtTotales.Columns.Add("cobranzaDelDia", typeof(float));
            dtTotales.Columns.Add("otros", typeof(float));
            dtTotales.Columns.Add("efectivo", typeof(float));
            dtTotales.Columns.Add("cheques", typeof(float));
            dtTotales.Columns.Add("vales", typeof(float));
            dtTotales.Columns.Add("facturas", typeof(float));
            dtTotales.Columns.Add("pagoChDevEfectivo", typeof(float));
            dtTotales.Columns.Add("pagoChDevCheque", typeof(float));
            dtTotales.Columns.Add("anticipoConEfectivo", typeof(float));
            dtTotales.Columns.Add("anticipoCCheqTransf", typeof(float));
            dtTotales.Columns.Add("dlls", typeof(float));
            dtTotales.Columns.Add("tc", typeof(float));
            dtTotales.Columns.Add("notaDeCredito", typeof(float));
            dtTotales.Columns.Add("tarjetaDeCredito", typeof(float));
            dtTotales.Columns.Add("tarjetaDeDebito", typeof(float));
            dtTotales.Columns.Add("transferenciaCobranza", typeof(float));
            dtTotales.Columns.Add("total", typeof(float));
            dtTotales.Columns.Add("totalDelDia", typeof(float));

            DataTable dtChequesDevueltos = new DataTable();
            dtChequesDevueltos.Columns.Add("cliente", typeof(string));
            dtChequesDevueltos.Columns.Add("cheque", typeof(int));
            dtChequesDevueltos.Columns.Add("impcheq", typeof(float));
            dtChequesDevueltos.Columns.Add("impefec", typeof(float));
            dtChequesDevueltos.Columns.Add("comcheq", typeof(float));
            dtChequesDevueltos.Columns.Add("comefe", typeof(float));

            DataTable dtAnticipos = new DataTable();
            dtAnticipos.Columns.Add("CRAZONSOCIAL", typeof(string));
            dtAnticipos.Columns.Add("CFOLIO", typeof(int));
            dtAnticipos.Columns.Add("CTEXTOEXTRA1", typeof(string));
            dtAnticipos.Columns.Add("CIMPORTEEXTRA4", typeof(float));
            dtAnticipos.Columns.Add("CTOTAL", typeof(float));
            dtAnticipos.Columns.Add("CTEXTOEXTRA3", typeof(string));
            dtAnticipos.Columns.Add("CIMPORTEEXTRA1", typeof(float));
            dtAnticipos.Columns.Add("CIMPORTEEXTRA3", typeof(float));

            DataTable dtDocumentos = new DataTable();
            dtDocumentos.Columns.Add("concepto", typeof(string));

            DataTable dtNotas = new DataTable();
            dtNotas.Columns.Add("NoAnt", typeof(int));
            dtNotas.Columns.Add("ImporteApl", typeof(float));
            dtNotas.Columns.Add("NoFact", typeof(int));
            dtNotas.Columns.Add("Importe", typeof(float));

            DataTable dtConsecutivos = new DataTable();
            dtConsecutivos.Columns.Add("fact_Inicial", typeof(int));
            dtConsecutivos.Columns.Add("fact_Final", typeof(int));
            dtConsecutivos.Columns.Add("nv_Inicial", typeof(int));
            dtConsecutivos.Columns.Add("nv_Final", typeof(int));

            DataTable dtElaboro = new DataTable();
            dtElaboro.Columns.Add("elaboro", typeof(string));
            dtElaboro.Columns.Add("fecha", typeof(string));
            dtElaboro.Columns.Add("sucursal", typeof(string));

            DataTable dtFacturaTipoPago = new DataTable();
            dtFacturaTipoPago.Columns.Add("tipo", typeof(int));
            dtFacturaTipoPago.Columns.Add("banco", typeof(string));
            dtFacturaTipoPago.Columns.Add("serie", typeof(string));
            dtFacturaTipoPago.Columns.Add("folio", typeof(string));
            dtFacturaTipoPago.Columns.Add("timbrada", typeof(string));
            dtFacturaTipoPago.Columns.Add("importe", typeof(float));

            DataTable dtRetiros = new DataTable();
            dtRetiros.Columns.Add("hora", typeof(string));
            dtRetiros.Columns.Add("folio", typeof(string));
            dtRetiros.Columns.Add("elaboro", typeof(string));
            dtRetiros.Columns.Add("monto", typeof(float));
            dtRetiros.Columns.Add("observa", typeof(string));

            DataTable dtFacturasCanceladas = new DataTable();
            dtFacturasCanceladas.Columns.Add("CCODIGOPRODUCTO", typeof(string));
            dtFacturasCanceladas.Columns.Add("CNOMBREPRODUCTO", typeof(string));
            dtFacturasCanceladas.Columns.Add("cunidades", typeof(float));
            dtFacturasCanceladas.Columns.Add("cfolio", typeof(string));
            dtFacturasCanceladas.Columns.Add("cseriedocumento", typeof(string));
            dtFacturasCanceladas.Columns.Add("cfecha", typeof(string));

            foreach(FacturaCancelada factura in facturaCancelada)
            {
                dtFacturasCanceladas.Rows.Add(factura.CCODIGOPRODUCTO,
                                              factura.CNOMBREPRODUCTO,
                                              factura.cunidades,
                                              factura.cfolio,
                                              factura.cseriedocumento,
                                              factura.cfecha);
            }

            foreach (Retiro retiro in Retiros)
            {
                dtRetiros.Rows.Add(retiro.hora, retiro.folio, retiro.elaboro, retiro.monto, retiro.observa);
            }

            foreach (FacturaTipoPago factura in facturaTipoPago)
            {
                dtFacturaTipoPago.Rows.Add(factura.tipo, factura.banco, factura.serie, factura.folio, factura.timbrada, factura.importe);
            }

            dtElaboro.Rows.Add(elaboro.elaboro, elaboro.fecha, sucursal);

            dtConsecutivos.Rows.Add(consecutivos.fact_Inicial,
                                    consecutivos.fact_Final,
                                    consecutivos.nv_Inicial,
                                    consecutivos.nv_Final);

            dtTotales.Rows.Add(totales.facturado,
                               totales.notasDeVenta,
                               totales.totalVentasDelDia,
                               totales.credito,
                               totales.totalVentasDeContado,
                               totales.chequesDevueltos,
                               totales.anticipos,
                               totales.devSobreVentas,
                               totales.devDeRemision,
                               totales.comisionPorDev,
                               totales.cobranzaDelDia,
                               totales.otros,
                               totales2.efectivo,
                               totales2.cheques,
                               totales2.vales,
                               totales2.facturas,
                               totales2.pagoChDevEfectivo,
                               totales2.pagoChDevCheque,
                               totales2.anticipoConEfectivo,
                               totales2.anticipoCCheqTransf,
                               totales2.dlls,
                               totales2.tc,
                               totales2.notaDeCredito,
                               totales2.tarjetaDeCredito,
                               totales2.tarjetaDeDebito,
                               totales2.transferenciaCobranza,
                               totales2.total,
                               totales.totalDelDia);

            foreach (DataGridViewRow factura in dgFacturas.Rows)
            {
                if (factura.Cells[3].Value != null)
                {
                    dtFacturas.Rows.Add(factura.Cells[0].Value,
                                        factura.Cells[1].Value,
                                        factura.Cells[2].Value,
                                        factura.Cells[3].Value == null ? 0 : float.Parse(factura.Cells[3].Value.ToString()));
                }
            }

            foreach (DataGridViewRow vale in dgVales.Rows)
            {
                if (vale.Cells[1].Value != null)
                {
                    dtVales.Rows.Add(vale.Cells[0].Value,
                                     vale.Cells[1].Value == null ? 0 : float.Parse(vale.Cells[1].Value.ToString()));
                }
            }

            foreach (DataGridViewRow cheque in dgCheques.Rows)
            {
                if (cheque.Cells[1].Value != null)
                {

                    dtCheques.Rows.Add(cheque.Cells[0].Value, float.Parse(cheque.Cells[1].Value.ToString()));
                }
            }

            foreach (DataGridViewRow efectivo in dgEfectivo.Rows)
            {
                if (efectivo.Cells[0].Value != null)
                {
                    int cantidad = efectivo.Cells[1].Value == null ? 0 : int.Parse(efectivo.Cells[1].Value.ToString());
                    float total = efectivo.Cells[2].Value == null ? 0 : float.Parse(efectivo.Cells[2].Value.ToString());
                    dtEfectivo.Rows.Add(float.Parse(efectivo.Cells[0].Value.ToString()), cantidad, total);
                }
            }

            foreach (DataGridViewRow fondo in dgFondo.Rows)
            {
                if (fondo.Cells[0].Value != null)
                {
                    int cantidad = fondo.Cells[1].Value == null ? 0 : int.Parse(fondo.Cells[1].Value.ToString());
                    float total = fondo.Cells[2].Value == null ? 0 : float.Parse(fondo.Cells[2].Value.ToString());
                    dtFondo.Rows.Add(float.Parse(fondo.Cells[0].Value.ToString()), cantidad, total);
                }
            }

            foreach (DataGridViewRow otro in dgOtros.Rows)
            {
                if (otro.Cells[0].Value != null)
                {
                    float cheque = otro.Cells[1].Value == null ? 0 : float.Parse(otro.Cells[1].Value.ToString());
                    string proveedor = otro.Cells[0].Value == null ? "" : otro.Cells[0].Value.ToString();
                    float importe = otro.Cells[2].Value == null ? 0 : float.Parse(otro.Cells[2].Value.ToString());
                    dtOtros.Rows.Add(otro.Cells[0].Value, cheque, importe);
                }
            }

            foreach (DataGridViewRow devuelto in dgChequesDevueltos.Rows)
            {
                if (devuelto.Cells[0].Value != null)
                {
                    string cliente = devuelto.Cells[0].Value == null ? "" : devuelto.Cells[0].Value.ToString();
                    int cheque = devuelto.Cells[1].Value == null ? 0 : int.Parse(devuelto.Cells[1].Value.ToString());
                    float impcheq = devuelto.Cells[2].Value == null ? 0 : float.Parse(devuelto.Cells[2].Value.ToString());
                    float impefec = devuelto.Cells[3].Value == null ? 0 : float.Parse(devuelto.Cells[3].Value.ToString());
                    float comcheq = devuelto.Cells[5].Value == null ? 0 : float.Parse(devuelto.Cells[5].Value.ToString());
                    float comefe = devuelto.Cells[6].Value == null ? 0 : float.Parse(devuelto.Cells[6].Value.ToString());
                    dtChequesDevueltos.Rows.Add(cliente, cheque, impcheq, impefec, comcheq, comefe);
                }
            }

            foreach (DataGridViewRow anticipo in dgAnticipos.Rows)
            {
                if (anticipo.Cells[0].Value != null)
                {

                    string CRAZONSOCIAL = anticipo.Cells[0].Value == null ? " " : anticipo.Cells[0].Value.ToString();
                    int CFOLIO = anticipo.Cells[1].Value == null ? 0 : int.Parse(anticipo.Cells[1].Value.ToString());
                    string CTEXTOEXTRA1 = anticipo.Cells[2].Value == null ? "." : anticipo.Cells[2].Value.ToString();
                    float CIMPORTEEXTRA4 = anticipo.Cells[3].Value == null ? 0 : float.Parse(anticipo.Cells[3].Value.ToString());
                    float CTOTAL = anticipo.Cells[4].Value == null ? 0 : float.Parse(anticipo.Cells[4].Value.ToString());
                    string CTEXTOEXTRA3 = anticipo.Cells[6].Value == null ? "." : anticipo.Cells[6].Value.ToString();
                    float CIMPORTEEXTRA1 = anticipo.Cells[7].Value == null ? 0 : float.Parse(anticipo.Cells[7].Value.ToString());
                    float CIMPORTEEXTRA3 = anticipo.Cells[8].Value == null ? 0 : float.Parse(anticipo.Cells[8].Value.ToString());
                    dtAnticipos.Rows.Add(CRAZONSOCIAL,
                                                CFOLIO,
                                                CTEXTOEXTRA1,
                                                CIMPORTEEXTRA4,
                                                CTOTAL,
                                                CTEXTOEXTRA3,
                                                CIMPORTEEXTRA1,
                                                CIMPORTEEXTRA3);
                }
            }

            foreach (DataGridViewRow documento in dgDocumentos.Rows)
            {
                if (documento.Cells[0].Value != null)
                {
                    string concepto = documento.Cells[0].Value == null ? " " : documento.Cells[0].Value.ToString();
                    dtDocumentos.Rows.Add(concepto);
                }
            }

            foreach (DataGridViewRow nota in dgNotas.Rows)
            {
                if (nota.Cells[0].Value != null)
                {
                    int NoAnt = nota.Cells[0].Value == null ? 0 : int.Parse(nota.Cells[0].Value.ToString());
                    float ImporteApl = nota.Cells[0].Value == null ? 0 : float.Parse(nota.Cells[0].Value.ToString());
                    int NoFact = nota.Cells[0].Value == null ? 0 : int.Parse(nota.Cells[0].Value.ToString());
                    float Importe = nota.Cells[0].Value == null ? 0 : float.Parse(nota.Cells[0].Value.ToString());
                    dtNotas.Rows.Add(NoAnt, ImporteApl, NoFact, Importe);
                }
            }


            ds.Tables.Add(dtFacturas);
            ds.Tables.Add(dtVales);
            ds.Tables.Add(dtCheques);
            ds.Tables.Add(dtEfectivo);
            ds.Tables.Add(dtFondo);
            ds.Tables.Add(dtOtros);
            ds.Tables.Add(dtTotales);
            ds.Tables.Add(dtChequesDevueltos);
            ds.Tables.Add(dtAnticipos);
            ds.Tables.Add(dtDocumentos);
            ds.Tables.Add(dtNotas);
            ds.Tables.Add(dtConsecutivos);
            ds.Tables.Add(dtElaboro);
            ds.Tables.Add(dtFacturaTipoPago);
            ds.Tables.Add(dtRetiros);
            ds.Tables.Add(dtFacturasCanceladas);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Corte\CrystalReport1.rpt");
            rd.Database.Tables[0].SetDataSource(ds.Tables[12]);
            rd.Subreports["InformeFacturas"].Database.Tables[0].SetDataSource(ds.Tables[0]);
            rd.Subreports["InformeVales"].Database.Tables[0].SetDataSource(ds.Tables[1]);
            rd.Subreports["InformeCheques"].Database.Tables[0].SetDataSource(ds.Tables[2]);
            rd.Subreports["InformeEfectivo"].Database.Tables[0].SetDataSource(ds.Tables[3]);
            rd.Subreports["InformeFondo"].Database.Tables[0].SetDataSource(ds.Tables[4]);
            rd.Subreports["InformeOtros"].Database.Tables[0].SetDataSource(ds.Tables[5]);
            rd.Subreports["InformeTotales"].Database.Tables[0].SetDataSource(ds.Tables[6]);
            rd.Subreports["InformeTotales2"].Database.Tables[0].SetDataSource(ds.Tables[6]);
            rd.Subreports["InformeChequesDevueltos"].Database.Tables[0].SetDataSource(ds.Tables[7]);
            rd.Subreports["InformeAnticipos"].Database.Tables[0].SetDataSource(ds.Tables[8]);
            rd.Subreports["InformeDocumentos"].Database.Tables[0].SetDataSource(ds.Tables[9]);
            rd.Subreports["InformeNotasDeCredito"].Database.Tables[0].SetDataSource(ds.Tables[10]);
            rd.Subreports["InformeConsecutivos"].Database.Tables[0].SetDataSource(ds.Tables[11]);
            rd.Subreports["InformeFacturaTipoPago"].Database.Tables[0].SetDataSource(ds.Tables[13]);
            rd.Subreports["InformeRetiros"].Database.Tables[0].SetDataSource(ds.Tables[14]);
            rd.Subreports["InformeFacturasCanceladas"].Database.Tables[0].SetDataSource(ds.Tables[15]);

           
            rd.SetParameterValue("pre", Variablescompartidas.cr);
            
            //}else if (Variablescompartidas.cr != "1")
            //{
            //    rd.SetParameterValue("pre", "PRELIMINAR");
            //}
            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
